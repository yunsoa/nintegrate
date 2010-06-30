using System;
using ScriptFX;
using JQuerySharp;
using NIntegrate.Scripts.Test.Demo.GoogleTracer.Records;
using System.DHTML;
using NIntegrate.Scripts.Plugins;

namespace NIntegrate.Scripts.Test.Demo.GoogleTracer.Views
{
    public class CnblogsGoogleSearchTracerView : IGoogleTracerView
    {
        private DOMEventHandler _showMoreResults;
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
            string keyword = (string)(object)JQueryFactory.JQuery(JQuerySelectors.SEARCH_KEYWORD).Text();
            return keyword;
        }

        public void RenderSearchResult(GoogleSearchResponse response)
        {
            ((JTemplatePlugin)JQueryFactory.JQuery(JQuerySelectors.SEARCH_RESULTS_PANEL))
                .SetTemplateElement(JTemplateElements.GOOGLE_TRACER)
                .ProcessTemplate(response);

            JQueryFactory.JQuery(JQuerySelectors.SHOW_MORE_RESULTS_BUTTON).Click(_showMoreResults);
        }

        public event DOMEventHandler ShowMoreResults
        {
            add
            {
                _showMoreResults = (DOMEventHandler)Delegate.Combine(_showMoreResults, value);
            }
            remove
            {
                _showMoreResults = (DOMEventHandler)Delegate.Remove(_showMoreResults, value);
            }
        }

        #endregion
    }
}
