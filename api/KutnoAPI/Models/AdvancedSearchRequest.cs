using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace KutnoAPI.Models
{
    public class AdvancedSearchRequest : SearchRequest
    {
        private List<SearchConditionWithAlternatives> _conditions = new List<SearchConditionWithAlternatives>();
        [JsonPropertyName("conditions")]
        new public List<SearchConditionWithAlternatives> Conditions
        {
            get
            {
                return _conditions;
            }
            set
            {
                _conditions = value;
                base.Conditions = _conditions.Select(c => (SearchCondition)c).ToList();
            }
        }
    }
}
