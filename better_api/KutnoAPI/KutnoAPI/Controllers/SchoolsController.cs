using Microsoft.AspNetCore.Mvc;

using Dapper;
using Npgsql;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace KutnoAPI.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class SchoolsController : ControllerBase
	{
		private string _connectionString = string.Empty;
		private static readonly string[] Summaries = new[]
		{
		"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
	};

		private readonly ILogger<SchoolsController> _logger;

		public SchoolsController(ILogger<SchoolsController> logger, IConfiguration configuration)
		{
			_connectionString = configuration["DbString"];
			_logger = logger;
		}

		[HttpGet(Name = "GetSchools")]
        public async Task<ActionResult<long>> Get()

        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                return Ok(0);
            }
        }
	}
}