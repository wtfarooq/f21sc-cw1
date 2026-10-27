using Yahtzee.Core;
namespace Yahtzee.WinForms;

// View: handlers forward to the controller; RefreshState only displays the model.
public partial class MainForm : Form
{
    private GameModel model = null!;
    private GameController controller = null!;
    private bool busy; // true while computer turns are animating; blocks overlapping moves

    public MainForm()
    {
        InitializeComponent();
        dice = new CheckBox[] { dice0, dice1, dice2, dice3, dice4 };
    }

    public void Bind(GameModel model, GameController controller)
    {
        this.model = model;
        this.controller = controller;
        model.Changed += RefreshState;

        for (int i = 0; i < dice.Length; i++)
        {
            int index = i;
            dice[i].Click += (_, _) => { if (!busy) Show(controller.ToggleHold(index)); };
        }
        newButton.Click += NewClicked;
        rollButton.Click += (_, _) => Show(controller.Roll());
        scoreButton.Click += ScoreClicked;
        saveButton.Click += (_, _) => SaveGame();
        loadButton.Click += LoadClicked;
        historyButton.Click += (_, _) => { using var dialog = new HistoryDialog(controller); dialog.ShowDialog(this); };
        FormClosing += ClosingForm;
        FormClosed += (_, _) => model.Changed -= RefreshState;
        RefreshState(this, EventArgs.Empty);
    }

    private void NewClicked(object? sender, EventArgs e)
    {
        using var dialog = new SetupDialog();
        if (dialog.ShowDialog(this) == DialogResult.OK) Run(() => controller.NewGame(dialog.Players));
    }

    private void ScoreClicked(object? sender, EventArgs e)
    {
        int row = grid.CurrentCell?.RowIndex ?? -1;
        if (row < 0 || row >= 13) message.Text = "Select a category first.";
        else Run(() => controller.Score((ScoreCategory)row));
    }

    private void LoadClicked(object? sender, EventArgs e)
    {
        using var dialog = new OpenFileDialog { Title = "Load game", Filter = "Yahtzee save (*.json)|*.json", CheckFileExists = true };
        if (dialog.ShowDialog(this) == DialogResult.OK) Run(() => controller.Load(dialog.FileName));
    }

    // Returns false if the user cancelled or saving failed.
    private bool SaveGame()
    {
        using var dialog = new SaveFileDialog { Title = "Save game", Filter = "Yahtzee save (*.json)|*.json", DefaultExt = "json" };
        if (dialog.ShowDialog(this) != DialogResult.OK) return false;
        var outcome = controller.Save(dialog.FileName);
        Show(outcome);
        return outcome.Success;
    }

    private void ClosingForm(object? sender, FormClosingEventArgs e)
    {
        if (!model.InProgress || !controller.IsDirty) return;
        var answer = MessageBox.Show(this, "Save the game before exiting?", "Yahtzee", MessageBoxButtons.YesNoCancel);
        if (answer == DialogResult.Cancel || (answer == DialogResult.Yes && !SaveGame())) e.Cancel = true;
    }

    // Runs an action, then lets computer players take their turns with a short
    // pause after each roll/hold so every move is visible, then shows the summary if the game ended.
    private async void Run(Func<OperationOutcome> action)
    {
        if (busy) return;
        busy = true;
        RefreshState(this, EventArgs.Empty);
        try
        {
            bool wasInProgress = model.InProgress;
            var outcome = action();
            Show(outcome);
            if (outcome.Success)
            {
                var computer = await controller.PlayComputerTurnsAsync(ComputerPause);
                Show(new OperationOutcome(true, $"{outcome.Message} {computer.Message}".Trim()));
            }
            if (outcome.Success && wasInProgress && model.IsComplete)
                MessageBox.Show(this, controller.Summary(), "Game over");
        }
        finally
        {
            busy = false;
            RefreshState(this, EventArgs.Empty);
        }
    }

    private static Task ComputerPause() => Task.Delay(600);

    // ⚀⚁⚂⚃⚄⚅ (U+2680-U+2685).
    private static string DieFace(int value) => value switch
    {
        1 => "\u2680",
        2 => "\u2681",
        3 => "\u2682",
        4 => "\u2683",
        5 => "\u2684",
        6 => "\u2685",
        _ => "-",
    };

    private void Show(OperationOutcome outcome) => message.Text = outcome.Message;

    private void RefreshState(object? sender, EventArgs e)
    {
        bool humanTurn = model.InProgress && model.CurrentPlayer.Type == PlayerType.Human;
        bool rolling = model.RollsUsed is > 0 and < GameModel.MaxRolls;

        status.Text = !model.IsStarted ? "Click New game to start."
            : model.IsComplete ? "Game over."
            : $"{model.CurrentPlayer.Name}'s turn - rolls left: {model.RollsLeft}";
        rollButton.Enabled = !busy && humanTurn && model.RollsLeft > 0;
        scoreButton.Enabled = !busy && humanTurn && model.RollsUsed > 0;
        saveButton.Enabled = !busy && model.InProgress;
        newButton.Enabled = !busy;
        loadButton.Enabled = !busy;

        for (int i = 0; i < dice.Length; i++)
        {
            bool held = model.Held[i];
            dice[i].Checked = held;
            string value = model.RollsUsed == 0 ? "-" : DieFace(model.Dice[i]);
            dice[i].Text = rolling ? $"{value}\n{(held ? "HELD" : "roll")}" : value;
        }

        // Columns: Category, one per player, Preview. Rebuilt only when the player count changes.
        int columns = model.Players.Count + 2;
        if (model.IsStarted && grid.ColumnCount != columns)
        {
            grid.Columns.Clear();
            grid.Rows.Clear();
            grid.ColumnCount = columns;
            foreach (DataGridViewColumn column in grid.Columns) column.SortMode = DataGridViewColumnSortMode.NotSortable;
            grid.Rows.Add(14);
            for (int r = 0; r < 13; r++) grid.Rows[r].Cells[0].Value = (ScoreCategory)r;
            grid.Rows[13].Cells[0].Value = "Total";
        }
        if (!model.IsStarted) return;

        for (int p = 0; p < model.Players.Count; p++)
            grid.Columns[p + 1].HeaderText = (p == model.CurrentIndex && model.InProgress ? "> " : "") + model.Players[p].Name;
        grid.Columns[0].HeaderText = "Category";
        grid.Columns[columns - 1].HeaderText = "Preview";
        for (int r = 0; r < 13; r++)
        {
            var category = (ScoreCategory)r;
            for (int p = 0; p < model.Players.Count; p++)
                grid.Rows[r].Cells[p + 1].Value = model.Players[p].Card.Scores[r];
            bool canPreview = model.InProgress && model.RollsUsed > 0 && !model.CurrentPlayer.Card.IsUsed(category);
            grid.Rows[r].Cells[columns - 1].Value = canPreview ? model.Preview(category) : null;
        }
        for (int p = 0; p < model.Players.Count; p++)
            grid.Rows[13].Cells[p + 1].Value = model.Players[p].Card.Total;
    }
}
