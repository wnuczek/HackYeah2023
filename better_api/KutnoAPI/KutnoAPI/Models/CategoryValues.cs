using System.Text.Json.Serialization;

namespace KutnoAPI.Models;

public class CategoryValues
{
    [JsonPropertyName("schoolrspo")]
    public long SchoolRSPO { get; set; }
    [JsonPropertyName("categoryid")]
    public int CategoryId { get; set; }
    public string CategoryStr { get; set; }

    [JsonPropertyName("value")]
    public decimal Value { get; set; }
}
