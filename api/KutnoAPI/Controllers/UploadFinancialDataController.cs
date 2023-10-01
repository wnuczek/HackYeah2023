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
				foreach(var i in incomes)
				{
					i.Year = request.Year;
				}
				var costs = parser.Parse(request.CostsSummary, true);
                foreach (var c in costs)
                {
                    c.Year = request.Year;
                }

				var all = incomes.Concat(costs)
					.GroupBy(r => r.Regon + '|' + r.Year.ToString() + '|' + r.YearPart.ToString() + '|' + r.Chapter + '|' + r.Paragraph)
					.Select(gr => new FinancialSummary()
					{
						Regon = gr.Max(i => i.Regon),
						Year = gr.Max(i => i.Year),
						YearPart = gr.Max(i => i.YearPart),
						Paragraph = gr.Max(i => i.Paragraph),
						Chapter = gr.Max(i => i.Chapter),
						Department = gr.Max(i => i.Department),
                        Engagement = gr.Max(i => i.Engagement),
                        MadeCost = gr.Max(i => i.MadeCost),
                        MadeIncome = gr.Max(i => i.MadeIncome),
						PlannedCost = gr.Max(i => i.PlannedCost),
						PlannedIncome = gr.Max(i => i.PlannedIncome),
						Receivables = gr.Max(i => i.Receivables)

                    }).ToList();

				var service = new FinancialSummaryService();
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    service.InsertDefinitionData(all, connection);
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