using System;
using ScriptFX;

namespace NIntegrate.Scripts.Test.Demo.GoogleTracer.Records
{
    [Record]
    public sealed class GoogleSearchResponseData
    {
        public GoogleSearchResponseDataResult[] Results;
        public GoogleSearchResponseDataCursor Cursor;
    }
}
