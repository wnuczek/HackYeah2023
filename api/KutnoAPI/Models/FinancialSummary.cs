using System.Text.Json.Serialization;

namespace KutnoAPI.Models
{
    public class FinancialSummary
    {
        [JsonPropertyName("regon")]
        public string Regon { get; set; }

        [JsonPropertyName("year")]
        public int Year { get; set; }

        [JsonPropertyName("yearpart")]
        public int YearPart { get; set; }

        [JsonPropertyName("department")]
        public string Department { get; set; }

        [JsonPropertyName("chapter")]
        public string Chapter { get; set; }

        [JsonPropertyName("paragraph")]
        public int Paragraph { get; set; }

        [JsonPropertyName("plannedincome")]
        public decimal PlannedIncome { get; set; }

        [JsonPropertyName("receivables")]
        public decimal Receivables { get; set; }

        [JsonPropertyName("madeincome")]
        public decimal MadeIncome { get; set; }

        [JsonPropertyName("plannedcost")]
        public decimal PlannedCost { get; set; }

        [JsonPropertyName("engagement")]
        public decimal Engagement { get; set; }

        [JsonPropertyName("madecost")]
        public decimal MadeCost { get; set; }
    }
}
