using Microsoft.AspNetCore.Mvc;

using Dapper;
using Npgsql;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using KutnoAPI.Models;
using MesInternalApi.Extensions;
using KutnoAPI.Extensions;
using KutnoAPI.Parsers;
using KutnoAPI.Services;

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
		[Route("SendBytes")]
		[HttpPost]
		public async Task<IActionResult> SendBytes()
		{
			try
			{

				string fileNameIncomeSummary = Path.Combine("..", "..", "..", "docs", "Sprawozdania[2022][IVKwarta³] Dochody.xml");
				byte[] fileBytesIncome = System.IO.File.ReadAllBytes(fileNameIncomeSummary);
				string fileNameCostsSummary = Path.Combine("..", "..", "..", "docs", "Sprawozdania[2022][IVKwarta³] Wydatki.xml");
				byte[] fileBytesCosts = System.IO.File.ReadAllBytes(fileNameCostsSummary);

				FinancialSummaryUploadRequest financialUploadRequest = new()
				{
					Year = 2022,
					IncomeSummary = fileBytesIncome,
					CostsSummary = fileBytesCosts
				};

				HttpClient client = new()
				{
					BaseAddress = new Uri("https://localhost:7218/")
				};
				var result = await APIService.CallApi(client, "UploadFinancialData", HttpMethod.Post, financialUploadRequest);

				return Ok(result);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpPost(Name = "UploadFinancial")]
        public async Task<ActionResult<bool>> Upload([FromBody] FinancialSummaryUploadRequest request)
        {
			try
			{
				FinancialStatementParser parser = new();
				var incomes = parser.Parse(request.IncomeSummary, false);
				var costs = parser.Parse(request.CostsSummary, true);

				foreach(var income in incomes)
				{
					income.Year = request.Year;
				}
				foreach (var cost in costs)
				{
					cost.Year = request.Year;
				}
				var unionResult = incomes.Union(costs).ToList();


				return Ok(unionResult);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
        }
	}
}