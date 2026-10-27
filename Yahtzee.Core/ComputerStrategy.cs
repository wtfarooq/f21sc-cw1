namespace Yahtzee.Core;

// Trivial AI: keep the most common face, reroll the rest, then take the best-scoring free category.
public static class ComputerStrategy
{
    public static string PlayTurn(GameModel game) =>
        PlayTurnAsync(game).GetAwaiter().GetResult();

    // After every visible change the model raises Changed (the form repaints),
    // then we await pause so the user actually sees each roll and hold.
    public static async Task<string> PlayTurnAsync(GameModel game, Func<Task>? pause = null)
    {
        game.Roll();
        if (pause != null) await pause();
        while (game.RollsLeft > 0)
        {
            int face = game.Dice.GroupBy(d => d).OrderByDescending(g => g.Count()).ThenByDescending(g => g.Key).First().Key;
            for (int i = 0; i < game.Dice.Count; i++)
                if (game.Held[i] != (game.Dice[i] == face)) game.ToggleHold(i);
            if (pause != null) await pause();
            if (game.Held.All(h => h)) break;
            game.Roll();
            if (pause != null) await pause();
        }

        var player = game.CurrentPlayer;
        var category = Enum.GetValues<ScoreCategory>().Where(c => !player.Card.IsUsed(c)).MaxBy(game.Preview);
        int points = game.Preview(category);
        game.Score(category);
        return $"{player.Name} scored {points} in {category}.";
    }
}
