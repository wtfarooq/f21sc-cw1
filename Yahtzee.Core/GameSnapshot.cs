namespace Yahtzee.Core;

// Plain data used for the save file. GameModel validates it before use.
public sealed class PlayerSnapshot
{
    public string Name { get; set; } = "";
    public PlayerType Type { get; set; }
    public AiStrategy? Strategy { get; set; }
    public int?[] Scores { get; set; } = [];
}

public sealed class GameSnapshot
{
    public List<PlayerSnapshot> Players { get; set; } = [];
    public int CurrentPlayer { get; set; }
    public int RollsUsed { get; set; }
    public int[] Dice { get; set; } = [];
    public bool[] Held { get; set; } = [];
}
