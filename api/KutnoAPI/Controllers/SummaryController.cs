using Dapper;
using KutnoAPI.Extensions;
using KutnoAPI.Models;
using KutnoAPI.Parsers;
using KutnoAPI.Services;
using MesInternalApi.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Npgsql;

namespace KutnoAPI.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class SummaryController : ControllerBase
	{
		private string _connectionString = string.Empty;
		private string STUDENTS_BASE_QUERY = "SELECT * FROM pgj.school_summary";
        private string FINANCIAL_BASE_QUERY = "SELECT * FROM pgj.financial_summary";

        private const int RECORD_DEFAULT_LIMIT = 100;

        private readonly ILogger<SummaryController> _logger;

		public SummaryController(ILogger<SummaryController> logger, IConfiguration configuration)
		{
			_connectionString = configuration["DbString"];
			_logger = logger;
		}

        [HttpPost("students")]
        public async Task<ActionResult<IEnumerable<SchoolSummary>>> getStudentsSummary(SearchRequest searchRequest)
        {
            var query = string.Empty;
            var paramDict = new Dictionary<string, object>();
            paramDict.Add("@limit", searchRequest.Limit > 0 ? searchRequest.Limit : RECORD_DEFAULT_LIMIT);
            paramDict.Add("@offset", searchRequest.Offset > 0 ? searchRequest.Offset : 0);

            School temp = new School()
            {
            };
            var propertiesNames = temp.GetType().GetProperties()
                .Select(p => p.Name).ToList();

            var orderStatement = string.Empty;

            //Check column name in order condition.
            if (!String.IsNullOrEmpty(searchRequest.SortingColumn))
            {
                if (!propertiesNames.Contains(searchRequest.SortingColumn))
                {
                    string message = string.Format("Invalid column in order statement: {0}", searchRequest.SortingColumn);
                    return BadRequest(message);
                }

                orderStatement = String.Format("ORDER BY {0} {1}", searchRequest.SortingColumn, searchRequest.SortingDirection == SortingDirection.ASCENDING ? "ASC" : "DESC");
            }

            if (searchRequest.Conditions == null || searchRequest.Conditions.Count == 0)
            {
                query = String.Format(STUDENTS_BASE_QUERY, "", orderStatement);
            }
            else
            {
                var invalidColumns = searchRequest.Conditions.Where(c => !propertiesNames.Contains(c.Column)).Select(c => c.Column).ToList();
                if (invalidColumns.Count > 0)
                {
                    var message = string.Format("Invalid columns: {0}", String.Join(", ", invalidColumns));
                    return BadRequest(message);
                }

                //If column names are valid, prepare query.
                string where_string = string.Empty;

                var new_dict = ConditionParser.ParsePostgres(out where_string, searchRequest.Conditions, temp);
                paramDict = paramDict.Concat(new_dict).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
                query = String.Format(STUDENTS_BASE_QUERY, where_string, orderStatement);
            }
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters(paramDict);
                var result = await connection.QueryAsync<SchoolSummary>(query, parameters);
                return Ok(result);
            }
        }

        [HttpPost("financial")]
        public async Task<ActionResult<IEnumerable<FinancialSummary>>> getFinancialSummary(SearchRequest searchRequest)
        {
            var query = string.Empty;
            var paramDict = new Dictionary<string, object>();
            paramDict.Add("@limit", searchRequest.Limit > 0 ? searchRequest.Limit : RECORD_DEFAULT_LIMIT);
            paramDict.Add("@offset", searchRequest.Offset > 0 ? searchRequest.Offset : 0);

            School temp = new School()
            {
            };
            var propertiesNames = temp.GetType().GetProperties()
                .Select(p => p.Name).ToList();

            var orderStatement = string.Empty;

            //Check column name in order condition.
            if (!String.IsNullOrEmpty(searchRequest.SortingColumn))
            {
                if (!propertiesNames.Contains(searchRequest.SortingColumn))
                {
                    string message = string.Format("Invalid column in order statement: {0}", searchRequest.SortingColumn);
                    return BadRequest(message);
                }

                orderStatement = String.Format("ORDER BY {0} {1}", searchRequest.SortingColumn, searchRequest.SortingDirection == SortingDirection.ASCENDING ? "ASC" : "DESC");
            }

            if (searchRequest.Conditions == null || searchRequest.Conditions.Count == 0)
            {
                query = String.Format(FINANCIAL_BASE_QUERY, "", orderStatement);
            }
            else
            {
                var invalidColumns = searchRequest.Conditions.Where(c => !propertiesNames.Contains(c.Column)).Select(c => c.Column).ToList();
                if (invalidColumns.Count > 0)
                {
                    var message = string.Format("Invalid columns: {0}", String.Join(", ", invalidColumns));
                    return BadRequest(message);
                }

                //If column names are valid, prepare query.
                string where_string = string.Empty;

                var new_dict = ConditionParser.ParsePostgres(out where_string, searchRequest.Conditions, temp);
                paramDict = paramDict.Concat(new_dict).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
                query = String.Format(FINANCIAL_BASE_QUERY, where_string, orderStatement);
            }
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters(paramDict);
                var result = await connection.QueryAsync<FinancialSummary>(query, parameters);
                return Ok(result);
            }
        }
    }
}