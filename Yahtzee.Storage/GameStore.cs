namespace Yahtzee.Storage;

public sealed record HighScore(string Player, int Score, DateTime PlayedAt);

public static class GameStore
{
    public static void Record(string path, DateTime playedAt, IEnumerable<(string Name, int Score)> results)
    {
        using var db = Open(path);
        var game = new GameRow { PlayedAt = playedAt };
        db.Games.Add(game);
        foreach (var (name, score) in results)
            db.Scores.Add(new ScoreRow { PlayerName = name, Score = score, Game = game });
        db.SaveChanges();
    }

    // Top scores overall, or only for one player when `player` is given.
    public static List<HighScore> TopScores(string path, string? player = null, int count = 5)
    {
        using var db = Open(path);
        return db.Scores
            .Where(s => player == null || s.PlayerName == player)
            .OrderByDescending(s => s.Score).ThenBy(s => s.Game!.PlayedAt)
            .Take(count)
            .Select(s => new HighScore(s.PlayerName, s.Score, s.Game!.PlayedAt))
            .ToList();
    }

    private static GameDb Open(string path)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(Path.GetFullPath(path))!);
        var db = new GameDb(path);
        db.Database.EnsureCreated();
        return db;
    }
}
