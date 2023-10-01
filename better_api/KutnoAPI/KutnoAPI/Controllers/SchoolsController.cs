using Dapper;
using KutnoAPI.Extensions;
using KutnoAPI.Models;
using KutnoAPI.Parsers;
using KutnoAPI.Services;
using MesInternalApi.Extensions;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace KutnoAPI.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class SchoolsController : ControllerBase
	{
		private string _connectionString = string.Empty;
		private string SCHOOLS_BASE_QUERY = "SELECT * FROM pgj.schools";

        private const int RECORD_DEFAULT_LIMIT = 100;

        private readonly ILogger<SchoolsController> _logger;

		public SchoolsController(ILogger<SchoolsController> logger, IConfiguration configuration)
		{
			_connectionString = configuration["DbString"];
			_logger = logger;
		}
        [Route("SendBytes")]
        [HttpPost]
        public async Task<IActionResult> SendBytes()
        {
            try
            {
				string fileName = "C:\\Users\\PGJ\\PycharmProjects\\HackYeah2023\\docs\\SIO 30.09.2022.xml";
				byte[] fileBytes = System.IO.File.ReadAllBytes(fileName);

                SchoolUploadRequest schoolUploadRequest = new()
                {
                    Year = 2022,
                    SchoolsWorksheet = fileBytes,
                    JobsWorksheet = fileBytes
                };

                HttpClient client = new()
                {
                    BaseAddress = new Uri("https://localhost:7218/")
                };
                var result = await APIService.CallApi(client, "Schools/Parse", HttpMethod.Post, schoolUploadRequest);
                return Ok(result);
			}
			catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("Parse")]
        [HttpPost]
        public async Task<IActionResult> Parse([FromBody] SchoolUploadRequest schoolUploadRequest)
        {
			try
			{
				SchoolParser schoolParser = new ();
				var r = schoolParser.Parse(schoolUploadRequest);
                SchoolService service = new();
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    service.InsertSchoolData(r, connection);
                }
                return Ok();
			}
			catch (Exception ex)
			{
                return BadRequest(ex.Message);
			}
		}
		[HttpPost(Name = "GetSchools")]
        public async Task<ActionResult<IEnumerable<School>>> getSchools(SearchRequest searchRequest)
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
                query = String.Format(SCHOOLS_BASE_QUERY, "", orderStatement);
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
                query = String.Format(SCHOOLS_BASE_QUERY, where_string, orderStatement);
            }
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters(paramDict);
                var result = await connection.QueryAsync<School>(query, parameters);
                return Ok(result);
            }
    }



	}
}