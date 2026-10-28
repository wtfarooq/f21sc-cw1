namespace Yahtzee.Core;

// Model: owns the game state, validates every move and raises Changed after a valid one.
public sealed class GameModel
{
    public const int MaxRolls = 3;
    private readonly Random random;
    private readonly int[] dice = new int[5];
    private readonly bool[] held = new bool[5];

    public GameModel(Random? random = null) => this.random = random ?? Random.Shared;

    public event EventHandler? Changed;

    public IReadOnlyList<Player> Players { get; private set; } = [];
    public IReadOnlyList<int> Dice => dice;
    public IReadOnlyList<bool> Held => held;
    public int RollsUsed { get; private set; }
    public int CurrentIndex { get; private set; }

    public Player CurrentPlayer => Players[CurrentIndex];
    public int RollsLeft => MaxRolls - RollsUsed;
    public bool IsStarted => Players.Count > 0;
    public bool IsComplete => IsStarted && Players.All(p => p.Card.IsFull);
    public bool InProgress => IsStarted && !IsComplete;

    public void Start(IEnumerable<Player> players)
    {
        var list = players.ToArray();
        if (list.Length is < 1 or > 4) throw new ArgumentException("A game needs 1 to 4 players.");
        Players = list;
        ResetTurn();
        CurrentIndex = 0;
        Changed?.Invoke(this, EventArgs.Empty);
    }

    public void Roll()
    {
        if (!InProgress) throw new InvalidOperationException("No game in progress.");
        if (RollsLeft == 0) throw new InvalidOperationException("No rolls left; choose a category.");
        for (int i = 0; i < dice.Length; i++)
            if (RollsUsed == 0 || !held[i]) dice[i] = random.Next(1, 7);
        RollsUsed++;
        Changed?.Invoke(this, EventArgs.Empty);
    }

    public void ToggleHold(int index)
    {
        if (!InProgress) throw new InvalidOperationException("No game in progress.");
        if (RollsUsed == 0 || RollsLeft == 0) throw new InvalidOperationException("Dice can only be held between rolls.");
        ArgumentOutOfRangeException.ThrowIfNegative(index);
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, dice.Length);
        held[index] = !held[index];
        Changed?.Invoke(this, EventArgs.Empty);
    }

    public int Preview(ScoreCategory category)
    {
        if (RollsUsed == 0) throw new InvalidOperationException("Roll the dice first.");
        return Scoring.Score(category, dice);
    }

    public void Score(ScoreCategory category)
    {
        if (!InProgress) throw new InvalidOperationException("No game in progress.");
        CurrentPlayer.Card.Set(category, Preview(category));
        CurrentIndex = (CurrentIndex + 1) % Players.Count;
        ResetTurn();
        Changed?.Invoke(this, EventArgs.Empty);
    }

    public GameSnapshot Snapshot() => new()
    {
        Players = Players.Select(p => new PlayerSnapshot { Name = p.Name, Type = p.Type, Strategy = p.Strategy, Scores = [.. p.Card.Scores] }).ToList(),
        CurrentPlayer = CurrentIndex,
        RollsUsed = RollsUsed,
        Dice = [.. dice],
        Held = [.. held],
    };

    // Validate everything first; only replace the current game if the snapshot is sound.
    public void Restore(GameSnapshot s)
    {
        if (s.Players.Count is < 1 or > 4) throw new ArgumentException("Bad player count.");
        if (s.CurrentPlayer < 0 || s.CurrentPlayer >= s.Players.Count) throw new ArgumentException("Bad current player.");
        if (s.RollsUsed < 0 || s.RollsUsed > MaxRolls) throw new ArgumentException("Bad roll count.");
        if (s.Dice.Length != 5 || s.Held.Length != 5) throw new ArgumentException("Bad dice.");
        if (s.Dice.Any(d => d < (s.RollsUsed == 0 ? 0 : 1) || d > 6)) throw new ArgumentException("Bad dice values.");

        var players = new List<Player>();
        foreach (var p in s.Players)
        {
            if (p.Scores.Length != 13 || p.Scores.Any(x => x is < 0 or > 50)) throw new ArgumentException("Bad scores.");
            if (p.Strategy is AiStrategy strategy && !Enum.IsDefined(strategy)) throw new ArgumentException("Bad AI strategy.");
            var player = new Player(p.Name, p.Type, p.Strategy);
            for (int i = 0; i < 13; i++)
                if (p.Scores[i] is int score) player.Card.Set((ScoreCategory)i, score);
            players.Add(player);
        }

        // Players before the current one have played one more round than the rest.
        var filled = players.Select(p => p.Card.Scores.Count(x => x != null)).ToArray();
        for (int i = 0; i < filled.Length; i++)
            if (filled[i] != filled[s.CurrentPlayer] + (i < s.CurrentPlayer ? 1 : 0))
                throw new ArgumentException("Scores do not match the current turn.");
        if (players.All(p => p.Card.IsFull)) throw new ArgumentException("The game is already finished.");

        Players = players;
        CurrentIndex = s.CurrentPlayer;
        RollsUsed = s.RollsUsed;
        s.Dice.CopyTo(dice, 0);
        s.Held.CopyTo(held, 0);
        Changed?.Invoke(this, EventArgs.Empty);
    }

    private void ResetTurn()
    {
        RollsUsed = 0;
        Array.Clear(dice);
        Array.Clear(held);
    }
}
