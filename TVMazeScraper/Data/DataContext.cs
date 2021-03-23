using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TVMazeScraper.Domain;
using TVMazeScraper.Domain.TVMaze;

namespace TVMazeScraper.Data
{
	public class DataContext : IDataContext
	{

		private const string TvMazeShowPageRequest = "http://api.tvmaze.com/shows?page=";
		private const string TvMazeCastRequestUrl = "http://api.tvmaze.com/shows/{showId}/cast";
		private float MinimumRequestTime = 10f / 20f; // 20 calls per 10 seconds
		private DateTime LastRequest;

		public DataContext()
		{
			LastRequest = new DateTime();
			Shows = new Dictionary<int, List<Show>>();
			Task.Run(async () => await PopulateDataContext());
		}

		public Dictionary <int, List<Show>> Shows {get;}

		private async Task PopulateDataContext() {
			bool hasReachedEndPage = false;
			for (int i = 0; !hasReachedEndPage; i++) {
				var showsRequest = await GetShowsForPage(i);
				if (!string.IsNullOrWhiteSpace(showsRequest))
				{
					var newShowList = JsonConvert.DeserializeObject<List<Show>>(showsRequest);
					if (newShowList.Count > 0)
					{
						Shows.Add(i, newShowList);
						await UpdateCastForShowPage(i);
						continue;
					}
				}
				hasReachedEndPage = true;
			}
		}

		private async Task UpdateCastForShowPage(int pageNr)
		{
			var showList = Shows[pageNr];
			for (int i = 0; i < showList.Count; i++)
			{
				var castMemberRequest = await GetCastForShow(showList[i].Id);
				if (!string.IsNullOrWhiteSpace(castMemberRequest))
				{
					var tvMazeCastList = JsonConvert.DeserializeObject<List<TvMazeCastMember>>(castMemberRequest);
					var newCastList = tvMazeCastList.ConvertAll(x => new CastMember
						{
							Id = x.Person.Id,
							Name = x.Person.Name, 
							Birthday = string.IsNullOrWhiteSpace(x.Person.Birthday) ? new DateTime() : DateTime.Parse(x.Person.Birthday)
						});
					newCastList.Sort((x, y) => DateTime.Compare(x.Birthday, y.Birthday));
					Shows[pageNr][i].Cast = newCastList;
				}
			}
		}

		private async Task<string> GetShowsForPage(int index)
		{
			await DelayRequest();
			using (var httpClient = new HttpClient())
			{
				using (var response = await httpClient.GetAsync(TvMazeShowPageRequest + index))
				{
					if (response.IsSuccessStatusCode)
						return await response.Content.ReadAsStringAsync();
				}
			}
			return null;
		}

		private async Task<string> GetCastForShow(int showId)
		{
			await DelayRequest();
			using (var httpClient = new HttpClient())
			{
				using (var response = await httpClient.GetAsync(TvMazeCastRequestUrl.Replace("{showId}", showId.ToString())))
				{
					if (response.IsSuccessStatusCode)
						return await response.Content.ReadAsStringAsync();
				}
			}
			return null;
			
		}

		private async Task DelayRequest()
		{
			var timeSinceLastRequest = DateTime.Now - LastRequest;
			if (timeSinceLastRequest.TotalMilliseconds < MinimumRequestTime)
			{
				await Task.Delay(timeSinceLastRequest.Milliseconds);
			}
			LastRequest = DateTime.Now;
		}
	}
}
