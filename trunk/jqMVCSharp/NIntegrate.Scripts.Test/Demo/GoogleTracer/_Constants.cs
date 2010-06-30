using System;
using ScriptFX;

namespace NIntegrate.Scripts.Test.Demo.GoogleTracer
{
    public static class JTemplateElements
    {
        public const string GOOGLE_TRACER = "jtGoogleTracer";
    }

    public static class JQuerySelectors
    {
        public const string SEARCH_KEYWORD = ".post .postTitle";
        public const string SHOW_MORE_RESULTS_BUTTON = "#btnShowMoreResults";
        public const string SEARCH_RESULTS_PANEL = "#divSearchResults";
    }

    public static class SearchUrls
    {
        public const string WEB_SEARCH_URL = "http://ajax.googleapis.com/ajax/services/search/web?v=1.0&start={1}&callback={2}&q={0}";
    }
}
