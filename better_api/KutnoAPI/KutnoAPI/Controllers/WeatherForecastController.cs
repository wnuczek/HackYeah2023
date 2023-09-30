using KutnoAPI.Parsers;
using Microsoft.AspNetCore.Mvc;

namespace KutnoAPI.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class WeatherForecastController : ControllerBase
	{
		private readonly ILogger<WeatherForecastController> _logger;

		public WeatherForecastController(ILogger<WeatherForecastController> logger, IConfiguration con)
		{
			_logger = logger;
		}

		[HttpGet]
		public async Task<ObjectResult> Get()
		{

			return Ok("Zajebiœcie");

		}
	}
}