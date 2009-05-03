<%@ Language="VBScript" %>
<%
    set logging = Server.CreateObject("EnterpriseServiceComWrapper.LoggingService")
    logging.WriteLog("ASP page start")
    
    set criteria = Server.CreateObject("EnterpriseAspNetAppQueryCriterias.ServiceCriteria")
    set query = Server.CreateObject("EnterpriseServiceComWrapper.QueryService")
    
    set table = query.Select(criteria.AddSortBy(criteria.ServiceName, false).And(criteria.Service_id.LessThan(3)))
    query.Dispose()
    set services = Server.CreateObject("EnterpriseServiceComWrapper.ServiceCollection")
    services.Load(table)
        
    response.Write services.Count & "<br />"

    set backComService = Server.CreateObject("EnterpriseServiceComWrapper.BackCompatibleService")
    set backComResult = backComService.GetCompatibleResult()
    response.Write backComResult.Value & "|" & backComResult.Value2 & "|" & backComService.SayHello() & "<br />"
    
    set backIncomService = Server.CreateObject("EnterpriseServiceComWrapper.BackIncompatibleService")
    set backIncomResult = backIncomService.GetIncompatibleResult()
    response.Write backIncomResult.Value & "<br />"
    
    logging.WriteLog("ASP page end")

    logging.Dispose()
%>