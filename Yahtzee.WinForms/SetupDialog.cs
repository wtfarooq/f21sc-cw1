using Yahtzee.Core;
namespace Yahtzee.WinForms;

// Asks for the number of players, their names and whether each is human or computer.
public partial class SetupDialog : Form
{
    public SetupDialog()
    {
        InitializeComponent();
        names = [name0, name1, name2, name3];
        types = [type0, type1, type2, type3];
        strategies = [strategy0, strategy1, strategy2, strategy3];

        for (int i = 0; i < 4; i++)
        {
            // Player 1 defaults to Human, the rest to Computer bots.
            int index = i;
            types[i].DataSource = Enum.GetValues<PlayerType>();
            types[i].SelectedItem = index == 0 ? PlayerType.Human : PlayerType.Computer;
            strategies[i].DataSource = Enum.GetValues<AiStrategy>();
            names[i].Text = index == 0 ? "Player 1" : $"Bot {index + 1}";
            types[i].SelectedIndexChanged += (_, _) => { AutoName(index); UpdateRows(); };
        }
        count.ValueChanged += (_, _) => UpdateRows();
        UpdateRows();
    }

    // Switching to Computer fills in "Bot N"; switching back restores "Player N".
    private void AutoName(int i)
    {
        if ((PlayerType)types[i].SelectedItem! == PlayerType.Computer
            && (string.IsNullOrWhiteSpace(names[i].Text) || names[i].Text == $"Player {i + 1}"))
            names[i].Text = $"Bot {i + 1}";
        else if ((PlayerType)types[i].SelectedItem! == PlayerType.Human && names[i].Text == $"Bot {i + 1}")
            names[i].Text = $"Player {i + 1}";
    }

    public List<(string Name, PlayerType Type, AiStrategy? Strategy)> Players =>
        Enumerable.Range(0, (int)count.Value).Select(i =>
            (names[i].Text, (PlayerType)types[i].SelectedItem!,
                (PlayerType)types[i].SelectedItem! == PlayerType.Computer
                    ? (AiStrategy?)strategies[i].SelectedItem! : null)).ToList();

    private void UpdateRows()
    {
        for (int i = 0; i < 4; i++)
        {
            names[i].Enabled = i < count.Value;
            types[i].Enabled = i < count.Value;
            strategies[i].Enabled = i < count.Value && (PlayerType)types[i].SelectedItem! == PlayerType.Computer;
        }
    }
}
