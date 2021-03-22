using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TVMazeScraper.Domain
{
	public class Show
	{
		[Key]
		public int Id { get; set; }
		public string Name { get; set; }
		public List<CastMember> Cast { get; set; }
	}
}
