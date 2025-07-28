namespace CrossCutting.Static
{
    public static class Constants
    {
        public const int WaitTimeInSeconds = 10;
        public const string Url = "https://www.epam.com/";
        public static string CookiesElementBy = "div#onetrust-consent-sdk #onetrust-accept-btn-handler";
        public static string DownloadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Downloads");

        public static class SearchForPositionBasedOnCriteria
        {
            public const string AboutLinkText = "About";
            public const string CareersLinkText = "Careers";
            public const string InsightsLinkText = "Insights";
            public const string KeywordsId = "new_form_job_search-keyword";
            public const string Location = "div.recruiting-search__location";
            public const string AllLocations = ".select2-results ul li:first-child ul li:first-child";
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
            public const string Articles = "//div[@class=\"search-results__items\"]//article";
        }

        public static class Insights
        {
            public const string CarouselNext = "button.slider__right-arrow";
            public const string CurrentCarouselTitle = "div.slider div.active div.slider__slide div.text";
            public const string ReadMoreButton = "div.slider div.active div.slider__slide div.single-slide__cta-container a";
            public const string CarouselContainer = "div.owl-stage-outer div.owl-stage div.owl-item.active div.slider__slide";
        }

        public static class Download
        {
            public const string DownloadButton = "//a[@download]";
        }
    }
}
