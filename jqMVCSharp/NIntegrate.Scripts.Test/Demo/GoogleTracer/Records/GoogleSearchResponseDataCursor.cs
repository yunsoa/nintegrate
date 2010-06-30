using System;
using ScriptFX;

namespace NIntegrate.Scripts.Test.Demo.GoogleTracer.Records
{
    [Record]
    public sealed class GoogleSearchResponseDataCursor
    {
        public GoogleSearchResponseDataCursorPage[] Pages;
        public int EstimatedResultCount;
        public int CurrentPageIndex;
        public string MoreResultUrl;
    }
}
