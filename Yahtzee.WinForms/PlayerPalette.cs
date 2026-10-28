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
