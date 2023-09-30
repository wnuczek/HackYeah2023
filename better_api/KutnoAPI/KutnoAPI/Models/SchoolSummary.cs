using System.Text.Json.Serialization;

namespace KutnoAPI.Models
{
    public class SchoolSummary
    {
        [JsonPropertyName("schoolrspo")]
        public long SchoolRSPO { get; set; }
        public int Year { get; set; }
        public decimal StudentsQuantity { get; set; }
        public decimal StudentsFromCountryQuantity { get; set; }
        public decimal StudentsFromSmallTownQuantity { get; set; }
        public decimal StudentsOutsideSchool { get; set; }
    }
}
