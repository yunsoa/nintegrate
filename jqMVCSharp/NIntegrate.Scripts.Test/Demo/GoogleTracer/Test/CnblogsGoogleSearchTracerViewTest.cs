using System;
using ScriptFX;
using NIntegrate.Scripts.Test.Demo.GoogleTracer.Views;
using JQuerySharp;
using jqMock;
using NIntegrate.Scripts.Test.Demo.GoogleTracer.Records;
using System.DHTML;

namespace NIntegrate.Scripts.Test.Demo.GoogleTracer.Test
{
    public class CnblogsGoogleSearchTracerViewTest : TestCase
    {
        public override void Execute()
        {
            base.Execute();

            CnblogsGoogleSearchTracerView view = new CnblogsGoogleSearchTracerView();

            QUnit.Test("Test SearchStart", delegate
            {
                QUnit.Equals(0, view.SearchStart);
                view.SearchStart = 1;
                QUnit.Equals(1, view.SearchStart);
                view.SearchStart = 2;
                QUnit.Equals(2, view.SearchStart);
            });

            QUnit.Test("Test GetSearchKeyword()", delegate
            {
                JQueryFactory.JQuery(JQuerySelectors.SEARCH_KEYWORD).Html("keyword1");
                QUnit.Equals("keyword1", view.GetSearchKeyword());
                JQueryFactory.JQuery(JQuerySelectors.SEARCH_KEYWORD).Html("keyword2");
                QUnit.Equals("keyword2", view.GetSearchKeyword());
            });

            QUnit.Test("Test RenderSearchResult()", delegate
            {
                jQuery pnlSearchResults = JQueryFactory.JQuery(JQuerySelectors.SEARCH_RESULTS_PANEL);
                jQuery btnShowMoreResults = JQueryFactory.JQuery(JQuerySelectors.SHOW_MORE_RESULTS_BUTTON);

                Mock mockJQuery = new Mock(Window.Self, "jQuery");
                mockJQuery.Modify().Args(JQuerySelectors.SEARCH_RESULTS_PANEL).ReturnValue(pnlSearchResults);
                mockJQuery.Modify().Args(JQuerySelectors.SHOW_MORE_RESULTS_BUTTON).ReturnValue(btnShowMoreResults);
                Mock mockSetTemplateElement = new Mock(pnlSearchResults, "setTemplateElement");
                mockSetTemplateElement.Modify().Args(JTemplateElements.GOOGLE_TRACER).ReturnValue(pnlSearchResults);
                Mock mockProcessTemplate = new Mock(pnlSearchResults, "processTemplate");
                mockProcessTemplate.Modify().Args(Is.Anything).ReturnValue();
                Mock mockShowMoreResultsBindClick = new Mock(btnShowMoreResults, "click");
                mockShowMoreResultsBindClick.Modify().Args(Is.Anything).ReturnValue();

                view.RenderSearchResult(new GoogleSearchResponse());

                mockJQuery.Verify();
                mockJQuery.Restore();
                mockSetTemplateElement.Verify();
                mockSetTemplateElement.Restore();
                mockProcessTemplate.Verify();
                mockProcessTemplate.Restore();
                mockShowMoreResultsBindClick.Verify();
                mockShowMoreResultsBindClick.Restore();
            });

            QUnit.Test("Test ShowMoreResults Event", delegate
            {
                QUnit.Equals(false, _showMoreResultsClicked);

                view.ShowMoreResults += new System.DHTML.DOMEventHandler(view_ShowMoreResults);
                view.RenderSearchResult(new GoogleSearchResponse());
                JQueryFactory.JQuery(JQuerySelectors.SHOW_MORE_RESULTS_BUTTON).Click();

                QUnit.Equals(true, _showMoreResultsClicked);
            });
        }

        private bool _showMoreResultsClicked = false;

        void view_ShowMoreResults()
        {
            _showMoreResultsClicked = true;
        }
    }
}
