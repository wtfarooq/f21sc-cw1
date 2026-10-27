namespace Yahtzee.WinForms;

partial class MainForm
{
    private Label status = null!;
    private Label message = null!;
    private FlowLayoutPanel buttons = null!;
    private FlowLayoutPanel dicePanel = null!;
    private CheckBox dice0 = null!;
    private CheckBox dice1 = null!;
    private CheckBox dice2 = null!;
    private CheckBox dice3 = null!;
    private CheckBox dice4 = null!;
    private CheckBox[] dice = null!;
    private Button newButton = null!;
    private Button rollButton = null!;
    private Button scoreButton = null!;
    private Button saveButton = null!;
    private Button loadButton = null!;
    private Button historyButton = null!;
    private DataGridView grid = null!;

    private void InitializeComponent()
    {
        grid = new DataGridView();
        message = new Label();
        dicePanel = new FlowLayoutPanel();
        dice0 = new CheckBox();
        dice1 = new CheckBox();
        dice2 = new CheckBox();
        dice3 = new CheckBox();
        dice4 = new CheckBox();
        newButton = new Button();
        rollButton = new Button();
        scoreButton = new Button();
        saveButton = new Button();
        loadButton = new Button();
        historyButton = new Button();
        buttons = new FlowLayoutPanel();
        status = new Label();
        ((System.ComponentModel.ISupportInitialize)grid).BeginInit();
        dicePanel.SuspendLayout();
        buttons.SuspendLayout();
        SuspendLayout();
        // 
        // grid
        // 
        grid.AllowUserToAddRows = false;
        grid.AllowUserToResizeRows = false;
        grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        grid.ColumnHeadersHeight = 34;
        grid.Dock = DockStyle.Fill;
        grid.Location = new Point(0, 162);
        grid.MultiSelect = false;
        grid.Name = "grid";
        grid.ReadOnly = true;
        grid.RowHeadersVisible = false;
        grid.RowHeadersWidth = 62;
        grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        grid.Size = new Size(760, 450);
        grid.TabIndex = 0;
        // 
        // message
        // 
        message.Dock = DockStyle.Bottom;
        message.Location = new Point(0, 612);
        message.Name = "message";
        message.Size = new Size(760, 28);
        message.TabIndex = 1;
        message.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // dicePanel
        // 
        dicePanel.Controls.Add(dice0);
        dicePanel.Controls.Add(dice1);
        dicePanel.Controls.Add(dice2);
        dicePanel.Controls.Add(dice3);
        dicePanel.Controls.Add(dice4);
        dicePanel.Dock = DockStyle.Top;
        dicePanel.Location = new Point(0, 72);
        dicePanel.Name = "dicePanel";
        dicePanel.Padding = new Padding(8);
        dicePanel.Size = new Size(760, 90);
        dicePanel.TabIndex = 2;
        // 
        // dice0
        // 
        dice0.Appearance = Appearance.Button;
        dice0.AutoCheck = false;
        dice0.Font = new Font("Segoe UI Symbol", 22F, FontStyle.Bold);
        dice0.Location = new Point(11, 11);
        dice0.Name = "dice0";
        dice0.Size = new Size(80, 70);
        dice0.TabIndex = 0;
        dice0.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // dice1
        // 
        dice1.Appearance = Appearance.Button;
        dice1.AutoCheck = false;
        dice1.Font = new Font("Segoe UI Symbol", 22F, FontStyle.Bold);
        dice1.Location = new Point(97, 11);
        dice1.Name = "dice1";
        dice1.Size = new Size(80, 70);
        dice1.TabIndex = 1;
        dice1.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // dice2
        // 
        dice2.Appearance = Appearance.Button;
        dice2.AutoCheck = false;
        dice2.Font = new Font("Segoe UI Symbol", 22F, FontStyle.Bold);
        dice2.Location = new Point(183, 11);
        dice2.Name = "dice2";
        dice2.Size = new Size(80, 70);
        dice2.TabIndex = 2;
        dice2.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // dice3
        // 
        dice3.Appearance = Appearance.Button;
        dice3.AutoCheck = false;
        dice3.Font = new Font("Segoe UI Symbol", 22F, FontStyle.Bold);
        dice3.Location = new Point(269, 11);
        dice3.Name = "dice3";
        dice3.Size = new Size(80, 70);
        dice3.TabIndex = 3;
        dice3.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // dice4
        // 
        dice4.Appearance = Appearance.Button;
        dice4.AutoCheck = false;
        dice4.Font = new Font("Segoe UI Symbol", 22F, FontStyle.Bold);
        dice4.Location = new Point(355, 11);
        dice4.Name = "dice4";
        dice4.Size = new Size(80, 70);
        dice4.TabIndex = 4;
        dice4.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // newButton
        // 
        newButton.AutoSize = true;
        newButton.Location = new Point(7, 7);
        newButton.Name = "newButton";
        newButton.Size = new Size(107, 35);
        newButton.TabIndex = 0;
        newButton.Text = "New game";
        // 
        // rollButton
        // 
        rollButton.AutoSize = true;
        rollButton.Location = new Point(120, 7);
        rollButton.Name = "rollButton";
        rollButton.Size = new Size(75, 35);
        rollButton.TabIndex = 1;
        rollButton.Text = "Roll";
        // 
        // scoreButton
        // 
        scoreButton.AutoSize = true;
        scoreButton.Location = new Point(201, 7);
        scoreButton.Name = "scoreButton";
        scoreButton.Size = new Size(209, 35);
        scoreButton.TabIndex = 2;
        scoreButton.Text = "Score selected category";
        // 
        // saveButton
        // 
        saveButton.AutoSize = true;
        saveButton.Location = new Point(416, 7);
        saveButton.Name = "saveButton";
        saveButton.Size = new Size(75, 35);
        saveButton.TabIndex = 3;
        saveButton.Text = "Save";
        // 
        // loadButton
        // 
        loadButton.AutoSize = true;
        loadButton.Location = new Point(497, 7);
        loadButton.Name = "loadButton";
        loadButton.Size = new Size(75, 35);
        loadButton.TabIndex = 4;
        loadButton.Text = "Load";
        // 
        // historyButton
        // 
        historyButton.AutoSize = true;
        historyButton.Location = new Point(578, 7);
        historyButton.Name = "historyButton";
        historyButton.Size = new Size(115, 35);
        historyButton.TabIndex = 5;
        historyButton.Text = "High scores";
        // 
        // buttons
        // 
        buttons.Controls.Add(newButton);
        buttons.Controls.Add(rollButton);
        buttons.Controls.Add(scoreButton);
        buttons.Controls.Add(saveButton);
        buttons.Controls.Add(loadButton);
        buttons.Controls.Add(historyButton);
        buttons.Dock = DockStyle.Top;
        buttons.Location = new Point(0, 32);
        buttons.Name = "buttons";
        buttons.Padding = new Padding(4);
        buttons.Size = new Size(760, 40);
        buttons.TabIndex = 3;
        // 
        // status
        // 
        status.Dock = DockStyle.Top;
        status.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        status.Location = new Point(0, 0);
        status.Name = "status";
        status.Size = new Size(760, 32);
        status.TabIndex = 4;
        status.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // MainForm
        // 
        ClientSize = new Size(760, 640);
        Controls.Add(grid);
        Controls.Add(message);
        Controls.Add(dicePanel);
        Controls.Add(buttons);
        Controls.Add(status);
        Name = "MainForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Yahtzee";
        ((System.ComponentModel.ISupportInitialize)grid).EndInit();
        dicePanel.ResumeLayout(false);
        buttons.ResumeLayout(false);
        buttons.PerformLayout();
        ResumeLayout(false);
    }
}
