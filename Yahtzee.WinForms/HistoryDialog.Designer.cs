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
        player.Location = new Point(259, 12);
        player.Name = "player";
        player.Size = new Size(160, 31);
        player.TabIndex = 1;
        // 
        // show
        // 
        show.Location = new Point(425, 12);
        show.Name = "show";
        show.Size = new Size(75, 31);
        show.TabIndex = 2;
        show.Text = "Show";
        // 
        // results
        // 
        results.Font = new Font("Consolas", 10F);
        results.Location = new Point(12, 48);
        results.Name = "results";
        results.Size = new Size(488, 211);
        results.TabIndex = 3;
        // 
        // promptLabel
        // 
        promptLabel.AutoSize = true;
        promptLabel.Location = new Point(12, 15);
        promptLabel.Name = "promptLabel";
        promptLabel.Size = new Size(156, 25);
        promptLabel.TabIndex = 0;
        promptLabel.Text = "Player (blank = all)";
        // 
        // HistoryDialog
        // 
        AcceptButton = show;
        ClientSize = new Size(512, 284);
        Controls.Add(promptLabel);
        Controls.Add(player);
        Controls.Add(show);
        Controls.Add(results);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "HistoryDialog";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Top 5 scores";
        ResumeLayout(false);
        PerformLayout();
    }
}
