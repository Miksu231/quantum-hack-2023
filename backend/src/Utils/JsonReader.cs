using System.Text.Json;
using QuantumHack.Models;

namespace QuantumHack.Utils;

public static class JsonReader
{
    public static Graph ReadGraphFromFile()
    {
        string jsonString = File.ReadAllText("./graphDoc.json");
        var graph = JsonSerializer.Deserialize<Graph>(jsonString);
        return graph!;
    }
}