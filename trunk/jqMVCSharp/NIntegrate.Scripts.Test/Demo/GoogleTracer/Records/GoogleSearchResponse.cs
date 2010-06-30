using System;
using ScriptFX;

namespace NIntegrate.Scripts.Test.Demo.GoogleTracer.Records
{
    [Record]
    public sealed class GoogleSearchResponse
    {
        public GoogleSearchResponseData ResponseData;
        public string ResponseDetails;
        public int ResponseStatus;
    }
}
