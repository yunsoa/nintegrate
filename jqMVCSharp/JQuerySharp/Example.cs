using System;
using System.DHTML;
using ScriptFX;
using ScriptFX.UI;
using JQuerySharp;

namespace JQuerySharp
{
	public class Example
	{
		public void Run()
		{
			// 1. Basic example
			jQuery jq = JQueryFactory.JQuery("#test");

			// 2. Testing the browser
			if (jQuery.Browser.Msie)
				Script.Alert("You're using IE");

			// 3. A click
			JQueryFactory.JQuery("#clickme").Click(new DOMEventHandler(OnClick));

			// 4. Bind to a custom event
			jq.Bind("paste", new DOMEventHandler(OnPaste));
			
			// 5. Another event
			JQueryFactory.JQuery().Ready(new DOMEventHandler(OnReady));

			// 6. Ajax with its JSON properties. Ajax support hasn't been tested (yet) and lacks event handler equivalents.
			Dictionary properties = new Dictionary("url", "YourPage.aspx/YourMethod", "type", "POST", "dataType", "xml", "data", "val1=x&val2=x", "processData", false);
			
			// Note the two different syntaxes - this requires an instance
			JQueryFactory.JQuery().AjaxError(new EmptyEventHandler(OnAjaxError));

			// This is a static method, no instance required.
			jQuery.Ajax(properties);
		}

		public void OnClick()
		{
			// Without using a variable
			JQueryFactory.JQuery("#newcontent").Html("<h1>new stuff here</h1>");

			// Cloning
			jQuery test = JQueryFactory.JQuery("#cloneme").Clone();

			// Chaining together
			jQuery jq = JQueryFactory.JQuery("#newcontent");
			jq.Attr("style", "color:red").Append("<p>This comes from attr</p>");
			jq.Append(test);
		}

		public void OnAjaxError()
		{
			Script.Alert("An AJAX error occured");
		}

		public void OnReady()
		{
			Script.Alert("Called from ready");

			// Plugin example
			AccordionPlugin plugin = (AccordionPlugin)JQueryFactory.JQuery("#test");
			plugin.Accordion();
		}

		public void OnPaste()
		{
			// Handle paste
		}
	}
}