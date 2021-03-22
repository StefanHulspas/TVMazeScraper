using Microsoft.AspNetCore.Mvc;
using TVMazeScraper.Contracts.V1;
using TVMazeScraper.Services;

namespace TVMazeScraper.Controllers.V1
{
	public class ShowController : Controller
	{

		private readonly IShowService showService;

		[HttpGet(ApiRoutes.Shows.GetFirstPage)]
		public IActionResult GetFirstPage()
		{
			return Ok(showService.GetShowsForPageAsync(0));
		}
		
		[HttpGet(ApiRoutes.Shows.GetPage)]
		public IActionResult GetPage([FromRoute]int pageNr)
		{
			return Ok(showService.GetShowsForPageAsync(pageNr));
		}
	}
}
