using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
namespace Yahtzee.Storage;

// One row per completed game, one row per player in that game.
public sealed class GameRow
{
    public int Id { get; set; }
    public DateTime PlayedAt { get; set; }
}
public sealed class ScoreRow
{
    public int Id { get; set; }
    public int GameId { get; set; }
    public string PlayerName { get; set; } = "";
    public int Score { get; set; }
    public GameRow? Game { get; set; }
}
public sealed class GameDb : DbContext
{
    private readonly string path;
    public GameDb(string path) => this.path = Path.GetFullPath(path);
    public DbSet<GameRow> Games => Set<GameRow>();
    public DbSet<ScoreRow> Scores => Set<ScoreRow>();
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        var connection = new SqliteConnectionStringBuilder { DataSource = path, Pooling = false };
        options.UseSqlite(connection.ToString());
    }
    protected override void OnModelCreating(ModelBuilder model) =>
        model.Entity<ScoreRow>().HasOne(s => s.Game).WithMany().HasForeignKey(s => s.GameId);
}
