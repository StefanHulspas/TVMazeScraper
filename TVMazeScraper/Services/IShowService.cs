using System.Collections.Generic;
using System.Threading.Tasks;
using TVMazeScraper.Domain;

namespace TVMazeScraper.Services
{
	public interface IShowService
	{
		public Task<List<Show>> GetShowsForPageAsync(int pageNr);
	}
}
