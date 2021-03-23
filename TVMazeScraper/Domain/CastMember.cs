using System;
using System.ComponentModel.DataAnnotations;
using TVMazeScraper.Domain.TVMaze;

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
