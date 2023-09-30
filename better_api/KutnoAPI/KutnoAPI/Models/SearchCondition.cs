using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace KutnoAPI.Models
{
    [BindProperties]
    public class SearchCondition : ISearchCondition
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

        internal string eOperatorString()
        {
            switch (Operator)
            {
                case eOperators.LIKE: return "LIKE";
                case eOperators.EQUAL: return "=";
                case eOperators.GREATER: return ">";
                case eOperators.GREATER_EQUAL: return ">=";
                case eOperators.LOWER: return "<";
                case eOperators.LOWER_EQUAL: return "<=";
                case eOperators.IN: return " IN";
                case eOperators.ILIKE: return " ILIKE";
                default: return "=";
            }
        }
    }
}