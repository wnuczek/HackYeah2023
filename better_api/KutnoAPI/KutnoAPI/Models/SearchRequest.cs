using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace KutnoAPI.Models
{
    public class SearchRequest
    {
        protected List<SearchCondition> _conditions = new List<SearchCondition>();
        [JsonPropertyName("offset")]
        public long Offset { get; set; } = 0;

        [JsonPropertyName("limit")]
        public long Limit { get; set; } = 100;

        [JsonPropertyName("sortingcolumn")]
        public string SortingColumn { get; set; } = "";

        [JsonPropertyName("sortingdirection")]
        public SortingDirection SortingDirection { get; set; } = SortingDirection.ASCENDING;

        [JsonPropertyName("conditions")]
        public List<SearchCondition> Conditions
        {
            get
            {
                return _conditions;
            }
            set
            {
                _conditions = value;
            }
        }
    }
}
