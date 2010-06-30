using System;
using ScriptFX;
using NIntegrate.Scripts.Test.Demo.GoogleTracer.Views;
using JQuerySharp;
using System.DHTML;
using NIntegrate.Scripts.Test.Demo.GoogleTracer.Records;

namespace NIntegrate.Scripts.Test.Demo.GoogleTracer.Controllers
{
    public class GoogleTracerController
    {
        private IGoogleTracerView _view;

        #region Properties

        public IGoogleTracerView View
        {
            get
            {
                if (_view == null)
                    _view = (IGoogleTracerView)Container.GetInstance(typeof(IGoogleTracerView));
                return _view;
            }
        }

        #endregion

        #region Public Methods

        public void Execute()
        {
            View.ShowMoreResults += new DOMEventHandler(ShowMoreResults);

            LoadSearchResults();
        }

        public void ShowMoreResults()
        {
            View.SearchStart = View.SearchStart + 4;
            LoadSearchResults();
        }

        public static void GoogleWebSearchCallback(object data)
        {
            ((Dictionary)(object)Window.Self)["_googlewebsearchresults"] = data;
        }

        #endregion

        #region Private Methods

        private void LoadSearchResults()
        {
            if (string.IsNullOrEmpty(View.GetSearchKeyword().Trim()))
                return;

            jQuery.GetScript(
                string.Format(
                    SearchUrls.WEB_SEARCH_URL,
                    View.GetSearchKeyword().Replace("'", "").Replace(" ", "+").Replace("&", "").Replace("?", ""),
                    View.SearchStart,
                    "NIntegrate.Scripts.Test.Demo.GoogleTracer.Controllers.GoogleTracerController.googleWebSearchCallback"
                ),
                (Function)(object)new DOMEventHandler(delegate
                    {
                        View.RenderSearchResult((GoogleSearchResponse)((Dictionary)(object)Window.Self)["_googlewebsearchresults"]);
                    })
            );
        }

        #endregion
    }
}
