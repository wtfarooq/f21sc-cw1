using Yahtzee.Core;
namespace Yahtzee.Tests;

[TestClass]
public class ScoringTests
{
    [TestMethod]
    [DataRow(ScoreCategory.Aces, new[] { 1, 1, 1, 2, 3 }, 3)]
    [DataRow(ScoreCategory.Twos, new[] { 2, 2, 3, 3, 4 }, 4)]
    [DataRow(ScoreCategory.Threes, new[] { 3, 3, 3, 3, 4 }, 12)]
    [DataRow(ScoreCategory.Fours, new[] { 4, 4, 1, 1, 1 }, 8)]
    [DataRow(ScoreCategory.Fives, new[] { 5, 1, 1, 1, 1 }, 5)]
    [DataRow(ScoreCategory.Sixes, new[] { 6, 6, 6, 1, 1 }, 18)]
    [DataRow(ScoreCategory.Sixes, new[] { 1, 2, 3, 4, 5 }, 0)]
    [DataRow(ScoreCategory.Chance, new[] { 1, 2, 3, 4, 4 }, 14)]
    [DataRow(ScoreCategory.ThreeOfAKind, new[] { 3, 3, 3, 4, 4 }, 17)]
    [DataRow(ScoreCategory.ThreeOfAKind, new[] { 3, 3, 4, 4, 5 }, 0)]
    [DataRow(ScoreCategory.ThreeOfAKind, new[] { 6, 6, 6, 6, 6 }, 30)]
    [DataRow(ScoreCategory.FourOfAKind, new[] { 6, 6, 6, 6, 0 + 1 }, 25)]
    [DataRow(ScoreCategory.FourOfAKind, new[] { 6, 6, 6, 1, 1 }, 0)]
    [DataRow(ScoreCategory.FullHouse, new[] { 2, 2, 3, 3, 3 }, 25)]
    [DataRow(ScoreCategory.FullHouse, new[] { 2, 2, 3, 3, 4 }, 0)]
    [DataRow(ScoreCategory.FullHouse, new[] { 4, 4, 4, 4, 4 }, 0)]   // a Yahtzee is not a full house
    [DataRow(ScoreCategory.SmallStraight, new[] { 1, 2, 3, 4, 6 }, 30)]
    [DataRow(ScoreCategory.SmallStraight, new[] { 2, 3, 4, 5, 5 }, 30)]
    [DataRow(ScoreCategory.SmallStraight, new[] { 3, 4, 5, 6, 1 }, 30)]
    [DataRow(ScoreCategory.SmallStraight, new[] { 1, 2, 3, 5, 6 }, 0)]
    [DataRow(ScoreCategory.LargeStraight, new[] { 1, 2, 3, 4, 5 }, 40)]
    [DataRow(ScoreCategory.LargeStraight, new[] { 6, 5, 4, 3, 2 }, 40)]
    [DataRow(ScoreCategory.LargeStraight, new[] { 1, 2, 3, 4, 6 }, 0)]
    [DataRow(ScoreCategory.Yahtzee, new[] { 5, 5, 5, 5, 5 }, 50)]
    [DataRow(ScoreCategory.Yahtzee, new[] { 5, 5, 5, 5, 4 }, 0)]
    public void ScoresCategory(ScoreCategory category, int[] dice, int expected) =>
        Assert.AreEqual(expected, Scoring.Score(category, dice));

    [TestMethod]
    public void RejectsInvalidDice()
    {
        Assert.ThrowsExactly<ArgumentException>(() => Scoring.Score(ScoreCategory.Chance, [1, 2, 3]));
        Assert.ThrowsExactly<ArgumentException>(() => Scoring.Score(ScoreCategory.Chance, [1, 2, 3, 4, 7]));
    }
}
