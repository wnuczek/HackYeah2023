using Microsoft.AspNetCore.Mvc;

using Dapper;
using Npgsql;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using KutnoAPI.Models;
using MesInternalApi.Extensions;
using KutnoAPI.Parsers;
using KutnoAPI.Services;

namespace KutnoAPI.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class UploadSchoolDataController : ControllerBase
	{
		private string _connectionString = string.Empty;

        private readonly ILogger<UploadSchoolDataController> _logger;

		public UploadSchoolDataController(ILogger<UploadSchoolDataController> logger, IConfiguration configuration)
		{
			_connectionString = configuration["DbString"];
			_logger = logger;
		}

		[HttpPost(Name = "UploadSchools")]
        public async Task<ActionResult> Upload([FromBody] SchoolUploadRequest request)
        {
            try
            {
                SchoolParser schoolParser = new();
                JobParser jobParser = new();

                var r = schoolParser.Parse(request);
                var j = jobParser.Parse(request);
                foreach (var jj in j)
                {
                    jj.Year = request.Year;
                }
                foreach (var s in r)
                {
                    s.Summary.Year = request.Year;
                }
                SchoolService service = new();
                JobService jservice = new();

                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    service.InsertSchoolData(r, connection);
                    jservice.InsertJobsData(j, connection);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
	}
}