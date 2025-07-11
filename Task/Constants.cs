namespace Task
{
    public static class Constants
    {
        public const int WaitTimeInSeconds = 10;
        public const string Url = "https://www.epam.com/";

        public static class SearchForPositionBasedOnCriteria
        {
            public const string CareersLinkText = "Careers";
            public const string KeywordsId = "new_form_job_search-keyword";
            public const string ProgrammingLanguage = "C#";
            public const string LocationFieldName = "location";
            public const string RemoteOption = "//*[@id=\"jobSearchFilterForm\"]/fieldset/div/p[1]/label";
            public const string Find = "//form[@id=\"jobSearchFilterForm\"]/descendant::button[@type=\"submit\"]";
            public const string LatestElement = "//ul[contains(@class, 'search-result__list')]/li[last()]";
            public const string Apply = "//ul[contains(@class, 'search-result__list')]/li[last()]//a[contains(text(), 'apply')]";
        }

        public static class ValidateGlobalSearch
        {
            public const string Magnifier = "search-icon";
            public const string Search = "q";
            public const string Find = ".//form[@class='header-search__field no-focus']//button";
            public const string LinksContainingTheWords = "//div[@class=\"search-results__items\"]//*[contains(text(), 'blockchain')]";
            public const string SearchTerm = "BLOCKCHAIN";
        }
    }
}
