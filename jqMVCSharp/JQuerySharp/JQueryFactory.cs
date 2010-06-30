using System;
using System.DHTML;
using ScriptFX;
using ScriptFX.UI;

namespace JQuerySharp
{
	[IgnoreNamespace]
	[Imported]
	[GlobalMethods]
	public static class JQueryFactory
	{
		public static jQuery JQuery() { return JQuery(); }
		public static jQuery JQuery(string selector) { return new jQuery(selector); }
		public static jQuery JQuery(DOMDocument document) { return new jQuery(document); }
	}
}
