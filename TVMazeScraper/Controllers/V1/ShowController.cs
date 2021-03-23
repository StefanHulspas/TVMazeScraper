using Microsoft.AspNetCore.Mvc;
using TVMazeScraper.Contracts.V1;
using TVMazeScraper.Services;

namespace TVMazeScraper.Controllers.V1
{
	public class ShowController : Controller
	{

		private readonly IShowService _showService;

		public ShowController(IShowService showService)
		{
			_showService = showService;
		}

		[HttpGet(ApiRoutes.Shows.GetDefaultPage)]
		public IActionResult GetDefaultPage()
		{
			return GetPage(0);
		}
		
		[HttpGet(ApiRoutes.Shows.GetPage)]
		public IActionResult GetPage([FromRoute]int pageNr)
		{
			return Ok(_showService.GetShowsForPageAsync(pageNr));
		}
		
		[HttpGet(ApiRoutes.Shows.GetShowPagesSaved)]
		public IActionResult GetShowPagesSaved()
		{
			return Ok($"Show pages saved: {_showService.GetShowPagesSaved()}");
		}
	}
}
