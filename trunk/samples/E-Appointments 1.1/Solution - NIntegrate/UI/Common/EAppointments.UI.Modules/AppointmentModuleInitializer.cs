//===============================================================================
// Microsoft Aspiring Software Architects Program
// E-Appointments - Case Study Implementation
//===============================================================================
// Copyright  Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================
// The example companies, organizations, products, domain names,
// e-mail addresses, logos, people, places, and events depicted
// herein are fictitious.  No association with any real company,
// organization, product, domain name, email address, logo, person,
// places, or events is intended or should be inferred.
//===============================================================================

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.CompositeWeb;
using Microsoft.Practices.CompositeWeb.Authorization;
using Microsoft.Practices.CompositeWeb.Configuration;
using Microsoft.Practices.CompositeWeb.Interfaces;
using Microsoft.Practices.PageFlow;
using Microsoft.Practices.PageFlow.WorkflowFoundation;
using EAppointments.UI.ServiceAgents.Interfaces;
using EAppointments.UI.ServiceAgents;
using System.Configuration;
using EAppointments.UI.Modules.Services;

namespace EAppointments.UI.Modules
{
    public class DefaultModuleInitializer : ModuleInitializer
    {
        private const string AuthorizationSection = "compositeWeb/authorization";

        public override void Load(ICompositionContainer container)
        {
            base.Load(container);

            AddModuleServices(container.Services);
            // TODO
            // RegisterRequiredPermissions(container.Services.Get<IPermissionsCatalog>(true));
            // RegisterSiteMapInformation(container.Services.Get<ISiteMapBuilderService>(true));

            RegisterPageFlows();
        }

        protected virtual void AddModuleServices(IServiceCollection moduleServices)
        {
            moduleServices.AddNew<AppointmentServiceAgent, IAppointmentServiceAgent>();
            moduleServices.AddNew<DirectoryServiceAgent, IDirectoryServiceAgent>();
            moduleServices.AddNew<ProviderServiceAgent, IProviderServiceAgent>();
            moduleServices.AddNew<WebSessionStateProvider, IStateProvider>();
            moduleServices.AddNew<WebNavigationService, INavigationService>();    
        }

        protected virtual void RegisterSiteMapInformation(ISiteMapBuilderService siteMapBuilderService)
        {
            SiteMapNodeInfo moduleRoot =
                new SiteMapNodeInfo("Appointment", "~/Appointment/Default.aspx", "E-Appointments - Dashboard",
                                    "The E-Appointments Dashboard", null, null, null, null);
            SiteMapNodeInfo createAppointmentNode =
                new SiteMapNodeInfo("Create", "~/Appointment/Create.aspx", "New Appointment",
                                    "Creates a new appointment");

            // TODO: Add the remaining nodes...
            siteMapBuilderService.AddNode(moduleRoot, 1);
            siteMapBuilderService.AddNode(createAppointmentNode, moduleRoot, "AllowCreateAppointment");
        }

        protected virtual void RegisterPageFlows()
        {
            // TODO: Add Page Flow
        }

        protected virtual void RegisterRequiredPermissions(IPermissionsCatalog permissionsCatalog)
        {
            // TODO: Add the remaining permissions
            
            //Action allowCreateAppointment = new Action("Allow Create Appointments", Permissions.AllowCreateAppointment);
            //List<Action> moduleActions = new List<Action>();
            //moduleActions.Add(allowCreateAppointment);
            //ModuleActionSet set = new ModuleActionSet("Appointment Creation", moduleActions);
            //permissionsCatalog.RegisterPermissionSet(set);
        }

        public override void Configure(IServiceCollection services, Configuration moduleConfiguration)
        {
            // Configure module authorization if needed
            IAuthorizationRulesService authorizationRuleService = services.Get<IAuthorizationRulesService>();
            if (authorizationRuleService != null)
            {
                AuthorizationConfigurationSection authorizationSection =
                    moduleConfiguration.GetSection(AuthorizationSection) as AuthorizationConfigurationSection;
                if (authorizationSection != null)
                {
                    foreach (AuthorizationRuleElement ruleElement in authorizationSection.ModuleRules)
                    {
                        authorizationRuleService.RegisterAuthorizationRule(ruleElement.AbsolutePath, ruleElement.RuleName);
                    }
                }
            }
        }
    }
}
