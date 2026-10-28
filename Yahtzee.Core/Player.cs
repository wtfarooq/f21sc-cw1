namespace Yahtzee.Core;

public enum PlayerType { Human, Computer }
public enum AiStrategy { Greedy, Random }

// One score per category; null means the category is not used yet.
public sealed class Scorecard
{
    public int?[] Scores { get; } = new int?[13];
    public int Total => Scores.Sum(s => s ?? 0);
    public bool IsFull => Scores.All(s => s != null);
    public bool IsUsed(ScoreCategory category) => Scores[(int)category] != null;

    public void Set(ScoreCategory category, int score)
    {
        if (IsUsed(category)) throw new InvalidOperationException($"{category} is already used.");
        Scores[(int)category] = score;
    }
}

public sealed class Player
{
    public string Name { get; }
    public PlayerType Type { get; }
    public AiStrategy? Strategy { get; }
    public Scorecard Card { get; } = new();

    public Player(string name, PlayerType type, AiStrategy? strategy = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        Name = name.Trim();
        Type = type;
        Strategy = type == PlayerType.Computer ? strategy ?? AiStrategy.Greedy : null;
    }
}
