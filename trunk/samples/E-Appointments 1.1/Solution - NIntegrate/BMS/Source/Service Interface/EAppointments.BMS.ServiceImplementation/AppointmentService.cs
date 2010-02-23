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
using System.ServiceModel;
using System.IdentityModel.Claims;
using System.Security.Permissions;
using System.Workflow.Activities;
using System.Workflow.Runtime;
using EAppointments.BMS;
using EAppointments.BMS.DataTypes;
using EAppointments.BMS.ServiceContracts;
using EAppointments.BMS.ServiceImplementation.Security;
using EAppointments.BMS.Workflow.Interfaces;
using WFExtensions;
using EAppointments.BMS.Workflow;
using System.Collections.ObjectModel;
using System.Workflow.Runtime.Hosting;
using System.Threading;
using System.Runtime.Remoting.Messaging;
using NIntegrate.Data;
using System.Data;
using NIntegrate.Mapping;

namespace EAppointments.BMS.ServiceImplementation
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class AppointmentService : IAppointmentService, IDisposable
    {
        private WorkflowRuntime _workflowRuntime;
        private AppointmentWorkflowService _appointmentLocalService;

        private static readonly QueryCommandFactory _cmdFac = new QueryCommandFactory();
        private static readonly MapperFactory _mapperFac = new MapperFactory();

        [ClaimsPrincipalPermissionAttribute(SecurityAction.Demand, RequiredClaimType = AppointmentClaimTypes.Read, Resource = Resources.Appointment)]
        public AppointmentCollection Query(QueryCriteria criteria)
        {
            if (criteria == null)
                throw new ArgumentNullException("criteria");

            var cmd = _cmdFac.CreateCommand(criteria, false);
            using (var conn = cmd.Connection)
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                using (var rdr = cmd.ExecuteReader())
                {
                    var mapper = _mapperFac.GetMapper<IDataReader, List<DataTypes.Appointment>>();
                    var list = mapper(rdr);
                    return new AppointmentCollection(list);
                }
            }
        }

        [ClaimsPrincipalPermissionAttribute(SecurityAction.Demand, RequiredClaimType = AppointmentClaimTypes.Read, Resource = Resources.Appointment)]
        public AppointmentCollection Find(DataTypes.AppointmentSearchCriteria criteria)
        {
            if (criteria == null)
                throw new ArgumentNullException("criteria");

            BMS.AppointmentSearchCriteria businessCriteria = AppointmentTranslator.TranslateSearchCriteria(criteria);
            
            List<BMS.Appointment> businessAppointments = new List<BMS.Appointment>(Helper.GetApplication().Find(businessCriteria));
            
            return new AppointmentCollection(businessAppointments.ConvertAll<DataTypes.Appointment>( 
                new Converter<BMS.Appointment, DataTypes.Appointment> (AppointmentTranslator.TranslateBusinessToService)
                ));
        }

        [ClaimsPrincipalPermissionAttribute(SecurityAction.Demand, RequiredClaimType = AppointmentClaimTypes.Create, Resource = Resources.Appointment)]
        public DataTypes.Appointment Save(DataTypes.Appointment appointment)
        {
            if (appointment == null)
                throw new ArgumentNullException("appointment");
            
            SetupWorkflowEnvironment();

            BMS.Appointment[] businessAppointments = Helper.GetApplication().Find(new AppointmentSearchCriteria(appointment.Ubrn));
            
            BMS.Appointment businessAppointment;
            if (businessAppointments.Length > 0)
            {
                // Existing Appointment
                // No need to use the workflow as no state change happening.
                businessAppointment = businessAppointments[0];
                businessAppointment = AppointmentTranslator.TranslateServiceToBusiness(appointment, businessAppointment);                
                businessAppointment.Save();
            }
            else
            {
                // New Appointment
                if (appointment.Patient == null || appointment.Provider == null || appointment.ClinicType == null)
                    throw new ArgumentNullException("appointment");
                
                Guid workflowInstanceId = Guid.NewGuid();

                WorkflowInstance workflowInstance =
                   _workflowRuntime.CreateWorkflow(typeof(AppointmentWorkflow), null, workflowInstanceId);
                workflowInstance.Start();

                _appointmentLocalService.RaiseAppointmentCreateEvent
                    (workflowInstanceId, appointment.Patient.Id, appointment.Provider.Id, appointment.ClinicType.Id);

                RunWorkFlow(workflowInstanceId);

                AppointmentSearchCriteria criteria = new AppointmentSearchCriteria(null);
                criteria.WorkflowId = workflowInstanceId;
                businessAppointment = Helper.GetApplication().Find(criteria)[0];
            }

            return AppointmentTranslator.TranslateBusinessToService(businessAppointment);
        }

        [ClaimsPrincipalPermissionAttribute(SecurityAction.Demand, RequiredClaimType = AppointmentClaimTypes.Book, Resource = Resources.Appointment)]
        public void Book(int ubrn, Guid slotId)
        {
            Appointment businessAppointment = Helper.GetApplication().FindAppointmentById(ubrn);

            EnsureTransitionPossible("Booked", (Guid)businessAppointment.WorkflowId);
            
            _appointmentLocalService.RaiseAppointmentBookEvent
                ((Guid)businessAppointment.WorkflowId, ubrn, slotId);

            RunWorkFlow((Guid)businessAppointment.WorkflowId);
        }

        [ClaimsPrincipalPermissionAttribute(SecurityAction.Demand, RequiredClaimType = AppointmentClaimTypes.Cancel, Resource = Resources.Appointment)]
        public void Cancel(int ubrn, String cancellationReason)
        {
            Appointment businessAppointment = Helper.GetApplication().FindAppointmentById(ubrn);

            EnsureTransitionPossible("Cancelled", (Guid)businessAppointment.WorkflowId);

            _appointmentLocalService.RaiseAppointmentCancelEvent
                ((Guid)businessAppointment.WorkflowId, ubrn, cancellationReason);

            RunWorkFlow((Guid)businessAppointment.WorkflowId);
        }

        [ClaimsPrincipalPermissionAttribute(SecurityAction.Demand, RequiredClaimType = AppointmentClaimTypes.Rebook, Resource = Resources.Appointment)]
        public void Rebook(int ubrn, Guid slotId)
        {
            Appointment businessAppointment = Helper.GetApplication().FindAppointmentById(ubrn);

            // Ensure Current state is Booked
            StateMachineWorkflowInstance stateMachine = new StateMachineWorkflowInstance(_workflowRuntime, (Guid)businessAppointment.WorkflowId);
            if (stateMachine.CurrentStateName != "Booked")
               throw new InvalidBusinessOperationException("This operation is only valid for booked appointments");
            
            EnsureTransitionPossible("Booked", (Guid)businessAppointment.WorkflowId);

            _appointmentLocalService.RaiseAppointmentReBookEvent
                ((Guid)businessAppointment.WorkflowId, ubrn, slotId);

            RunWorkFlow((Guid)businessAppointment.WorkflowId);
        }

        [ClaimsPrincipalPermissionAttribute(SecurityAction.Demand, RequiredClaimType = AppointmentClaimTypes.Approve, Resource = Resources.Appointment)]
        public void Approve(int ubrn)
        {
           Appointment businessAppointment = Helper.GetApplication().FindAppointmentById(ubrn);

            EnsureTransitionPossible("Approved", (Guid)businessAppointment.WorkflowId);

            _appointmentLocalService.RaiseAppointmentApproveEvent
                 ((Guid)businessAppointment.WorkflowId, ubrn);

            RunWorkFlow((Guid)businessAppointment.WorkflowId);
        }

        [ClaimsPrincipalPermissionAttribute(SecurityAction.Demand, RequiredClaimType = AppointmentClaimTypes.Reject, Resource = Resources.Appointment)]
        public void Reject(int ubrn)
        {
            Appointment businessAppointment = Helper.GetApplication().FindAppointmentById(ubrn);

            EnsureTransitionPossible("Rejected", (Guid)businessAppointment.WorkflowId);

            _appointmentLocalService.RaiseAppointmentRejectEvent
                ((Guid)businessAppointment.WorkflowId, ubrn);

            RunWorkFlow((Guid)businessAppointment.WorkflowId);
        }

        [ClaimsPrincipalPermissionAttribute(SecurityAction.Demand, RequiredClaimType = AppointmentClaimTypes.Delete, Resource = Resources.Appointment)]
        public void Delete(int ubrn)
        {
            Appointment businessAppointment = Helper.GetApplication().FindAppointmentById(ubrn);

            EnsureTransitionPossible("Deleted", (Guid)businessAppointment.WorkflowId);

            _appointmentLocalService.RaiseAppointmentDeleteEvent
                 ((Guid)businessAppointment.WorkflowId, ubrn);

            RunWorkFlow((Guid)businessAppointment.WorkflowId);
        }

        /// <summary>
        /// Ensures that the instance of AppointmentWorkFlowService has been initialized
        /// and added to the workflow's data exchange
        /// </summary>
        /// <remarks>
        /// There will only be a single instance of this object in activation
        /// at any point in time.
        /// </remarks>
        private void SetupWorkflowEnvironment()
        {
            if (_appointmentLocalService == null)
            {
                WfWcfExtension extension =
                    OperationContext.Current.Host.Extensions.Find<WfWcfExtension>();

                _workflowRuntime = extension.WorkflowRuntime;

                // We will try and fetch the instance from the active WorkflowRuntime's
                // ExtenalDataExchangeService and if it currently does not have an
                // activated instance, we will create a new instance and add it.

                ExternalDataExchangeService dataExchangeService =
                    _workflowRuntime.GetService<ExternalDataExchangeService>();

                _appointmentLocalService =
                    (AppointmentWorkflowService)dataExchangeService.GetService(typeof(AppointmentWorkflowService));

                if (_appointmentLocalService == null)
                {
                    _appointmentLocalService = new AppointmentWorkflowService();
                    dataExchangeService.AddService(_appointmentLocalService);
                }
            }
        }

        /// <summary>
        /// Ensures that an appropriate exception is thrown in case the state machine 
        /// encounters an invalid state transition. The exceptions that the workflow runtime 
        /// current throws ("Event Delivery failed") is not helpful
        /// </summary>
        /// <param name="stateToTransition">The operation which has been called which is the state to which the 
        /// transition will occur (if valid)</param>
        /// <param name="workflowInstanceId">Guid - workflow instance Id</param>
        private void EnsureTransitionPossible(string stateToTransition, Guid workflowInstanceId)
        {
            SetupWorkflowEnvironment();
            
            StateMachineWorkflowInstance stateMachine = new StateMachineWorkflowInstance(_workflowRuntime, workflowInstanceId);
            
            if (!stateMachine.PossibleStateTransitions.Contains(stateToTransition))
            {
                string[] transitions = new string[stateMachine.PossibleStateTransitions.Count];
                stateMachine.PossibleStateTransitions.CopyTo(transitions, 0);

                string message = String.Format("The appointment can be {0} but not {1}", string.Join(",",transitions), stateToTransition);
                throw new InvalidBusinessOperationException(message);
            }
        }

        /// <summary>
        /// Run the workflow in a synchronous way using the ManualWorkflowSchedulerService
        /// </summary>
        /// <param name="workflowInstanceId"></param>
        private void RunWorkFlow(Guid workflowInstanceId)
        {
            StateMachineWorkflowInstance stateMachine = new StateMachineWorkflowInstance(_workflowRuntime, workflowInstanceId);
            StateActivity oldState = stateMachine.CurrentState;

            ManualWorkflowSchedulerService service = _workflowRuntime.GetService<ManualWorkflowSchedulerService>();
            service.RunWorkflow(workflowInstanceId);

            // Hack : This is not the best way to handle exceptions (sychronously) in Workflows
            // Read more about the best practice here - http://forums.microsoft.com/MSDN/ShowPost.aspx?PostID=833203&SiteID=1    
            // And here - http://www.topxml.com/rbnews/Orchestration---Workflow/re-60988_Synchronous-CallWorkflow-sample-revisited.aspx
            if (CallContext.GetData("Exception") != null)
            {
                // This is the compensation activity - that should be ideally defined in the workflow itself
                // In our case, the only activity that occurs after External Method Calls for Book, Cancel etc.
                // is the state change activity. We just reverse it here.
                // Ideally, we can have a fault handler for each event driven activity
                // that will set the call context with a "WCFException" and halt the execution of the workflow.
                stateMachine.SetState(oldState);
               
                service.RunWorkflow(workflowInstanceId);

                Exception exceptionToThrow = (Exception)CallContext.GetData("Exception");

                CallContext.SetData("Exception", null);
               
                throw exceptionToThrow;             
            }
        }
        
        /// <summary>
        /// Releases the resources used by AppointmentService.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                this._workflowRuntime.Dispose();
            }
        }
    }
}
