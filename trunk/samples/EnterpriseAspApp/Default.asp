<%@ Language="VBScript" %>
<%
    set logging = Server.CreateObject("EnterpriseServiceComWrapper.LoggingService")
    logging.WriteLog("ASP page start")
    
    'set query = Server.CreateObject("EnterpriseServiceComWrapper.ServiceQueryService")
    'set criteria = Server.CreateObject("EnterpriseAspNetAppQueryCriterias.ServiceCriteria")
    
    'response.Write criteria.SortBy(criteria.ServiceName, false)
    'criteria.SortBy(criteria.ServiceName, false)
    'set services = query.Select(criteria.ToBaseCriteria())
    'query.Dispose()
    
    'response.Write services
    
    set backComService = Server.CreateObject("EnterpriseServiceComWrapper.BackCompatibleService")
    set backComResult = backComService.GetCompatibleResult()
    response.Write backComResult.Value & backComResult.Value2 & backComService.SayHello() & "<br />"
    
    set backIncomService = Server.CreateObject("EnterpriseServiceComWrapper.BackIncompatibleService")
    set backIncomResultV1 = backIncomService.GetIncompatibleResult()
    response.Write backIncomResultV1.Value & "<br />"
    set backIncomResultV2 = backIncomService.GetIncompatibleResultV2()
    response.Write backIncomResultV2.Value & "<br />"
    
    logging.WriteLog("ASP page end")

    logging.Dispose()
    
    
    
    
%>