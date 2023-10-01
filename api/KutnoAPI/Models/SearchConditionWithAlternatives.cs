using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace KutnoAPI.Models
{
    [BindProperties]
    public class SearchConditionWithAlternatives : SearchCondition
    {
        [JsonPropertyName("alternativeconditions")]
        public List<SearchCondition> AlternativeConditions { get; set; } = new List<SearchCondition>();

    }
}