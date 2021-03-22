using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TVMazeScraper.Domain;

namespace TVMazeScraper.Data
{
	public class DataContext : IdentityDbContext
	{

		private const string tvMazeShowListBaseUrl = "http://api.tvmaze.com/shows?page=";

		public DataContext() {
			PopulateDataContext();
		}

		public Dictionary <int, List<Show>> Shows {get; set;}

		private async void PopulateDataContext() {
			int index = 0;
			bool hasReachedEndPage = false;
			while (!hasReachedEndPage) {
				var shows = GetShowsForPage(index);
				if (!string.IsNullOrEmpty(shows.Result)) {
					List<Show> newShowList = new List<Show>();
					newShowList = JsonConvert.DeserializeObject<List<Show>>(shows.Result);
					if (newShowList.Count > 0)
					{
						Shows.Add(index, newShowList);
					}
				}
			}
		}

		private async Task<string> GetShowsForPage(int index)
		{
			using (var httpClient = new HttpClient())
			{
				using (var response = await httpClient.GetAsync(tvMazeShowListBaseUrl + index))
				{
					if (response.IsSuccessStatusCode)
						return await response.Content.ReadAsStringAsync();
					else return null;
				}
			}
		}
	}
}
