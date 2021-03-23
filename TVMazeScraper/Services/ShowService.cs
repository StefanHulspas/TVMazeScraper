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

		public List<Show> GetShowsForPageAsync(int pageNr) {
			if (_dataContext.Shows.ContainsKey(pageNr))
			{
				return _dataContext.Shows[pageNr];
			}
			
			return new List<Show>();
		}

		public int GetShowPagesSaved()
		{
			if (_dataContext.Shows.Keys.Count > 0)
				return _dataContext.Shows.Keys.Max() + 1;
			return 0;
		}
	}
}
