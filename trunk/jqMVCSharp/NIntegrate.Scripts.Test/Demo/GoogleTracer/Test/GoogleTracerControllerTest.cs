using System;
using ScriptFX;
using NIntegrate.Scripts.Test.Demo.GoogleTracer.Views;
using NIntegrate.Scripts.Test.Demo.GoogleTracer.Controllers;
using jqMock;
using NIntegrate.Scripts.Test.Demo.GoogleTracer.Records;
using System.DHTML;

namespace NIntegrate.Scripts.Test.Demo.GoogleTracer.Test
{
    public class MockGoogleTracerView : IGoogleTracerView
    {
        private int _searchStart = 0;

        #region IGoogleTracerView Members

        public int SearchStart
        {
            get
            {
                return _searchStart;
            }
            set
            {
                _searchStart = value;
            }
        }

        public string GetSearchKeyword()
        {
            return "keyword";
        }

        public void RenderSearchResult(NIntegrate.Scripts.Test.Demo.GoogleTracer.Records.GoogleSearchResponse response)
        {
            return;
        }

        public event System.DHTML.DOMEventHandler ShowMoreResults;

        #endregion
    }

    public class GoogleTracerControllerTest : TestCase
    {
        public override void Execute()
        {
            base.Execute();

            MockGoogleTracerView mockView = new MockGoogleTracerView();
            Container.RegisterInstance(typeof(IGoogleTracerView), mockView);

            GoogleTracerController controller = new GoogleTracerController();

            QUnit.Test("Test get View", delegate
            {
                QUnit.Equals(mockView, controller.View);
            });

            QUnit.Test("Test Execute() & ShowMoreResults()", delegate
            {
                GoogleSearchResponse data = new GoogleSearchResponse();

                Mock mockAddShowMoreResults = new Mock(mockView, "add_showMoreResults");
                mockAddShowMoreResults.Modify().Args(Is.Anything).ReturnValue();
                Mock mockRenderSearchResult = new Mock(mockView, "renderSearchResult");
                mockRenderSearchResult.Modify().Args(data).ReturnValue();
                mockRenderSearchResult.Modify().Args(data).ReturnValue();
                Mock mockGetScript = new Mock(Script.Eval("jQuery"), "getScript");
                mockGetScript.Modify().Args(Is.Anything, Is.Anything).Callback(1, null).ReturnValue();
                mockGetScript.Modify().Args(Is.Anything, Is.Anything).Callback(1, null).ReturnValue();

                QUnit.Equals(0, mockView.SearchStart);
                ((Dictionary)(object)Window.Self)["_googlewebsearchresults"] = data;
                controller.Execute();
                ((Dictionary)(object)Window.Self)["_googlewebsearchresults"] = data;
                controller.ShowMoreResults();
                QUnit.Equals(4, mockView.SearchStart);

                mockAddShowMoreResults.Verify();
                mockAddShowMoreResults.Restore();
                mockRenderSearchResult.Verify();
                mockRenderSearchResult.Restore();
                mockGetScript.Verify();
                mockGetScript.Restore();
            });
        }
    }
}
