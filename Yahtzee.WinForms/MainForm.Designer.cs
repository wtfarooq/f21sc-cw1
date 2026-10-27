namespace Yahtzee.WinForms;

partial class MainForm
{
    private Label status = null!;
    private Label message = null!;
    private FlowLayoutPanel dicePanel = null!;
    private CheckBox dice0 = null!;
    private CheckBox dice1 = null!;
    private CheckBox dice2 = null!;
    private CheckBox dice3 = null!;
    private CheckBox dice4 = null!;
    private CheckBox[] dice = null!;
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
        status = new Label();
        historyButton = new Button();
        loadButton = new Button();
        saveButton = new Button();
        scoreButton = new Button();
        rollButton = new Button();
        newButton = new Button();
        buttons = new FlowLayoutPanel();
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
        grid.Location = new Point(0, 172);
        grid.MultiSelect = false;
        grid.Name = "grid";
        grid.ReadOnly = true;
        grid.RowHeadersVisible = false;
        grid.RowHeadersWidth = 62;
        grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        grid.Size = new Size(759, 596);
        grid.TabIndex = 0;
        // 
        // message
        // 
        message.Dock = DockStyle.Bottom;
        message.Location = new Point(0, 768);
        message.Name = "message";
        message.Size = new Size(759, 28);
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
        dicePanel.Location = new Point(0, 82);
        dicePanel.Name = "dicePanel";
        dicePanel.Padding = new Padding(8);
        dicePanel.Size = new Size(759, 90);
        dicePanel.TabIndex = 2;
        // 
        // dice0
        // 
        dice0.Appearance = Appearance.Button;
        dice0.AutoCheck = false;
        dice0.Font = new Font("Segoe UI Symbol", 32F, FontStyle.Bold);
        dice0.Location = new Point(8, 8);
        dice0.Margin = new Padding(0);
        dice0.Name = "dice0";
        dice0.Size = new Size(80, 70);
        dice0.TabIndex = 0;
        dice0.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // dice1
        // 
        dice1.Appearance = Appearance.Button;
        dice1.AutoCheck = false;
        dice1.Font = new Font("Segoe UI Symbol", 32F, FontStyle.Bold);
        dice1.Location = new Point(88, 8);
        dice1.Margin = new Padding(0);
        dice1.Name = "dice1";
        dice1.Size = new Size(80, 70);
        dice1.TabIndex = 1;
        dice1.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // dice2
        // 
        dice2.Appearance = Appearance.Button;
        dice2.AutoCheck = false;
        dice2.Font = new Font("Segoe UI Symbol", 32F, FontStyle.Bold);
        dice2.Location = new Point(168, 8);
        dice2.Margin = new Padding(0);
        dice2.Name = "dice2";
        dice2.Size = new Size(80, 70);
        dice2.TabIndex = 2;
        dice2.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // dice3
        // 
        dice3.Appearance = Appearance.Button;
        dice3.AutoCheck = false;
        dice3.Font = new Font("Segoe UI Symbol", 32F, FontStyle.Bold);
        dice3.Location = new Point(248, 8);
        dice3.Margin = new Padding(0);
        dice3.Name = "dice3";
        dice3.Size = new Size(80, 70);
        dice3.TabIndex = 3;
        dice3.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // dice4
        // 
        dice4.Appearance = Appearance.Button;
        dice4.AutoCheck = false;
        dice4.Font = new Font("Segoe UI Symbol", 32F, FontStyle.Bold);
        dice4.Location = new Point(328, 8);
        dice4.Margin = new Padding(0);
        dice4.Name = "dice4";
        dice4.Size = new Size(80, 70);
        dice4.TabIndex = 4;
        dice4.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // status
        // 
        status.Dock = DockStyle.Top;
        status.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        status.Location = new Point(0, 0);
        status.Name = "status";
        status.Size = new Size(759, 32);
        status.TabIndex = 4;
        status.TextAlign = ContentAlignment.MiddleLeft;
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
        // loadButton
        // 
        loadButton.AutoSize = true;
        loadButton.Location = new Point(497, 7);
        loadButton.Name = "loadButton";
        loadButton.Size = new Size(75, 35);
        loadButton.TabIndex = 4;
        loadButton.Text = "Load";
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
        // scoreButton
        // 
        scoreButton.AutoSize = true;
        scoreButton.Location = new Point(201, 7);
        scoreButton.Name = "scoreButton";
        scoreButton.Size = new Size(209, 35);
        scoreButton.TabIndex = 2;
        scoreButton.Text = "Score selected category";
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
        // newButton
        // 
        newButton.AutoSize = true;
        newButton.Location = new Point(7, 7);
        newButton.Name = "newButton";
        newButton.Size = new Size(107, 35);
        newButton.TabIndex = 0;
        newButton.Text = "New game";
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
        buttons.Size = new Size(759, 50);
        buttons.TabIndex = 3;
        // 
        // MainForm
        // 
        ClientSize = new Size(759, 796);
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

    private Button historyButton;
    private Button loadButton;
    private Button saveButton;
    private Button scoreButton;
    private Button rollButton;
    private Button newButton;
    private FlowLayoutPanel buttons;
}
