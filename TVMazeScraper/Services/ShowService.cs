using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TVMazeScraper.Data;
using TVMazeScraper.Domain;

namespace TVMazeScraper.Services
{
	public class ShowService : IShowService
	{
		private readonly DataContext _dataContext;

		public ShowService(DataContext dataContext) {
			_dataContext = dataContext;
		}

		public async Task<List<Show>> GetShowsForPageAsync(int pageNr) {
			if (_dataContext.Shows.ContainsKey(pageNr))
			{
				return _dataContext.Shows[pageNr];
			}
			else return null;
		}
	}
}
