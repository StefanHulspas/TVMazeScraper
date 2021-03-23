using System.Collections.Generic;
using System.Threading.Tasks;
using TVMazeScraper.Domain;

namespace TVMazeScraper.Services
{
	public interface IShowService
	{
		public List<Show> GetShowsForPageAsync(int pageNr);
		public int GetShowPagesSaved();
	}
}
