namespace Yahtzee.WinForms;

// Shows the five highest scores overall, or for the player typed in.
public partial class HistoryDialog : Form
{
    private readonly GameController controller;

    public HistoryDialog(GameController controller)
    {
        this.controller = controller;
        InitializeComponent();
        show.Click += (_, _) => Show5();
        Shown += (_, _) => Show5();
    }

    private void Show5()
    {
        results.Items.Clear();
        try
        {
            var scores = controller.HighScores(player.Text);
            for (int i = 0; i < scores.Count; i++)
                results.Items.Add($"{i + 1}. {scores[i].Player,-15} {scores[i].Score,4}  {scores[i].PlayedAt:yyyy-MM-dd HH:mm}");
            if (scores.Count == 0) results.Items.Add("No games recorded.");
        }
        catch (Exception ex) when (ex is System.Data.Common.DbException or IOException)
        {
            results.Items.Add("Could not read the database: " + ex.Message);
        }
    }
}
