using System.Text.Json;
using System.Text.Json.Serialization;
using Yahtzee.Core;
namespace Yahtzee.Storage;

// Save file = one JSON document holding a GameSnapshot.
public static class GameFiles
{
    private static readonly JsonSerializerOptions Options = new()
    {
        WriteIndented = true,
        RespectNullableAnnotations = true,
        Converters = { new JsonStringEnumConverter() },
    };

    public static void Save(string path, GameSnapshot snapshot) =>
        File.WriteAllText(path, JsonSerializer.Serialize(snapshot, Options));

    public static GameSnapshot Load(string path) =>
        JsonSerializer.Deserialize<GameSnapshot>(File.ReadAllText(path), Options)
        ?? throw new JsonException("The file is empty.");
}
