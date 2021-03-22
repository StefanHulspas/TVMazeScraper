namespace TVMazeScraper.Contracts.V1
{
	public static class ApiRoutes
	{
		public const string Root = "api";
		public const string Version = "v1";
		public const string Base = Root + "/" + Version;

		public static class Shows {
			
			public const string GetFirstPage = Base + "/shows";
			public const string GetPage = Base + "/shows/{pageNR}";
		}
	}
}
