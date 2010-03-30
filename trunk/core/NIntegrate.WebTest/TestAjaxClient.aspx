<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestAjaxClient.aspx.cs" Inherits="NIntegrate.WebTest.TestAjaxClient" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <script language="javascript" type="text/javascript" src="Scripts/jquery-1.4.2.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:ScriptManager ID="dm" runat="server">
        <Services>
            <asp:ServiceReference Path="http://localhost:2166/TestAjaxClientService.svc" />
        </Services>
    </asp:ScriptManager>
    <script language="javascript" type="text/javascript">
        var svc = new tempuri.org.TestAjaxClientService();
        svc.Hello(function(result) { alert('called by ASP.NET AJAX: ' + result); });

        jQuery.getJSON('http://localhost:2166/TestAjaxClientService.svc/Hello', function(data) { alert('called by jQuery: ' + data.d); });

        jQuery.getJSON('http://127.0.0.1:2166/TestAjaxClientService.svc/Hello?jsoncallback=?', function(data) { alert('called by jQuery through JSONP protocol (no cache): ' + data.d); });

        function jsonpCallback(data) {
            alert('called by jQuery through JSONP protocol (cached): ' + data.d);
        }

        jQuery.ajaxSetup({ cache: true });
        jQuery.getScript('http://127.0.0.1:2166/TestAjaxClientService.svc/Hello?jsoncallback=jsonpCallback');
        jQuery.ajaxSetup({ cache: false });

//        function _isArray(obj) {
//            if (obj.constructor.toString().indexOf("Array") == -1)
//                return false;
//            else
//                return true;
//        }

//        function _isDictionary(obj) {
//            if (obj && _isArray(obj) && obj.length > 0) {
//                var properties = new Array();
//                for (var property in obj[0]) {
//                    properties.push(property);
//                }

//                if (properties.length == 2
//                    && properties[0] == "Key"
//                    && properties[1] == "Value") {
//                    return true;
//                }
//            }

//            return false;
//        }

//        function tryCreateDictionary(data) {
//            if (data) {
//                if (_isDictionary(data)) {
//                    var dic = new Hashtable();
//                    dic.isDictionary = function() { return true; };
//                    for (var i = 0; i < data.length; ++i) {
//                        dic.put(data[i].Key, tryCreateDictionary(data[i].Value));
//                    }
//                    return dic;
//                }
//                else if (_isArray(data)) {
//                    data.isArray = function() { return true; };
//                    for (var i = 0; i < data.length; ++i) {
//                        data[i] = tryCreateDictionary(data[i]);
//                    }
//                }
//                return data;
//            }

//            return {};
//        }

//        function enumDict(data) {
//            var dic = tryCreateDictionary(data.d);
//            alert(dic.get(1).Data);
//            alert(dic.get(2).Status);

//        }        

    </script>
    </div>
    </form>
</body>
</html>
