namespace Yahtzee.Core;

// Greedy keeps the most common face and maximizes points
// Random rerolls and picks any free category.
public static class ComputerStrategy
{
    public static string PlayTurn(GameModel game) =>
        PlayTurnAsync(game).GetAwaiter().GetResult();

    // After every visible change the model raises Changed (the form repaints),
    // then we await pause so the user actually sees each roll and hold.
    public static async Task<string> PlayTurnAsync(GameModel game, Func<Task>? pause = null)
    {
        var strategy = game.CurrentPlayer.Strategy
            ?? throw new InvalidOperationException("The current player has no AI strategy.");
        game.Roll();
        if (pause != null) await pause();
        while (game.RollsLeft > 0 && strategy == AiStrategy.Greedy)
        {
            int face = game.Dice.GroupBy(d => d).OrderByDescending(g => g.Count()).ThenByDescending(g => g.Key).First().Key;
            for (int i = 0; i < game.Dice.Count; i++)
                if (game.Held[i] != (game.Dice[i] == face)) game.ToggleHold(i);
            if (pause != null) await pause();
            if (game.Held.All(h => h)) break;
            game.Roll();
            if (pause != null) await pause();
        }
        while (game.RollsLeft > 0)
        {
            game.Roll();
            if (pause != null) await pause();
        }

        var player = game.CurrentPlayer;
        var available = Enum.GetValues<ScoreCategory>().Where(c => !player.Card.IsUsed(c)).ToArray();
        var category = strategy == AiStrategy.Greedy
            ? available.MaxBy(game.Preview)
            : available[Random.Shared.Next(available.Length)];
        int points = game.Preview(category);
        game.Score(category);
        return $"{player.Name} scored {points} in {category}.";
    }
}
