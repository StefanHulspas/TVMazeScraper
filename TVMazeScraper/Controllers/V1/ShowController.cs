using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using TVMazeScraper.Domain;
using TVMazeScraper.Contracts.V1;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using TVMazeScraper.Services;

namespace TVMazeScraper.Controllers.V1
{
	public class ShowController : Controller
	{

		private readonly IShowService showService;

		public ShowController() {

		}

		[HttpGet(ApiRoutes.Shows.GetPage)]
		private IActionResult GetPage([FromRoute]int pageNr)
		{
			return Ok(showService.GetShowsForPageAsync(pageNr));
		}
	}
}
