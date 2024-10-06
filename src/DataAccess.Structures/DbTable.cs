using System.Text.Json.Serialization;

namespace DataAccess.Structures
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum DbTable
    {
        NA,
        SudokuProblems
    }
}
