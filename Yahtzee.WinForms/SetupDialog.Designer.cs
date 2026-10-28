namespace Yahtzee.WinForms;

partial class SetupDialog
{
    private NumericUpDown count = null!;
    private TextBox[] names = null!;
    private ComboBox[] types = null!;
    private ComboBox[] strategies = null!;
    private TextBox name0 = null!;
    private TextBox name1 = null!;
    private TextBox name2 = null!;
    private TextBox name3 = null!;
    private ComboBox type0 = null!;
    private ComboBox type1 = null!;
    private ComboBox type2 = null!;
    private ComboBox type3 = null!;
    private ComboBox strategy0 = null!;
    private ComboBox strategy1 = null!;
    private ComboBox strategy2 = null!;
    private ComboBox strategy3 = null!;
    private Label dot0 = null!;
    private Label dot1 = null!;
    private Label dot2 = null!;
    private Label dot3 = null!;
    private Label titleLabel = null!;
    private Button ok = null!;
    private Button cancel = null!;

    private void InitializeComponent()
    {
        count = new NumericUpDown();
        dot0 = new Label();
        dot1 = new Label();
        dot2 = new Label();
        dot3 = new Label();
        name0 = new TextBox();
        name1 = new TextBox();
        name2 = new TextBox();
        name3 = new TextBox();
        type0 = new ComboBox();
        type1 = new ComboBox();
        type2 = new ComboBox();
        type3 = new ComboBox();
        strategy0 = new ComboBox();
        strategy1 = new ComboBox();
        strategy2 = new ComboBox();
        strategy3 = new ComboBox();
        titleLabel = new Label();
        ok = new Button();
        cancel = new Button();
        ((System.ComponentModel.ISupportInitialize)count).BeginInit();
        SuspendLayout();
        // 
        // count
        // 
        count.Location = new Point(465, 37);
        count.Maximum = new decimal(new int[] { 4, 0, 0, 0 });
        count.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        count.Name = "count";
        count.Size = new Size(60, 31);
        count.TabIndex = 1;
        count.Value = new decimal(new int[] { 1, 0, 0, 0 });
        // 
        // dot0
        // 
        dot0.Font = new Font("Segoe UI", 18F);
        dot0.ForeColor = PlayerPalette.Colors[0];
        dot0.Location = new Point(36, 91);
        dot0.Name = "dot0";
        dot0.Size = new Size(36, 48);
        dot0.TabIndex = 2;
        dot0.Text = "●";
        dot0.TextAlign = ContentAlignment.MiddleCenter;
        dot0.UseCompatibleTextRendering = true;
        // 
        // dot1
        // 
        dot1.Font = new Font("Segoe UI", 18F);
        dot1.ForeColor = PlayerPalette.Colors[1];
        dot1.Location = new Point(36, 128);
        dot1.Name = "dot1";
        dot1.Size = new Size(36, 48);
        dot1.TabIndex = 5;
        dot1.Text = "●";
        dot1.TextAlign = ContentAlignment.MiddleCenter;
        dot1.UseCompatibleTextRendering = true;
        // 
        // dot2
        // 
        dot2.Font = new Font("Segoe UI", 18F);
        dot2.ForeColor = PlayerPalette.Colors[2];
        dot2.Location = new Point(36, 165);
        dot2.Name = "dot2";
        dot2.Size = new Size(36, 48);
        dot2.TabIndex = 8;
        dot2.Text = "●";
        dot2.TextAlign = ContentAlignment.MiddleCenter;
        dot2.UseCompatibleTextRendering = true;
        // 
        // dot3
        // 
        dot3.Font = new Font("Segoe UI", 18F);
        dot3.ForeColor = PlayerPalette.Colors[3];
        dot3.Location = new Point(36, 208);
        dot3.Name = "dot3";
        dot3.Size = new Size(36, 48);
        dot3.TabIndex = 11;
        dot3.Text = "●";
        dot3.TextAlign = ContentAlignment.MiddleCenter;
        dot3.UseCompatibleTextRendering = true;
        // 
        // name0
        // 
        name0.Location = new Point(79, 100);
        name0.Name = "name0";
        name0.Size = new Size(160, 31);
        name0.TabIndex = 3;
        name0.Text = "Player 1";
        // 
        // name1
        // 
        name1.Location = new Point(79, 137);
        name1.Name = "name1";
        name1.Size = new Size(160, 31);
        name1.TabIndex = 6;
        name1.Text = "Player 2";
        // 
        // name2
        // 
        name2.Location = new Point(79, 174);
        name2.Name = "name2";
        name2.Size = new Size(160, 31);
        name2.TabIndex = 9;
        name2.Text = "Player 3";
        // 
        // name3
        // 
        name3.Location = new Point(79, 217);
        name3.Name = "name3";
        name3.Size = new Size(160, 31);
        name3.TabIndex = 12;
        name3.Text = "Player 4";
        // 
        // type0
        // 
        type0.DropDownStyle = ComboBoxStyle.DropDownList;
        type0.Location = new Point(245, 98);
        type0.Name = "type0";
        type0.Size = new Size(149, 33);
        type0.TabIndex = 4;
        // 
        // type1
        // 
        type1.DropDownStyle = ComboBoxStyle.DropDownList;
        type1.Location = new Point(245, 137);
        type1.Name = "type1";
        type1.Size = new Size(149, 33);
        type1.TabIndex = 7;
        // 
        // type2
        // 
        type2.DropDownStyle = ComboBoxStyle.DropDownList;
        type2.Location = new Point(245, 176);
        type2.Name = "type2";
        type2.Size = new Size(149, 33);
        type2.TabIndex = 10;
        // 
        // type3
        // 
        type3.DropDownStyle = ComboBoxStyle.DropDownList;
        type3.Location = new Point(245, 215);
        type3.Name = "type3";
        type3.Size = new Size(149, 33);
        type3.TabIndex = 13;
        // 
        // strategy0
        // 
        strategy0.DropDownStyle = ComboBoxStyle.DropDownList;
        strategy0.Location = new Point(400, 100);
        strategy0.Name = "strategy0";
        strategy0.Size = new Size(125, 33);
        strategy0.TabIndex = 14;
        // 
        // strategy1
        // 
        strategy1.DropDownStyle = ComboBoxStyle.DropDownList;
        strategy1.Location = new Point(400, 137);
        strategy1.Name = "strategy1";
        strategy1.Size = new Size(125, 33);
        strategy1.TabIndex = 15;
        // 
        // strategy2
        // 
        strategy2.DropDownStyle = ComboBoxStyle.DropDownList;
        strategy2.Location = new Point(400, 176);
        strategy2.Name = "strategy2";
        strategy2.Size = new Size(125, 33);
        strategy2.TabIndex = 16;
        // 
        // strategy3
        // 
        strategy3.DropDownStyle = ComboBoxStyle.DropDownList;
        strategy3.Location = new Point(400, 215);
        strategy3.Name = "strategy3";
        strategy3.Size = new Size(125, 33);
        strategy3.TabIndex = 17;
        // 
        // titleLabel
        // 
        titleLabel.AutoSize = true;
        titleLabel.Location = new Point(31, 37);
        titleLabel.Name = "titleLabel";
        titleLabel.Size = new Size(109, 25);
        titleLabel.TabIndex = 0;
        titleLabel.Text = "Players (1-4)";
        // 
        // ok
        // 
        ok.DialogResult = DialogResult.OK;
        ok.Location = new Point(425, 274);
        ok.Name = "ok";
        ok.Size = new Size(100, 34);
        ok.TabIndex = 18;
        ok.Text = "Start";
        // 
        // cancel
        // 
        cancel.DialogResult = DialogResult.Cancel;
        cancel.Location = new Point(319, 274);
        cancel.Name = "cancel";
        cancel.Size = new Size(100, 34);
        cancel.TabIndex = 19;
        cancel.Text = "Cancel";
        // 
        // SetupDialog
        // 
        AcceptButton = ok;
        CancelButton = cancel;
        ClientSize = new Size(571, 354);
        Controls.Add(titleLabel);
        Controls.Add(count);
        Controls.Add(dot0);
        Controls.Add(name0);
        Controls.Add(type0);
        Controls.Add(strategy0);
        Controls.Add(dot1);
        Controls.Add(name1);
        Controls.Add(type1);
        Controls.Add(strategy1);
        Controls.Add(dot2);
        Controls.Add(name2);
        Controls.Add(type2);
        Controls.Add(strategy2);
        Controls.Add(dot3);
        Controls.Add(name3);
        Controls.Add(type3);
        Controls.Add(strategy3);
        Controls.Add(ok);
        Controls.Add(cancel);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "SetupDialog";
        StartPosition = FormStartPosition.CenterParent;
        Text = "New game";
        ((System.ComponentModel.ISupportInitialize)count).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }
}
