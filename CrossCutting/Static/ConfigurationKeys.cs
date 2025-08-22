namespace CrossCutting.Static
{
    public static class ConfigurationKeys
    {
        public const string WaitTimeInSeconds = "WaitTimeInSeconds";
        public const string Url = "Url";
        public const string Browser = "Browser";
        public const string CookiesElementBy = "CookiesElementBy";
        public const string DownloadDirectory = "Downloads";

        public static class SearchForPositionBasedOnCriteria
        {
            public const string AboutLinkText = "SearchForPositionBasedOnCriteria:AboutLinkText";
            public const string CareersLinkText = "SearchForPositionBasedOnCriteria:CareersLinkText";
            public const string InsightsLinkText = "SearchForPositionBasedOnCriteria:InsightsLinkText";
            public const string KeywordsId = "SearchForPositionBasedOnCriteria:KeywordsId";
            public const string Location = "SearchForPositionBasedOnCriteria:Location";
            public const string AllLocations = "SearchForPositionBasedOnCriteria:AllLocations";
            public const string RemoteOption = "SearchForPositionBasedOnCriteria:RemoteOption";
            public const string Find = "SearchForPositionBasedOnCriteria:Find";
            public const string LatestElement = "SearchForPositionBasedOnCriteria:LatestElement";
            public const string Apply = "SearchForPositionBasedOnCriteria:Apply";
        }

        public static class ValidateGlobalSearch
        {
            public const string Magnifier = "ValidateGlobalSearch:Magnifier";
            public const string Search = "ValidateGlobalSearch:Search";
            public const string Find = "ValidateGlobalSearch:Find";
            public const string Articles = "ValidateGlobalSearch:Articles";
            public const string ArticleTitle = "ValidateGlobalSearch:ArticleTitle";
            public const string ArticleDescription = "ValidateGlobalSearch:ArticleDescription";
        }

        public static class Insights
        {
            public const string CarouselNext = "Insights:CarouselNext";
            public const string InsightArticleTitle = "Insights:InsightArticleTitle";
            public const string CurrentCarouselTitle = "Insights:CurrentCarouselTitle";
            public const string ReadMoreButton = "Insights:ReadMoreButton";
            public const string CarouselContainer = "Insights:CarouselContainer";
        }

        public static class Download
        {
            public const string DownloadButton = "Download:DownloadButton";
        }
    }
}
