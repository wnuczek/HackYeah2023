using System.Text.Json.Serialization;

namespace KutnoAPI.Models;

public class CategoryDefinition
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("symbol")]
    public string Symbol { get; set; }

    [JsonPropertyName("year")]
    public int Year { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("factor")]
    public decimal Factor { get; set; }
}
