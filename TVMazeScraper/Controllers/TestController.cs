using Microsoft.AspNetCore.Mvc;

namespace TVMazeScraper.Controllers
{
	public class TestController : Controller
	{
		[HttpGet("api/user")]
		public IActionResult Get() {
			return Ok(new { name = "Stefan" });
		}
	}
}
