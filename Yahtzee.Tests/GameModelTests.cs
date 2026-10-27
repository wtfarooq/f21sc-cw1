using Yahtzee.Core;
namespace Yahtzee.Tests;

[TestClass]
public class GameModelTests
{
    private static GameModel NewGame(int players = 2, PlayerType type = PlayerType.Human)
    {
        var game = new GameModel(new Random(1));
        game.Start(Enumerable.Range(1, players).Select(i => new Player($"P{i}", type)));
        return game;
    }

    [TestMethod]
    public void StartRejectsBadPlayerCounts()
    {
        var game = new GameModel();
        Assert.ThrowsExactly<ArgumentException>(() => game.Start([]));
        Assert.ThrowsExactly<ArgumentException>(() => game.Start(Enumerable.Range(0, 5).Select(i => new Player("x", PlayerType.Human))));
        Assert.ThrowsExactly<ArgumentException>(() => new Player(" ", PlayerType.Human));
    }

    [TestMethod]
    public void RollsAreLimitedToThree()
    {
        var game = NewGame();
        game.Roll(); game.Roll(); game.Roll();
        Assert.AreEqual(0, game.RollsLeft);
        Assert.ThrowsExactly<InvalidOperationException>(game.Roll);
    }

    [TestMethod]
    public void HeldDiceAreNotRerolled()
    {
        var game = NewGame();
        game.Roll();
        int first = game.Dice[0];
        game.ToggleHold(0);
        game.Roll();
        Assert.AreEqual(first, game.Dice[0]);
    }

    [TestMethod]
    public void HoldNeedsARollFirst()
    {
        var game = NewGame();
        Assert.ThrowsExactly<InvalidOperationException>(() => game.ToggleHold(0));
        game.Roll();
        Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => game.ToggleHold(5));
    }

    [TestMethod]
    public void ScoringNeedsARollAndAnUnusedCategory()
    {
        var game = NewGame(1);
        Assert.ThrowsExactly<InvalidOperationException>(() => game.Score(ScoreCategory.Chance));
        game.Roll();
        game.Score(ScoreCategory.Chance);
        game.Roll();
        Assert.ThrowsExactly<InvalidOperationException>(() => game.Score(ScoreCategory.Chance));
    }

    [TestMethod]
    public void ScoringPassesTurnAndResetsDice()
    {
        var game = NewGame();
        game.Roll();
        game.Score(ScoreCategory.Chance);
        Assert.AreEqual(1, game.CurrentIndex);
        Assert.AreEqual(0, game.RollsUsed);
        Assert.IsTrue(game.Held.All(h => !h));
    }

    [TestMethod]
    public void FailedMovesDoNotRaiseChanged()
    {
        var game = NewGame();
        int changes = 0;
        game.Changed += (_, _) => changes++;
        Assert.ThrowsExactly<InvalidOperationException>(() => game.Score(ScoreCategory.Chance));
        Assert.AreEqual(0, changes);
    }

    [TestMethod]
    public void GameCompletesAfterThirteenRoundsEach()
    {
        var game = NewGame(2);
        foreach (var category in Enum.GetValues<ScoreCategory>())
            for (int p = 0; p < 2; p++) { game.Roll(); game.Score(category); }
        Assert.IsTrue(game.IsComplete);
        Assert.IsFalse(game.InProgress);
        Assert.ThrowsExactly<InvalidOperationException>(game.Roll);
    }

    [TestMethod]
    public void ComputerPlaysAWholeGame()
    {
        var game = NewGame(2, PlayerType.Computer);
        while (game.InProgress) ComputerStrategy.PlayTurn(game);
        Assert.IsTrue(game.Players.All(p => p.Card.IsFull));
    }

    [TestMethod]
    public void SnapshotRoundTrips()
    {
        var game = NewGame(2);
        game.Roll(); game.Score(ScoreCategory.Chance);
        game.Roll(); game.ToggleHold(2);
        var copy = new GameModel();
        copy.Restore(game.Snapshot());
        Assert.AreEqual(game.CurrentIndex, copy.CurrentIndex);
        Assert.AreEqual(game.RollsUsed, copy.RollsUsed);
        CollectionAssert.AreEqual(game.Dice.ToArray(), copy.Dice.ToArray());
        CollectionAssert.AreEqual(game.Held.ToArray(), copy.Held.ToArray());
        Assert.AreEqual(game.Players[0].Card.Total, copy.Players[0].Card.Total);
        Assert.AreEqual("P1", copy.Players[0].Name);
    }

    [TestMethod]
    public void BadSnapshotLeavesGameUnchanged()
    {
        var game = NewGame(2);
        game.Roll();
        var bad = game.Snapshot();
        bad.CurrentPlayer = 7;
        Assert.ThrowsExactly<ArgumentException>(() => game.Restore(bad));
        var badScores = game.Snapshot();
        badScores.Players[1].Scores[0] = 3; // second player ahead of the first
        Assert.ThrowsExactly<ArgumentException>(() => game.Restore(badScores));
        Assert.AreEqual(1, game.RollsUsed);
        Assert.HasCount(2, game.Players);
    }
}
