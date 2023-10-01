using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace KutnoAPI.Models
{
    public interface ISearchCondition
    {
        [JsonPropertyName("value")]
        [Required(AllowEmptyStrings = false)]
        public string Value { get; set; }

        [JsonPropertyName("operator")]
        [Required]
        public eOperators Operator { get; set; }

        [JsonPropertyName("column")]
        [Required(AllowEmptyStrings = false)]
        public string Column { get; set; }
    }
    public enum eOperators
    {
        LIKE,
        EQUAL,
        GREATER,
        GREATER_EQUAL,
        LOWER,
        LOWER_EQUAL,
        IN,
        ILIKE
    }



}