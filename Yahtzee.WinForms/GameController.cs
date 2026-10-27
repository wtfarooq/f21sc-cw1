using System.Data.Common;
using System.Text.Json;
using Yahtzee.Core;
using Yahtzee.Storage;
namespace Yahtzee.WinForms;

public sealed record OperationOutcome(bool Success, string Message);

// Controller: the form calls these methods; expected failures become an OperationOutcome.
public sealed class GameController
{
    private readonly GameModel model;
    private readonly string dbPath;
    private bool recorded;

    public GameController(GameModel model, string dbPath)
    {
        this.model = model;
        this.dbPath = dbPath;
        model.Changed += (_, _) => IsDirty = true;
    }

    // True when the game changed since it was last saved (or started/loaded).
    public bool IsDirty { get; private set; }

    public OperationOutcome NewGame(IEnumerable<(string Name, PlayerType Type)> players) =>
        Execute(() =>
        {
            model.Start(players.Select(p => new Player(p.Name, p.Type)));
            recorded = false;
            IsDirty = false;
        }, "New game started.");

    public OperationOutcome Roll() => Execute(model.Roll, "Rolled.");

    public OperationOutcome ToggleHold(int index) => Execute(() => model.ToggleHold(index), "Hold changed.");

    public OperationOutcome Score(ScoreCategory category) =>
        Execute(() => { model.Score(category); RecordIfComplete(); }, $"Scored {category}.");

    // Lets computer players take their turns until a human (or the end) is reached.
    // Each pause lets the form repaint, so every computer roll and hold is visible.
    public Task<OperationOutcome> PlayComputerTurnsAsync(Func<Task>? pause = null) =>
        ExecuteAsync(async () =>
        {
            var messages = new List<string>();
            while (model.InProgress && model.CurrentPlayer.Type == PlayerType.Computer)
                messages.Add(await ComputerStrategy.PlayTurnAsync(model, pause));
            RecordIfComplete();
            return string.Join(" ", messages);
        });

    public OperationOutcome Save(string path) =>
        Execute(() => { GameFiles.Save(path, model.Snapshot()); IsDirty = false; }, "Game saved.");

    public OperationOutcome Load(string path) =>
        Execute(() => { model.Restore(GameFiles.Load(path)); recorded = false; IsDirty = false; }, "Game loaded.");

    public List<HighScore> HighScores(string? player) =>
        GameStore.TopScores(dbPath, string.IsNullOrWhiteSpace(player) ? null : player.Trim());

    // Players ordered by score, highest first.
    public string Summary() =>
        string.Join(Environment.NewLine, model.Players.OrderByDescending(p => p.Card.Total)
            .Select((p, i) => $"{i + 1}. {p.Name} - {p.Card.Total}"));

    private void RecordIfComplete()
    {
        if (!model.IsComplete || recorded) return;
        recorded = true;
        GameStore.Record(dbPath, DateTime.Now, model.Players.Select(p => (p.Name, p.Card.Total)));
    }

    private static OperationOutcome Execute(Action action, string success) =>
        Execute(() => { action(); return success; });

    private static OperationOutcome Execute(Func<string> action)
    {
        try
        {
            return new OperationOutcome(true, action());
        }
        catch (Exception ex) when (ex is ArgumentException or InvalidOperationException or IOException
            or UnauthorizedAccessException or JsonException or DbException)
        {
            return new OperationOutcome(false, ex.Message);
        }
    }

    private static async Task<OperationOutcome> ExecuteAsync(Func<Task<string>> action)
    {
        try
        {
            return new OperationOutcome(true, await action());
        }
        catch (Exception ex) when (ex is ArgumentException or InvalidOperationException or IOException
            or UnauthorizedAccessException or JsonException or DbException)
        {
            return new OperationOutcome(false, ex.Message);
        }
    }
}
