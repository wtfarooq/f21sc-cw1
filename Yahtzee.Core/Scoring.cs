namespace Yahtzee.Core;

public enum ScoreCategory
{
    Aces, Twos, Threes, Fours, Fives, Sixes,
    Chance, ThreeOfAKind, FourOfAKind, FullHouse,
    SmallStraight, LargeStraight, Yahtzee
}

// Simplified Appendix B rules: a category that does not match scores 0.
public static class Scoring
{
    public static int Score(ScoreCategory category, IReadOnlyList<int> dice)
    {
        if (dice.Count != 5 || dice.Any(d => d < 1 || d > 6))
            throw new ArgumentException("Five dice with values 1-6 are required.", nameof(dice));

        var counts = new int[7]; // counts[face] = how many dice show face
        foreach (var d in dice) counts[d]++;
        int sum = dice.Sum();
        int most = counts.Max();

        return category switch
        {
            <= ScoreCategory.Sixes => counts[(int)category + 1] * ((int)category + 1),
            ScoreCategory.Chance => sum,
            ScoreCategory.ThreeOfAKind => most >= 3 ? sum : 0,
            ScoreCategory.FourOfAKind => most >= 4 ? sum : 0,
            ScoreCategory.FullHouse => most == 3 && counts.Contains(2) ? 25 : 0,
            ScoreCategory.SmallStraight => HasConsecutiveFaces(counts, 4) ? 30 : 0,
            ScoreCategory.LargeStraight => HasConsecutiveFaces(counts, 5) ? 40 : 0,
            ScoreCategory.Yahtzee => most == 5 ? 50 : 0,
            _ => throw new ArgumentOutOfRangeException(nameof(category)),
        };
    }

    // True if `length` consecutive faces all appear at least once.
    private static bool HasConsecutiveFaces(int[] counts, int length)
    {
        for (int start = 1; start + length - 1 <= 6; start++)
            if (Enumerable.Range(start, length).All(f => counts[f] > 0)) return true;
        return false;
    }
}
