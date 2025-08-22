namespace CrossCutting.Types
{
    public static class BrowserTypeFactory
    {
        public static BrowserType FromString(string browser)
        {
            return browser.ToLower() switch
            {
                "chrome" => BrowserType.Chrome,
                "firefox" => BrowserType.Firefox,
                "edge" => BrowserType.Edge,
                _ => throw new ArgumentException($"Browser type '{browser}' is not supported.")
            };
        }
    }
}
