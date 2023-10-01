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
	public class UploadFinancialDataController : ControllerBase
	{
		private string _connectionString = string.Empty;
		private string SCHOOLS_BASE_QUERY = "SELECT * FROM g.schools";

        private const int RECORD_DEFAULT_LIMIT = 100;

        private readonly ILogger<UploadFinancialDataController> _logger;

		public UploadFinancialDataController(ILogger<UploadFinancialDataController> logger, IConfiguration configuration)
		{
			_connectionString = configuration["DbString"];
			_logger = logger;
		}

		[HttpGet(Name = "UploadFinancial")]
        private async Task<ActionResult<bool>> Upload([FromBody] SchoolUploadRequest searchRequest)
        {
			return true;
        }
	}
}