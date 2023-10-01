using System.Text.Json.Serialization;

namespace KutnoAPI.Models
{
    public class SchoolSummary
    {
        [JsonPropertyName("schoolrspo")]
        public long SchoolRSPO { get; set; }

        [JsonPropertyName("year")]
        public int Year { get; set; }

        [JsonPropertyName("studentsquantity")]
        public decimal StudentsQuantity { get; set; }

        [JsonPropertyName("studentsfromcountryquantity")]
        public decimal StudentsFromCountryQuantity { get; set; }

        [JsonPropertyName("studentsfromsmalltownquantity")]
        public decimal StudentsFromSmallTownQuantity { get; set; }

        [JsonPropertyName("studentsqutsideschool")]
        public decimal StudentsOutsideSchool { get; set; }
    }
}
