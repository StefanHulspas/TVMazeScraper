using System.Collections.Generic;
using TVMazeScraper.Domain;

namespace TVMazeScraper.Data
{
    public interface IDataContext
    {
        
        public Dictionary <int, List<Show>> Shows {get;}
    }
}