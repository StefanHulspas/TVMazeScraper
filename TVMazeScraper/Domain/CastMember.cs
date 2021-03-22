using System;
using System.ComponentModel.DataAnnotations;

namespace TVMazeScraper.Domain
{
	public class CastMember
	{
		[Key]
		public int Id { get; set; }
		public string Name { get; set; }
		public DateTime Birthday { get; set; }
	}
}
