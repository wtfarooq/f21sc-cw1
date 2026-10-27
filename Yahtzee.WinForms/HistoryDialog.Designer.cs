namespace Yahtzee.WinForms;

partial class HistoryDialog
{
    private TextBox player = null!;
    private Button show = null!;
    private ListBox results = null!;
    private Label promptLabel = null!;

    private void InitializeComponent()
    {
        player = new TextBox();
        show = new Button();
        results = new ListBox();
        promptLabel = new Label();
        SuspendLayout();
        //
        // player
        //
        player.Location = new Point(110, 12);
        player.Width = 160;
        //
        // show
        //
        show.Location = new Point(280, 10);
        show.Text = "Show";
        //
        // results
        //
        results.Font = new Font("Consolas", 10);
        results.Location = new Point(12, 45);
        results.Size = new Size(430, 150);
        //
        // promptLabel
        //
        promptLabel.AutoSize = true;
        promptLabel.Location = new Point(12, 15);
        promptLabel.Text = "Player (blank = all)";
        Controls.Add(promptLabel);
        Controls.Add(player);
        Controls.Add(show);
        Controls.Add(results);

        Text = "Top 5 scores";
        ClientSize = new Size(454, 210);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        StartPosition = FormStartPosition.CenterParent;
        AcceptButton = show;
        ResumeLayout(false);
        PerformLayout();
    }
}
