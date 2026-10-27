using System.Text.Json;
using Yahtzee.Core;
using Yahtzee.Storage;
namespace Yahtzee.Tests;

[TestClass]
public class StorageTests
{
    private string folder = "";

    [TestInitialize]
    public void Setup() => Directory.CreateDirectory(folder = Path.Combine(Path.GetTempPath(), "f21sc-yahtzee-" + Guid.NewGuid()));

    [TestCleanup]
    public void Cleanup() => Directory.Delete(folder, true);

    [TestMethod]
    public void SaveFileRoundTrips()
    {
        var game = new GameModel();
        game.Start([new Player("Ann", PlayerType.Human), new Player("Bot", PlayerType.Computer)]);
        game.Roll();
        var path = Path.Combine(folder, "game.json");
        GameFiles.Save(path, game.Snapshot());

        var loaded = new GameModel();
        loaded.Restore(GameFiles.Load(path));
        Assert.AreEqual(PlayerType.Computer, loaded.Players[1].Type);
        CollectionAssert.AreEqual(game.Dice.ToArray(), loaded.Dice.ToArray());
    }

    [TestMethod]
    [DataRow("not json")]
    [DataRow("null")]
    [DataRow("{\"Players\":null}")]
    public void CorruptSaveFileIsRejected(string text)
    {
        var path = Path.Combine(folder, "bad.json");
        File.WriteAllText(path, text);
        Assert.ThrowsExactly<JsonException>(() => GameFiles.Load(path));
    }

    [TestMethod]
    public void TopScoresAreOrderedLimitedAndFiltered()
    {
        var db = Path.Combine(folder, "games.db");
        var t = new DateTime(2026, 10, 1);
        GameStore.Record(db, t, [("Ann", 100), ("Bob", 200)]);
        GameStore.Record(db, t.AddDays(1), [("Ann", 150), ("Cy", 120)]);
        for (int i = 0; i < 5; i++) GameStore.Record(db, t.AddDays(2 + i), [("Ann", 10 + i)]);

        var top = GameStore.TopScores(db);
        Assert.HasCount(5, top);
        CollectionAssert.AreEqual(new[] { 200, 150, 120, 100, 14 }, top.Select(h => h.Score).ToArray());
        Assert.AreEqual(t.AddDays(1), top[1].PlayedAt);

        var ann = GameStore.TopScores(db, "Ann");
        Assert.IsTrue(ann.All(h => h.Player == "Ann"));
        CollectionAssert.AreEqual(new[] { 150, 100, 14, 13, 12 }, ann.Select(h => h.Score).ToArray());
        Assert.IsEmpty(GameStore.TopScores(db, "Nobody"));
    }
}
