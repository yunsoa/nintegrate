using System;
using ScriptFX;

namespace NIntegrate.Scripts.Test.Demo.GoogleTracer.Records
{
    [Record]
    public sealed class GoogleSearchResponseDataResult
    {
        [PreserveCase]
        public string GsearchResultClass;
        public string UnescapedUrl;
        public string Url;
        public string VisibleUrl;
        public string CacheUrl;
        public string Title;
        public string TitleNoFormatting;
        public string Content;
    }
}
