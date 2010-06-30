using System;
using System.DHTML;
using ScriptFX;
using ScriptFX.UI;

namespace JQuerySharp
{
	public delegate void EmptyEventHandler();

	/// <include file="documentation.xml" path="/documentation/member[@name='jQuery']"/>  
	[IgnoreNamespace]
	[Imported]
	public class jQuery
	{
		// Constructors
		public jQuery() { }
		public jQuery(string selector) { }
		public jQuery(DOMDocument document) { }

		//
		// Attributes
		//

		/// <include file="documentation.xml" path="/documentation/member[@name='jQuery.AddClass']"/>
		public jQuery AddClass(string className) { return new jQuery(); }
		public jQuery Attr(string attribute) { return new jQuery(); }
		public jQuery Attr(Dictionary properties) { return new jQuery(); }
		public jQuery Attr(string key, string value) { return new jQuery(); }
		public jQuery Attr(string key, EmptyEventHandler handler) { return new jQuery(); }
		public jQuery Attr(string key, Function function) { return new jQuery(); }
		public jQuery Html() { return new jQuery(); }
		public jQuery Html(string val) { return new jQuery(); }
		public jQuery RemoveAttr(string name) { return new jQuery(); }
		public jQuery RemoveClass(string name) { return new jQuery(); }
		public jQuery Text() { return new jQuery(); }
		public jQuery Text(string val) { return new jQuery(); }
		public jQuery ToggleClass(string className) { return new jQuery(); }
		public jQuery Val() { return new jQuery(); }
		public jQuery Val(string val) { return new jQuery(); }

		//
		// Manipulation
		//
		public jQuery After(string content) { return new jQuery(); }
		public jQuery After(jQuery content) { return new jQuery(); }
		public jQuery Append(string content) { return new jQuery(); }
		public jQuery Append(jQuery content) { return new jQuery(); }
		public jQuery AppendTo(string content) { return new jQuery(); }
		public jQuery AppendTo(jQuery content) { return new jQuery(); }
		public jQuery Before(string content) { return new jQuery(); }
		public jQuery Before(jQuery content) { return new jQuery(); }
		public jQuery Clone() { return new jQuery(); }
		public jQuery Clone(bool deep) { return new jQuery(); }
		public jQuery Empty() { return new jQuery(); }
		public jQuery InsertAfter(string content) { return new jQuery(); }
		public jQuery InsertAfter(jQuery content) { return new jQuery(); }
		public jQuery InsertBefore(string content) { return new jQuery(); }
		public jQuery InsertBefore(jQuery content) { return new jQuery(); }
		public jQuery Prepend(string content) { return new jQuery(); }
		public jQuery Prepend(jQuery content) { return new jQuery(); }
		public jQuery PrependTo(string content) { return new jQuery(); }
		public jQuery PrependTo(jQuery content) { return new jQuery(); }
		public jQuery Remove(string expr) { return new jQuery(); }
		public jQuery ReplaceAll(string selector) { return new jQuery(); }
		public jQuery ReplaceWith(string content) { return new jQuery(); }
		public jQuery Wrap(string html) { return new jQuery(); }
		public jQuery Wrap(DOMElement element) { return new jQuery(); }
		public jQuery WrapInner(DOMElement element) { return new jQuery(); }
		public jQuery WrapInner(string html) { return new jQuery(); }
		public jQuery WrapAll(string html) { return new jQuery(); }

		//
		// Traversing
		//
		public jQuery Add(string expr) { return new jQuery(); } // expr can be html too
		public jQuery Add(DOMElement element) { return new jQuery(); }
		public jQuery Add(DOMElement[] elements) { return new jQuery(); }
		public jQuery AndSelf() { return new jQuery(); }
		public jQuery Children(string expr) { return new jQuery(); }
		public jQuery Closest(string expr) { return new jQuery(); }
		public jQuery Contains(string str) { return new jQuery(); }
		public jQuery Contents() { return new jQuery(); }
		public jQuery End() { return new jQuery(); }
		public jQuery Filter(string expression) { return new jQuery(); }
		public jQuery Filter(EmptyEventHandler handler) { return new jQuery(); }
		public jQuery Filter(Function function) { return new jQuery(); }
		public jQuery Find(string expr) { return new jQuery(); }
		public jQuery HasClass(string className) { return new jQuery(); }
		public jQuery Is(string expr) { return new jQuery(); }
		public jQuery Next(string expr) { return new jQuery(); }
		public jQuery NextAll(string expr) { return new jQuery(); }
		public jQuery Not(DOMElement element) { return new jQuery(); }
		public jQuery Not(string expr) { return new jQuery(); }
		public jQuery Not(jQuery jquery) { return new jQuery(); }
		public jQuery OffsetParent() { return new jQuery(); }
		public jQuery Parent(string expr) { return new jQuery(); }
		public jQuery Parents(string expr) { return new jQuery(); }
		public jQuery Prev(string expr) { return new jQuery(); }
		public jQuery PrevAll(string expr) { return new jQuery(); }
		public jQuery Siblings(string expr) { return new jQuery(); }

		//
		// CSS
		//
		public jQuery Css(string name) { return new jQuery(); }
		public jQuery Css(Dictionary map) { return new jQuery(); }
		public jQuery Css(string key, string value) { return new jQuery(); }
		public jQuery Css(string key, Number value) { return new jQuery(); }
		public jQuery Height() { return new jQuery(); }
		public jQuery Height(string val) { return new jQuery(); }
		public jQuery Height(Number val) { return new jQuery(); }
		public jQuery Width() { return new jQuery(); }
		public jQuery Width(string val) { return new jQuery(); }
		public jQuery Width(int val) { return new jQuery(); }

		//
		// Javascript
		//
		public static Browser Browser;
		public static jQuery Each(Object o, Function function) { return new jQuery(); }
		public static jQuery Each(Object o, EmptyEventHandler handler) { return new jQuery(); }
		public static Object Extend(Object target, Object prop1, Object prop2) { return new Object(); }
		public static ArrayList Grep(ArrayList array, Function function, bool inv) { return new ArrayList(); }
		public static ArrayList Grep(ArrayList array, EmptyEventHandler handler, bool inv) { return new ArrayList(); }
		public static ArrayList Map(ArrayList array, Function function) { return new ArrayList(); }
		public static ArrayList Map(ArrayList array, EmptyEventHandler handler) { return new ArrayList(); }
		public static ArrayList Merge(ArrayList first, ArrayList second) { return new ArrayList(); }
		public jQuery Slice(Number start, Number end) { return new jQuery(); }
		public static string Trim(string str) { return str.Trim(); }

		//
		// Events
		//
		public jQuery Bind(string eventName, DOMEventHandler handler) { return this; }
		public jQuery Bind(string eventName, Object data, DOMEventHandler handler) { return this; }
		public jQuery Bind(string eventName, Function fn) { return this; }
		public jQuery Bind(string eventName, Object data, Function fn) { return this; }
		public jQuery Blur() { return new jQuery(); }
		public jQuery Blur(Function fn) { return new jQuery(); }
		public jQuery Blur(DOMEventHandler handler) { return new jQuery(); }
		public jQuery Change(Function fn) { return new jQuery(); }
		public jQuery Change(DOMEventHandler handler) { return new jQuery(); }
		public jQuery Click() { return new jQuery(); }
		public jQuery Click(Function fn) { return new jQuery(); }
		public jQuery Click(DOMEventHandler handler) { return new jQuery(); }
		public jQuery DblClick(Function fn) { return new jQuery(); }
		public jQuery DblClick(DOMEventHandler handler) { return new jQuery(); }
		public jQuery Error(Function fn) { return new jQuery(); }
		public jQuery Error(DOMEventHandler handler) { return new jQuery(); }
		public jQuery Focus() { return new jQuery(); }
		public jQuery Focus(Function fn) { return new jQuery(); }
		public jQuery Focus(DOMEventHandler handler) { return new jQuery(); }
		public jQuery Hover(Function over, Function fnOut) { return new jQuery(); }
		public jQuery Hover(DOMEventHandler handlerOver, DOMEventHandler handlerOut) { return new jQuery(); }
		public jQuery Keydown(Function fn) { return new jQuery(); }
		public jQuery Keydown(DOMEventHandler handler) { return new jQuery(); }
		public jQuery Keypress(Function fn) { return new jQuery(); }
		public jQuery Keypress(DOMEventHandler handler) { return new jQuery(); }
		public jQuery Keyup(Function fn) { return new jQuery(); }
		public jQuery Keyup(DOMEventHandler handler) { return new jQuery(); }
		public jQuery Load(Function fn) { return new jQuery(); }
		public jQuery Load(DOMEventHandler handler) { return new jQuery(); }
		public jQuery Mousedown(Function fn) { return new jQuery(); }
		public jQuery Mousedown(DOMEventHandler handler) { return new jQuery(); }
		public jQuery Mousemove(Function fn) { return new jQuery(); }
		public jQuery Mousemove(DOMEventHandler handler) { return new jQuery(); }
		public jQuery Mouseout(Function fn) { return new jQuery(); }
		public jQuery Mouseout(DOMEventHandler handler) { return new jQuery(); }
		public jQuery Mouseover(Function fn) { return new jQuery(); }
		public jQuery Mouseover(DOMEventHandler handler) { return new jQuery(); }
		public jQuery Mouseup(Function fn) { return new jQuery(); }
		public jQuery Mouseup(DOMEventHandler handler) { return new jQuery(); }
		public jQuery One(string fnType, object data, Function fn) { return new jQuery(); }
		public jQuery One(string fnType, object data, DOMEventHandler handler) { return new jQuery(); }
		public jQuery Ready(Function fn) { return new jQuery(); }
		public jQuery Ready(DOMEventHandler handler) { return new jQuery(); }
		public jQuery Resize(Function fn) { return new jQuery(); }
		public jQuery Resize(DOMEventHandler handler) { return new jQuery(); }
		public jQuery Scroll(Function fn) { return new jQuery(); }
		public jQuery Scroll(DOMEventHandler handler) { return new jQuery(); }
		public jQuery Select() { return new jQuery(); }
		public jQuery Select(Function fn) { return new jQuery(); }
		public jQuery Select(DOMEventHandler handler) { return new jQuery(); }
		public jQuery Submit() { return new jQuery(); }
		public jQuery Submit(Function fn) { return new jQuery(); }
		public jQuery Submit(DOMEventHandler handler) { return new jQuery(); }
		public jQuery Toggle(Function even, Function odd) { return new jQuery(); }
		public jQuery Toggle(DOMEventHandler handler) { return new jQuery(); }
		public jQuery Trigger(string fnType, ArrayList data) { return new jQuery(); }
		public jQuery Unbind(string fnType, Function fn) { return new jQuery(); }
		public jQuery Unbind(string fnType, DOMEventHandler handler) { return new jQuery(); }
		public jQuery Unload(Function fn) { return new jQuery(); }
		public jQuery Unload(DOMEventHandler handler) { return new jQuery(); }

		//
		// Effects
		//
		public jQuery Animate(Dictionary hash, string speed, string easing, Function callback) { return new jQuery(); }
		public jQuery Animate(Dictionary hash, Number speed, string easing, Function callback) { return new jQuery(); }
		public jQuery Animate(Dictionary hash, string speed, string easing, EmptyEventHandler handler) { return new jQuery(); }
		public jQuery Animate(Dictionary hash, Number speed, string easing, EmptyEventHandler handler) { return new jQuery(); }

		public jQuery FadeIn(string speed, Function callback) { return new jQuery(); }
		public jQuery FadeIn(Number speed, Function callback) { return new jQuery(); }
		public jQuery FadeIn(string speed, EmptyEventHandler handler) { return new jQuery(); }
		public jQuery FadeIn(Number speed, EmptyEventHandler handler) { return new jQuery(); }

		public jQuery FadeOut(string speed, Function callback) { return new jQuery(); }
		public jQuery FadeOut(Number speed, Function callback) { return new jQuery(); }
		public jQuery FadeOut(string speed, EmptyEventHandler handler) { return new jQuery(); }
		public jQuery FadeOut(Number speed, EmptyEventHandler handler) { return new jQuery(); }

		public jQuery FadeTo(string speed, float opacity, Function callback) { return new jQuery(); }
		public jQuery FadeTo(Number speed, float opacity, Function callback) { return new jQuery(); }
		public jQuery FadeTo(string speed, float opacity, EmptyEventHandler handler) { return new jQuery(); }
		public jQuery FadeTo(Number speed, float opacity, EmptyEventHandler handler) { return new jQuery(); }

		public jQuery Hide() { return new jQuery(); }
		public jQuery Hide(string speed, Function callback) { return new jQuery(); }
		public jQuery Hide(Number speed, Function callback) { return new jQuery(); }
		public jQuery Hide(string speed, EmptyEventHandler handler) { return new jQuery(); }
		public jQuery Hide(Number speed, EmptyEventHandler handler) { return new jQuery(); }

		public jQuery Show() { return new jQuery(); }
		public jQuery Show(string speed, Function callback) { return new jQuery(); }
		public jQuery Show(Number speed, Function callback) { return new jQuery(); }
		public jQuery Show(string speed, EmptyEventHandler handler) { return new jQuery(); }
		public jQuery Show(Number speed, EmptyEventHandler handler) { return new jQuery(); }

		public jQuery SlideDown(string speed, Function callback) { return new jQuery(); }
		public jQuery SlideDown(Number speed, Function callback) { return new jQuery(); }
		public jQuery SlideDown(string speed, EmptyEventHandler handler) { return new jQuery(); }
		public jQuery SlideDown(Number speed, EmptyEventHandler handler) { return new jQuery(); }

		public jQuery SlideToggle(string speed, Function callback) { return new jQuery(); }
		public jQuery SlideToggle(Number speed, Function callback) { return new jQuery(); }
		public jQuery SlideToggle(string speed, EmptyEventHandler handler) { return new jQuery(); }
		public jQuery SlideToggle(Number speed, EmptyEventHandler handler) { return new jQuery(); }

		public jQuery SlideUp(string speed, Function callback) { return new jQuery(); }
		public jQuery SlideUp(Number speed, Function callback) { return new jQuery(); }
		public jQuery SlideUp(string speed, EmptyEventHandler handler) { return new jQuery(); }
		public jQuery SlideUp(Number speed, EmptyEventHandler handler) { return new jQuery(); }
		public jQuery Toggle() { return new jQuery(); }

		//
		// Ajax
		//
		public static XMLHttpRequest Ajax(Dictionary properties) { return new XMLHttpRequest(); }
		public jQuery AjaxComplete(Function callback) { return new jQuery(); }
		public jQuery AjaxComplete(EmptyEventHandler handler) { return new jQuery(); }
		public jQuery AjaxError(Function callback) { return new jQuery(); }
		public jQuery AjaxError(EmptyEventHandler handler) { return new jQuery(); }
		public jQuery AjaxSend(Function callback) { return new jQuery(); }
		public jQuery AjaxSend(EmptyEventHandler handler) { return new jQuery(); }
		public static void AjaxSetup(Dictionary map) { }
		public jQuery AjaxStart(Function callback) { return new jQuery(); }
		public jQuery AjaxStart(EmptyEventHandler handler) { return new jQuery(); }
		public jQuery AjaxStop(Function callback) { return new jQuery(); }
		public jQuery AjaxStop(EmptyEventHandler handler) { return new jQuery(); }
		public jQuery AjaxSuccess(Function callback) { return new jQuery(); }
		public jQuery AjaxSuccess(EmptyEventHandler handler) { return new jQuery(); }
		public static void AjaxTimeout(Number time) { }

		public static XMLHttpRequest Get(string url, Dictionary map, Function callback) { return new XMLHttpRequest(); }
		public static XMLHttpRequest GetIfModified(string url, Dictionary map, Function callback) { return new XMLHttpRequest(); }
		public static XMLHttpRequest GetJSON(string url, Dictionary map, Function callback) { return new XMLHttpRequest(); }
		public static XMLHttpRequest GetScript(string url, Function callback) { return new XMLHttpRequest(); }

		public static jQuery Load(string url, Dictionary parameters, Function callback) { return new jQuery(); }
		public static jQuery LoadIfModified(string url, Dictionary map, Function callback) { return new jQuery(); }
		public static XMLHttpRequest Post(string url, Dictionary map, Function callback) { return new XMLHttpRequest(); }
		public static string Serialize() { return new String(); }
	}

	[IgnoreNamespace]
	[Imported]
	public class Browser
	{
		public bool Safari;
		public bool Opera;
		public bool Msie;
		public bool Mozilla;
	}
}
