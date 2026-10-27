using Yahtzee.Core;
namespace Yahtzee.WinForms;

internal static class PlayerPalette
{
    public static IReadOnlyList<Color> Colors { get; } =
    [
        Color.FromArgb(75, 127, 235),
        Color.FromArgb(232, 74, 95),
        Color.FromArgb(80, 184, 139),
        Color.FromArgb(243, 163, 47),
    ];

    public static Color Tint(int playerIndex)
    {
        Color color = Colors[playerIndex];
        return Color.FromArgb(
            (color.R + 255) / 2,
            (color.G + 255) / 2,
            (color.B + 255) / 2);
    }
}

// Asks for the number of players, their names and whether each is human or computer.
public partial class SetupDialog : Form
{
    public SetupDialog()
    {
        InitializeComponent();
        names = [name0, name1, name2, name3];
        types = [type0, type1, type2, type3];

        for (int i = 0; i < 4; i++)
        {
            // Player 1 defaults to Human, the rest to Computer bots.
            int index = i;
            types[i].DataSource = Enum.GetValues<PlayerType>();
            types[i].SelectedItem = index == 0 ? PlayerType.Human : PlayerType.Computer;
            names[i].Text = index == 0 ? "Player 1" : $"Bot {index + 1}";
            types[i].SelectedIndexChanged += (_, _) => AutoName(index);
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

    public List<(string Name, PlayerType Type)> Players =>
        Enumerable.Range(0, (int)count.Value).Select(i => (names[i].Text, (PlayerType)types[i].SelectedItem!)).ToList();

    private void UpdateRows()
    {
        for (int i = 0; i < 4; i++)
        {
            names[i].Enabled = i < count.Value;
            types[i].Enabled = i < count.Value;
        }
    }
}
