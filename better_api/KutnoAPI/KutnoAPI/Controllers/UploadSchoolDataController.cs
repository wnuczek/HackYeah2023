using Microsoft.AspNetCore.Mvc;

using Dapper;
using Npgsql;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using KutnoAPI.Models;
using MesInternalApi.Extensions;

namespace KutnoAPI.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class UploadSchoolDataController : ControllerBase
	{
		private string _connectionString = string.Empty;
		private string SCHOOLS_BASE_QUERY = "SELECT * FROM g.schools";

        private const int RECORD_DEFAULT_LIMIT = 100;

        private readonly ILogger<UploadSchoolDataController> _logger;

		public UploadSchoolDataController(ILogger<UploadSchoolDataController> logger, IConfiguration configuration)
		{
			_connectionString = configuration["DbString"];
			_logger = logger;
		}

		[HttpGet(Name = "Upload")]
        private async Task<ActionResult<bool>> Upload([FromBody] SchoolUploadRequest searchRequest)
        {
			return true;
        }
	}
}