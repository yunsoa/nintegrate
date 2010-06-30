using System;
using ScriptFX;
using NIntegrate.Scripts.Test.Demo.GoogleTracer.Records;
using System.DHTML;

namespace NIntegrate.Scripts.Test.Demo.GoogleTracer.Views
{
    public interface IGoogleTracerView
    {
        int SearchStart { get; set; }
        string GetSearchKeyword();
        void RenderSearchResult(GoogleSearchResponse response);
        event DOMEventHandler ShowMoreResults;
    }
}
