namespace TVMazeScraper.Contracts.V1
{
	public static class ApiRoutes
	{
		public const string Root = "api";
		public const string Version = "v1";
		public const string Base = Root + "/" + Version;

		public static class Shows {
			
			public const string GetDefaultPage = Base + "/shows";
			public const string GetPage = Base + "/shows/{pageNr}";
			public const string GetShowPagesSaved = Base + "/countsavedpages";
		}
	}
}
