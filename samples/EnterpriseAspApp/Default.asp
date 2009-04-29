<%@ Language="VBScript" %>
<%
    set logging = Server.CreateObject("EnterpriseServiceComWrapper.LoggingService")
    logging.WriteLog("This is a log from ASP page.")
    logging.Dispose()
    
    
    
    
%>