using System.Text.Json.Serialization;

namespace KutnoAPI.Models
{
    public class JobSummary
    {
        [JsonPropertyName("schoolrspo")]
        public long SchoolRspo { get; set; }

        [JsonPropertyName("year")]
        public int Year { get; set; }

        [JsonPropertyName("nopromotiongradedquantity")]
        public decimal NoPromotionGradedQuantity { get; set; }

        [JsonPropertyName("nominatedquantity")]
        public decimal NominatedQuantity { get; set; }

        [JsonPropertyName("promotiongradedquantity")]
        public decimal PromotionGradedQuantity { get; set; }
    }
}
