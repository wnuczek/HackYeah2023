using System.Text.Json.Serialization;

namespace KutnoAPI.Models;

public class School
{
    [JsonPropertyName("rspo")]
    public int Rspo { get; set; }

    [JsonPropertyName("regon")]
    public string Regon { get; set; }

    [JsonPropertyName("schooltype")]
    public SchoolType SchoolType { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("address")]
    public string Address { get; set; }

    [JsonPropertyName("buildingnumber")]
    public string BuildingNumber { get; set; }

    [JsonPropertyName("flatnumber")]
    public string FlatNumber { get; set; }

    [JsonPropertyName("town")]
    public string Town { get; set; }

    [JsonPropertyName("postcode")]
    public string PostCode { get; set; }

    [JsonPropertyName("post")]
    public string Post { get; set; }

    [JsonPropertyName("owner")]
    public OwnerType OwnerType { get; set; }
    public List<CategoryValues> Categories { get; set; }

    public SchoolSummary Summary { get; set; }

}

