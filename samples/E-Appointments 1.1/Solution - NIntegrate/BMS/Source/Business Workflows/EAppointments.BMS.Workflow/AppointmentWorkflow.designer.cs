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
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Drawing;
using System.Reflection;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Serialization;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using System.Workflow.Runtime;
using System.Workflow.Activities;
using System.Workflow.Activities.Rules;

namespace EAppointments.BMS.Workflow
{
    partial class AppointmentWorkflow
    {
		#region Designer generated code
        
        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCode]
        private void InitializeComponent()
        {
            this.CanModifyActivities = true;
            System.Workflow.ComponentModel.ActivityBind activitybind1 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.WorkflowParameterBinding workflowparameterbinding1 = new System.Workflow.ComponentModel.WorkflowParameterBinding();
            System.Workflow.ComponentModel.ActivityBind activitybind2 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.WorkflowParameterBinding workflowparameterbinding2 = new System.Workflow.ComponentModel.WorkflowParameterBinding();
            System.Workflow.ComponentModel.ActivityBind activitybind3 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.WorkflowParameterBinding workflowparameterbinding3 = new System.Workflow.ComponentModel.WorkflowParameterBinding();
            System.Workflow.ComponentModel.ActivityBind activitybind4 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.WorkflowParameterBinding workflowparameterbinding4 = new System.Workflow.ComponentModel.WorkflowParameterBinding();
            System.Workflow.ComponentModel.ActivityBind activitybind5 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.WorkflowParameterBinding workflowparameterbinding5 = new System.Workflow.ComponentModel.WorkflowParameterBinding();
            System.Workflow.ComponentModel.ActivityBind activitybind6 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.WorkflowParameterBinding workflowparameterbinding6 = new System.Workflow.ComponentModel.WorkflowParameterBinding();
            System.Workflow.ComponentModel.ActivityBind activitybind7 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.WorkflowParameterBinding workflowparameterbinding7 = new System.Workflow.ComponentModel.WorkflowParameterBinding();
            System.Workflow.ComponentModel.ActivityBind activitybind8 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.WorkflowParameterBinding workflowparameterbinding8 = new System.Workflow.ComponentModel.WorkflowParameterBinding();
            System.Workflow.ComponentModel.ActivityBind activitybind9 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.WorkflowParameterBinding workflowparameterbinding9 = new System.Workflow.ComponentModel.WorkflowParameterBinding();
            System.Workflow.ComponentModel.ActivityBind activitybind10 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.WorkflowParameterBinding workflowparameterbinding10 = new System.Workflow.ComponentModel.WorkflowParameterBinding();
            System.Workflow.ComponentModel.ActivityBind activitybind11 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.WorkflowParameterBinding workflowparameterbinding11 = new System.Workflow.ComponentModel.WorkflowParameterBinding();
            System.Workflow.ComponentModel.ActivityBind activitybind12 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.WorkflowParameterBinding workflowparameterbinding12 = new System.Workflow.ComponentModel.WorkflowParameterBinding();
            System.Workflow.ComponentModel.ActivityBind activitybind13 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.WorkflowParameterBinding workflowparameterbinding13 = new System.Workflow.ComponentModel.WorkflowParameterBinding();
            System.Workflow.ComponentModel.ActivityBind activitybind14 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.WorkflowParameterBinding workflowparameterbinding14 = new System.Workflow.ComponentModel.WorkflowParameterBinding();
            System.Workflow.ComponentModel.ActivityBind activitybind15 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.WorkflowParameterBinding workflowparameterbinding15 = new System.Workflow.ComponentModel.WorkflowParameterBinding();
            System.Workflow.ComponentModel.ActivityBind activitybind16 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.WorkflowParameterBinding workflowparameterbinding16 = new System.Workflow.ComponentModel.WorkflowParameterBinding();
            System.Workflow.ComponentModel.ActivityBind activitybind17 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.WorkflowParameterBinding workflowparameterbinding17 = new System.Workflow.ComponentModel.WorkflowParameterBinding();
            System.Workflow.ComponentModel.ActivityBind activitybind18 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.WorkflowParameterBinding workflowparameterbinding18 = new System.Workflow.ComponentModel.WorkflowParameterBinding();
            System.Workflow.ComponentModel.ActivityBind activitybind19 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.WorkflowParameterBinding workflowparameterbinding19 = new System.Workflow.ComponentModel.WorkflowParameterBinding();
            System.Workflow.ComponentModel.ActivityBind activitybind20 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.WorkflowParameterBinding workflowparameterbinding20 = new System.Workflow.ComponentModel.WorkflowParameterBinding();
            System.Workflow.ComponentModel.ActivityBind activitybind21 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.WorkflowParameterBinding workflowparameterbinding21 = new System.Workflow.ComponentModel.WorkflowParameterBinding();
            System.Workflow.ComponentModel.ActivityBind activitybind22 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.WorkflowParameterBinding workflowparameterbinding22 = new System.Workflow.ComponentModel.WorkflowParameterBinding();
            System.Workflow.ComponentModel.ActivityBind activitybind23 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.WorkflowParameterBinding workflowparameterbinding23 = new System.Workflow.ComponentModel.WorkflowParameterBinding();
            System.Workflow.ComponentModel.ActivityBind activitybind24 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.WorkflowParameterBinding workflowparameterbinding24 = new System.Workflow.ComponentModel.WorkflowParameterBinding();
            System.Workflow.ComponentModel.ActivityBind activitybind25 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.WorkflowParameterBinding workflowparameterbinding25 = new System.Workflow.ComponentModel.WorkflowParameterBinding();
            System.Workflow.ComponentModel.ActivityBind activitybind26 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.WorkflowParameterBinding workflowparameterbinding26 = new System.Workflow.ComponentModel.WorkflowParameterBinding();
            System.Workflow.ComponentModel.ActivityBind activitybind27 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.WorkflowParameterBinding workflowparameterbinding27 = new System.Workflow.ComponentModel.WorkflowParameterBinding();
            System.Workflow.ComponentModel.ActivityBind activitybind28 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.WorkflowParameterBinding workflowparameterbinding28 = new System.Workflow.ComponentModel.WorkflowParameterBinding();
            System.Workflow.ComponentModel.ActivityBind activitybind29 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.WorkflowParameterBinding workflowparameterbinding29 = new System.Workflow.ComponentModel.WorkflowParameterBinding();
            System.Workflow.ComponentModel.ActivityBind activitybind30 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.WorkflowParameterBinding workflowparameterbinding30 = new System.Workflow.ComponentModel.WorkflowParameterBinding();
            System.Workflow.ComponentModel.ActivityBind activitybind31 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.WorkflowParameterBinding workflowparameterbinding31 = new System.Workflow.ComponentModel.WorkflowParameterBinding();
            System.Workflow.ComponentModel.ActivityBind activitybind32 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.WorkflowParameterBinding workflowparameterbinding32 = new System.Workflow.ComponentModel.WorkflowParameterBinding();
            System.Workflow.ComponentModel.ActivityBind activitybind33 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.WorkflowParameterBinding workflowparameterbinding33 = new System.Workflow.ComponentModel.WorkflowParameterBinding();
            System.Workflow.ComponentModel.ActivityBind activitybind34 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.WorkflowParameterBinding workflowparameterbinding34 = new System.Workflow.ComponentModel.WorkflowParameterBinding();
            this.setDeletedState2 = new System.Workflow.Activities.SetStateActivity();
            this.callDelete2 = new System.Workflow.Activities.CallExternalMethodActivity();
            this.handleDelete2 = new System.Workflow.Activities.HandleExternalEventActivity();
            this.setDeletedState = new System.Workflow.Activities.SetStateActivity();
            this.callDelete3 = new System.Workflow.Activities.CallExternalMethodActivity();
            this.handleDeleted3 = new System.Workflow.Activities.HandleExternalEventActivity();
            this.setCancelledState3 = new System.Workflow.Activities.SetStateActivity();
            this.callCancelled3 = new System.Workflow.Activities.CallExternalMethodActivity();
            this.handleCancelled3 = new System.Workflow.Activities.HandleExternalEventActivity();
            this.setApprovedState2 = new System.Workflow.Activities.SetStateActivity();
            this.callApprove2 = new System.Workflow.Activities.CallExternalMethodActivity();
            this.handleApprovedEvent2 = new System.Workflow.Activities.HandleExternalEventActivity();
            this.setCancelledState2 = new System.Workflow.Activities.SetStateActivity();
            this.callCancelled2 = new System.Workflow.Activities.CallExternalMethodActivity();
            this.handleCancelEvent2 = new System.Workflow.Activities.HandleExternalEventActivity();
            this.setRejectedState2 = new System.Workflow.Activities.SetStateActivity();
            this.callReject2 = new System.Workflow.Activities.CallExternalMethodActivity();
            this.handleRejected2 = new System.Workflow.Activities.HandleExternalEventActivity();
            this.setBookedState1 = new System.Workflow.Activities.SetStateActivity();
            this.callRebookMethod = new System.Workflow.Activities.CallExternalMethodActivity();
            this.handleRebookEvent = new System.Workflow.Activities.HandleExternalEventActivity();
            this.setCancelledState = new System.Workflow.Activities.SetStateActivity();
            this.callCancel = new System.Workflow.Activities.CallExternalMethodActivity();
            this.handleCancelEvent = new System.Workflow.Activities.HandleExternalEventActivity();
            this.setRejectedState = new System.Workflow.Activities.SetStateActivity();
            this.callReject = new System.Workflow.Activities.CallExternalMethodActivity();
            this.handleRejected = new System.Workflow.Activities.HandleExternalEventActivity();
            this.setApprovedState = new System.Workflow.Activities.SetStateActivity();
            this.callApprove = new System.Workflow.Activities.CallExternalMethodActivity();
            this.handleApprovedEvent = new System.Workflow.Activities.HandleExternalEventActivity();
            this.setAppointmentDeleted = new System.Workflow.Activities.SetStateActivity();
            this.callDelete = new System.Workflow.Activities.CallExternalMethodActivity();
            this.handleDeleteEvent = new System.Workflow.Activities.HandleExternalEventActivity();
            this.setBookedState = new System.Workflow.Activities.SetStateActivity();
            this.callBookMethod = new System.Workflow.Activities.CallExternalMethodActivity();
            this.handleBookEvent = new System.Workflow.Activities.HandleExternalEventActivity();
            this.setPendingState = new System.Workflow.Activities.SetStateActivity();
            this.callCreateMethod = new System.Workflow.Activities.CallExternalMethodActivity();
            this.handleCreateEvent = new System.Workflow.Activities.HandleExternalEventActivity();
            this.OnDeleted2 = new System.Workflow.Activities.EventDrivenActivity();
            this.onDeleted3 = new System.Workflow.Activities.EventDrivenActivity();
            this.OnCancelled3 = new System.Workflow.Activities.EventDrivenActivity();
            this.OnApproved2 = new System.Workflow.Activities.EventDrivenActivity();
            this.OnCancelled2 = new System.Workflow.Activities.EventDrivenActivity();
            this.OnRejected2 = new System.Workflow.Activities.EventDrivenActivity();
            this.onReBooked = new System.Workflow.Activities.EventDrivenActivity();
            this.OnCancelled = new System.Workflow.Activities.EventDrivenActivity();
            this.OnRejected = new System.Workflow.Activities.EventDrivenActivity();
            this.OnApproved = new System.Workflow.Activities.EventDrivenActivity();
            this.OnDeleted = new System.Workflow.Activities.EventDrivenActivity();
            this.OnBooked = new System.Workflow.Activities.EventDrivenActivity();
            this.InitializeAppointment = new System.Workflow.Activities.EventDrivenActivity();
            this.Cancelled = new System.Workflow.Activities.StateActivity();
            this.Rejected = new System.Workflow.Activities.StateActivity();
            this.Approved = new System.Workflow.Activities.StateActivity();
            this.Booked = new System.Workflow.Activities.StateActivity();
            this.Deleted = new System.Workflow.Activities.StateActivity();
            this.Pending = new System.Workflow.Activities.StateActivity();
            this.InitialState = new System.Workflow.Activities.StateActivity();
            // 
            // setDeletedState2
            // 
            this.setDeletedState2.Name = "setDeletedState2";
            this.setDeletedState2.TargetStateName = "Deleted";
            // 
            // callDelete2
            // 
            this.callDelete2.InterfaceType = typeof(EAppointments.BMS.Workflow.Interfaces.IAppointmentWorkflowService);
            this.callDelete2.MethodName = "Delete";
            this.callDelete2.Name = "callDelete2";
            activitybind1.Name = "AppointmentWorkflow";
            activitybind1.Path = "deletedArgs.Ubrn";
            workflowparameterbinding1.ParameterName = "ubrn";
            workflowparameterbinding1.SetBinding(System.Workflow.ComponentModel.WorkflowParameterBinding.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind1)));
            this.callDelete2.ParameterBindings.Add(workflowparameterbinding1);
            // 
            // handleDelete2
            // 
            this.handleDelete2.EventName = "AppointmentDeleted";
            this.handleDelete2.InterfaceType = typeof(EAppointments.BMS.Workflow.Interfaces.IAppointmentWorkflowService);
            this.handleDelete2.Name = "handleDelete2";
            activitybind2.Name = "AppointmentWorkflow";
            activitybind2.Path = "deletedArgs";
            workflowparameterbinding2.ParameterName = "e";
            workflowparameterbinding2.SetBinding(System.Workflow.ComponentModel.WorkflowParameterBinding.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind2)));
            this.handleDelete2.ParameterBindings.Add(workflowparameterbinding2);
            // 
            // setDeletedState
            // 
            this.setDeletedState.Name = "setDeletedState";
            this.setDeletedState.TargetStateName = "Deleted";
            // 
            // callDelete3
            // 
            this.callDelete3.InterfaceType = typeof(EAppointments.BMS.Workflow.Interfaces.IAppointmentWorkflowService);
            this.callDelete3.MethodName = "Delete";
            this.callDelete3.Name = "callDelete3";
            activitybind3.Name = "AppointmentWorkflow";
            activitybind3.Path = "deletedArgs.Ubrn";
            workflowparameterbinding3.ParameterName = "ubrn";
            workflowparameterbinding3.SetBinding(System.Workflow.ComponentModel.WorkflowParameterBinding.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind3)));
            this.callDelete3.ParameterBindings.Add(workflowparameterbinding3);
            // 
            // handleDeleted3
            // 
            this.handleDeleted3.EventName = "AppointmentDeleted";
            this.handleDeleted3.InterfaceType = typeof(EAppointments.BMS.Workflow.Interfaces.IAppointmentWorkflowService);
            this.handleDeleted3.Name = "handleDeleted3";
            activitybind4.Name = "AppointmentWorkflow";
            activitybind4.Path = "deletedArgs";
            workflowparameterbinding4.ParameterName = "e";
            workflowparameterbinding4.SetBinding(System.Workflow.ComponentModel.WorkflowParameterBinding.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind4)));
            this.handleDeleted3.ParameterBindings.Add(workflowparameterbinding4);
            // 
            // setCancelledState3
            // 
            this.setCancelledState3.Name = "setCancelledState3";
            this.setCancelledState3.TargetStateName = "Cancelled";
            // 
            // callCancelled3
            // 
            this.callCancelled3.InterfaceType = typeof(EAppointments.BMS.Workflow.Interfaces.IAppointmentWorkflowService);
            this.callCancelled3.MethodName = "Cancel";
            this.callCancelled3.Name = "callCancelled3";
            activitybind5.Name = "AppointmentWorkflow";
            activitybind5.Path = "cancelledArgs.Reason";
            workflowparameterbinding5.ParameterName = "reason";
            workflowparameterbinding5.SetBinding(System.Workflow.ComponentModel.WorkflowParameterBinding.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind5)));
            activitybind6.Name = "AppointmentWorkflow";
            activitybind6.Path = "cancelledArgs.Ubrn";
            workflowparameterbinding6.ParameterName = "ubrn";
            workflowparameterbinding6.SetBinding(System.Workflow.ComponentModel.WorkflowParameterBinding.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind6)));
            this.callCancelled3.ParameterBindings.Add(workflowparameterbinding5);
            this.callCancelled3.ParameterBindings.Add(workflowparameterbinding6);
            // 
            // handleCancelled3
            // 
            this.handleCancelled3.EventName = "AppointmentCancelled";
            this.handleCancelled3.InterfaceType = typeof(EAppointments.BMS.Workflow.Interfaces.IAppointmentWorkflowService);
            this.handleCancelled3.Name = "handleCancelled3";
            activitybind7.Name = "AppointmentWorkflow";
            activitybind7.Path = "cancelledArgs";
            workflowparameterbinding7.ParameterName = "e";
            workflowparameterbinding7.SetBinding(System.Workflow.ComponentModel.WorkflowParameterBinding.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind7)));
            this.handleCancelled3.ParameterBindings.Add(workflowparameterbinding7);
            // 
            // setApprovedState2
            // 
            this.setApprovedState2.Name = "setApprovedState2";
            this.setApprovedState2.TargetStateName = "Approved";
            // 
            // callApprove2
            // 
            this.callApprove2.InterfaceType = typeof(EAppointments.BMS.Workflow.Interfaces.IAppointmentWorkflowService);
            this.callApprove2.MethodName = "Approve";
            this.callApprove2.Name = "callApprove2";
            activitybind8.Name = "AppointmentWorkflow";
            activitybind8.Path = "approvedArgs.Ubrn";
            workflowparameterbinding8.ParameterName = "ubrn";
            workflowparameterbinding8.SetBinding(System.Workflow.ComponentModel.WorkflowParameterBinding.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind8)));
            this.callApprove2.ParameterBindings.Add(workflowparameterbinding8);
            // 
            // handleApprovedEvent2
            // 
            this.handleApprovedEvent2.EventName = "AppointmentApproved";
            this.handleApprovedEvent2.InterfaceType = typeof(EAppointments.BMS.Workflow.Interfaces.IAppointmentWorkflowService);
            this.handleApprovedEvent2.Name = "handleApprovedEvent2";
            activitybind9.Name = "AppointmentWorkflow";
            activitybind9.Path = "approvedArgs";
            workflowparameterbinding9.ParameterName = "e";
            workflowparameterbinding9.SetBinding(System.Workflow.ComponentModel.WorkflowParameterBinding.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind9)));
            this.handleApprovedEvent2.ParameterBindings.Add(workflowparameterbinding9);
            // 
            // setCancelledState2
            // 
            this.setCancelledState2.Name = "setCancelledState2";
            this.setCancelledState2.TargetStateName = "Cancelled";
            // 
            // callCancelled2
            // 
            this.callCancelled2.InterfaceType = typeof(EAppointments.BMS.Workflow.Interfaces.IAppointmentWorkflowService);
            this.callCancelled2.MethodName = "Cancel";
            this.callCancelled2.Name = "callCancelled2";
            activitybind10.Name = "AppointmentWorkflow";
            activitybind10.Path = "cancelledArgs.Reason";
            workflowparameterbinding10.ParameterName = "reason";
            workflowparameterbinding10.SetBinding(System.Workflow.ComponentModel.WorkflowParameterBinding.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind10)));
            activitybind11.Name = "AppointmentWorkflow";
            activitybind11.Path = "cancelledArgs.Ubrn";
            workflowparameterbinding11.ParameterName = "ubrn";
            workflowparameterbinding11.SetBinding(System.Workflow.ComponentModel.WorkflowParameterBinding.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind11)));
            this.callCancelled2.ParameterBindings.Add(workflowparameterbinding10);
            this.callCancelled2.ParameterBindings.Add(workflowparameterbinding11);
            // 
            // handleCancelEvent2
            // 
            this.handleCancelEvent2.EventName = "AppointmentCancelled";
            this.handleCancelEvent2.InterfaceType = typeof(EAppointments.BMS.Workflow.Interfaces.IAppointmentWorkflowService);
            this.handleCancelEvent2.Name = "handleCancelEvent2";
            activitybind12.Name = "AppointmentWorkflow";
            activitybind12.Path = "cancelledArgs";
            workflowparameterbinding12.ParameterName = "e";
            workflowparameterbinding12.SetBinding(System.Workflow.ComponentModel.WorkflowParameterBinding.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind12)));
            this.handleCancelEvent2.ParameterBindings.Add(workflowparameterbinding12);
            // 
            // setRejectedState2
            // 
            this.setRejectedState2.Name = "setRejectedState2";
            this.setRejectedState2.TargetStateName = "Rejected";
            // 
            // callReject2
            // 
            this.callReject2.InterfaceType = typeof(EAppointments.BMS.Workflow.Interfaces.IAppointmentWorkflowService);
            this.callReject2.MethodName = "Reject";
            this.callReject2.Name = "callReject2";
            activitybind13.Name = "AppointmentWorkflow";
            activitybind13.Path = "rejectedArgs.Ubrn";
            workflowparameterbinding13.ParameterName = "ubrn";
            workflowparameterbinding13.SetBinding(System.Workflow.ComponentModel.WorkflowParameterBinding.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind13)));
            this.callReject2.ParameterBindings.Add(workflowparameterbinding13);
            // 
            // handleRejected2
            // 
            this.handleRejected2.EventName = "AppointmentRejected";
            this.handleRejected2.InterfaceType = typeof(EAppointments.BMS.Workflow.Interfaces.IAppointmentWorkflowService);
            this.handleRejected2.Name = "handleRejected2";
            activitybind14.Name = "AppointmentWorkflow";
            activitybind14.Path = "rejectedArgs";
            workflowparameterbinding14.ParameterName = "e";
            workflowparameterbinding14.SetBinding(System.Workflow.ComponentModel.WorkflowParameterBinding.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind14)));
            this.handleRejected2.ParameterBindings.Add(workflowparameterbinding14);
            // 
            // setBookedState1
            // 
            this.setBookedState1.Name = "setBookedState1";
            this.setBookedState1.TargetStateName = "Booked";
            // 
            // callRebookMethod
            // 
            this.callRebookMethod.InterfaceType = typeof(EAppointments.BMS.Workflow.Interfaces.IAppointmentWorkflowService);
            this.callRebookMethod.MethodName = "ReBook";
            this.callRebookMethod.Name = "callRebookMethod";
            activitybind15.Name = "AppointmentWorkflow";
            activitybind15.Path = "reBookedArgs.NewSlotId";
            workflowparameterbinding15.ParameterName = "slotId";
            workflowparameterbinding15.SetBinding(System.Workflow.ComponentModel.WorkflowParameterBinding.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind15)));
            activitybind16.Name = "AppointmentWorkflow";
            activitybind16.Path = "reBookedArgs.Ubrn";
            workflowparameterbinding16.ParameterName = "ubrn";
            workflowparameterbinding16.SetBinding(System.Workflow.ComponentModel.WorkflowParameterBinding.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind16)));
            this.callRebookMethod.ParameterBindings.Add(workflowparameterbinding15);
            this.callRebookMethod.ParameterBindings.Add(workflowparameterbinding16);
            // 
            // handleRebookEvent
            // 
            this.handleRebookEvent.EventName = "AppointmentReBooked";
            this.handleRebookEvent.InterfaceType = typeof(EAppointments.BMS.Workflow.Interfaces.IAppointmentWorkflowService);
            this.handleRebookEvent.Name = "handleRebookEvent";
            activitybind17.Name = "AppointmentWorkflow";
            activitybind17.Path = "reBookedArgs";
            workflowparameterbinding17.ParameterName = "e";
            workflowparameterbinding17.SetBinding(System.Workflow.ComponentModel.WorkflowParameterBinding.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind17)));
            this.handleRebookEvent.ParameterBindings.Add(workflowparameterbinding17);
            // 
            // setCancelledState
            // 
            this.setCancelledState.Name = "setCancelledState";
            this.setCancelledState.TargetStateName = "Cancelled";
            // 
            // callCancel
            // 
            this.callCancel.InterfaceType = typeof(EAppointments.BMS.Workflow.Interfaces.IAppointmentWorkflowService);
            this.callCancel.MethodName = "Cancel";
            this.callCancel.Name = "callCancel";
            activitybind18.Name = "AppointmentWorkflow";
            activitybind18.Path = "cancelledArgs.Reason";
            workflowparameterbinding18.ParameterName = "reason";
            workflowparameterbinding18.SetBinding(System.Workflow.ComponentModel.WorkflowParameterBinding.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind18)));
            activitybind19.Name = "AppointmentWorkflow";
            activitybind19.Path = "cancelledArgs.Ubrn";
            workflowparameterbinding19.ParameterName = "ubrn";
            workflowparameterbinding19.SetBinding(System.Workflow.ComponentModel.WorkflowParameterBinding.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind19)));
            this.callCancel.ParameterBindings.Add(workflowparameterbinding18);
            this.callCancel.ParameterBindings.Add(workflowparameterbinding19);
            // 
            // handleCancelEvent
            // 
            this.handleCancelEvent.EventName = "AppointmentCancelled";
            this.handleCancelEvent.InterfaceType = typeof(EAppointments.BMS.Workflow.Interfaces.IAppointmentWorkflowService);
            this.handleCancelEvent.Name = "handleCancelEvent";
            activitybind20.Name = "AppointmentWorkflow";
            activitybind20.Path = "cancelledArgs";
            workflowparameterbinding20.ParameterName = "e";
            workflowparameterbinding20.SetBinding(System.Workflow.ComponentModel.WorkflowParameterBinding.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind20)));
            this.handleCancelEvent.ParameterBindings.Add(workflowparameterbinding20);
            // 
            // setRejectedState
            // 
            this.setRejectedState.Name = "setRejectedState";
            this.setRejectedState.TargetStateName = "Rejected";
            // 
            // callReject
            // 
            this.callReject.InterfaceType = typeof(EAppointments.BMS.Workflow.Interfaces.IAppointmentWorkflowService);
            this.callReject.MethodName = "Reject";
            this.callReject.Name = "callReject";
            activitybind21.Name = "AppointmentWorkflow";
            activitybind21.Path = "rejectedArgs.Ubrn";
            workflowparameterbinding21.ParameterName = "ubrn";
            workflowparameterbinding21.SetBinding(System.Workflow.ComponentModel.WorkflowParameterBinding.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind21)));
            this.callReject.ParameterBindings.Add(workflowparameterbinding21);
            // 
            // handleRejected
            // 
            this.handleRejected.EventName = "AppointmentRejected";
            this.handleRejected.InterfaceType = typeof(EAppointments.BMS.Workflow.Interfaces.IAppointmentWorkflowService);
            this.handleRejected.Name = "handleRejected";
            activitybind22.Name = "AppointmentWorkflow";
            activitybind22.Path = "rejectedArgs";
            workflowparameterbinding22.ParameterName = "e";
            workflowparameterbinding22.SetBinding(System.Workflow.ComponentModel.WorkflowParameterBinding.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind22)));
            this.handleRejected.ParameterBindings.Add(workflowparameterbinding22);
            // 
            // setApprovedState
            // 
            this.setApprovedState.Name = "setApprovedState";
            this.setApprovedState.TargetStateName = "Approved";
            // 
            // callApprove
            // 
            this.callApprove.InterfaceType = typeof(EAppointments.BMS.Workflow.Interfaces.IAppointmentWorkflowService);
            this.callApprove.MethodName = "Approve";
            this.callApprove.Name = "callApprove";
            activitybind23.Name = "AppointmentWorkflow";
            activitybind23.Path = "approvedArgs.Ubrn";
            workflowparameterbinding23.ParameterName = "ubrn";
            workflowparameterbinding23.SetBinding(System.Workflow.ComponentModel.WorkflowParameterBinding.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind23)));
            this.callApprove.ParameterBindings.Add(workflowparameterbinding23);
            // 
            // handleApprovedEvent
            // 
            this.handleApprovedEvent.EventName = "AppointmentApproved";
            this.handleApprovedEvent.InterfaceType = typeof(EAppointments.BMS.Workflow.Interfaces.IAppointmentWorkflowService);
            this.handleApprovedEvent.Name = "handleApprovedEvent";
            activitybind24.Name = "AppointmentWorkflow";
            activitybind24.Path = "approvedArgs";
            workflowparameterbinding24.ParameterName = "e";
            workflowparameterbinding24.SetBinding(System.Workflow.ComponentModel.WorkflowParameterBinding.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind24)));
            this.handleApprovedEvent.ParameterBindings.Add(workflowparameterbinding24);
            // 
            // setAppointmentDeleted
            // 
            this.setAppointmentDeleted.Name = "setAppointmentDeleted";
            this.setAppointmentDeleted.TargetStateName = "Deleted";
            // 
            // callDelete
            // 
            this.callDelete.InterfaceType = typeof(EAppointments.BMS.Workflow.Interfaces.IAppointmentWorkflowService);
            this.callDelete.MethodName = "Delete";
            this.callDelete.Name = "callDelete";
            activitybind25.Name = "AppointmentWorkflow";
            activitybind25.Path = "deletedArgs.Ubrn";
            workflowparameterbinding25.ParameterName = "ubrn";
            workflowparameterbinding25.SetBinding(System.Workflow.ComponentModel.WorkflowParameterBinding.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind25)));
            this.callDelete.ParameterBindings.Add(workflowparameterbinding25);
            // 
            // handleDeleteEvent
            // 
            this.handleDeleteEvent.EventName = "AppointmentDeleted";
            this.handleDeleteEvent.InterfaceType = typeof(EAppointments.BMS.Workflow.Interfaces.IAppointmentWorkflowService);
            this.handleDeleteEvent.Name = "handleDeleteEvent";
            activitybind26.Name = "AppointmentWorkflow";
            activitybind26.Path = "deletedArgs";
            workflowparameterbinding26.ParameterName = "e";
            workflowparameterbinding26.SetBinding(System.Workflow.ComponentModel.WorkflowParameterBinding.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind26)));
            this.handleDeleteEvent.ParameterBindings.Add(workflowparameterbinding26);
            // 
            // setBookedState
            // 
            this.setBookedState.Name = "setBookedState";
            this.setBookedState.TargetStateName = "Booked";
            // 
            // callBookMethod
            // 
            this.callBookMethod.InterfaceType = typeof(EAppointments.BMS.Workflow.Interfaces.IAppointmentWorkflowService);
            this.callBookMethod.MethodName = "Book";
            this.callBookMethod.Name = "callBookMethod";
            activitybind27.Name = "AppointmentWorkflow";
            activitybind27.Path = "bookedArgs.SlotId";
            workflowparameterbinding27.ParameterName = "slotId";
            workflowparameterbinding27.SetBinding(System.Workflow.ComponentModel.WorkflowParameterBinding.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind27)));
            activitybind28.Name = "AppointmentWorkflow";
            activitybind28.Path = "bookedArgs.Ubrn";
            workflowparameterbinding28.ParameterName = "ubrn";
            workflowparameterbinding28.SetBinding(System.Workflow.ComponentModel.WorkflowParameterBinding.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind28)));
            this.callBookMethod.ParameterBindings.Add(workflowparameterbinding27);
            this.callBookMethod.ParameterBindings.Add(workflowparameterbinding28);
            // 
            // handleBookEvent
            // 
            this.handleBookEvent.EventName = "AppointmentBooked";
            this.handleBookEvent.InterfaceType = typeof(EAppointments.BMS.Workflow.Interfaces.IAppointmentWorkflowService);
            this.handleBookEvent.Name = "handleBookEvent";
            activitybind29.Name = "AppointmentWorkflow";
            activitybind29.Path = "bookedArgs";
            workflowparameterbinding29.ParameterName = "e";
            workflowparameterbinding29.SetBinding(System.Workflow.ComponentModel.WorkflowParameterBinding.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind29)));
            this.handleBookEvent.ParameterBindings.Add(workflowparameterbinding29);
            // 
            // setPendingState
            // 
            this.setPendingState.Name = "setPendingState";
            this.setPendingState.TargetStateName = "Pending";
            // 
            // callCreateMethod
            // 
            this.callCreateMethod.InterfaceType = typeof(EAppointments.BMS.Workflow.Interfaces.IAppointmentWorkflowService);
            this.callCreateMethod.MethodName = "Create";
            this.callCreateMethod.Name = "callCreateMethod";
            activitybind30.Name = "AppointmentWorkflow";
            activitybind30.Path = "createdArgs.ClinicTypeId";
            workflowparameterbinding30.ParameterName = "clinicTypeId";
            workflowparameterbinding30.SetBinding(System.Workflow.ComponentModel.WorkflowParameterBinding.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind30)));
            activitybind31.Name = "AppointmentWorkflow";
            activitybind31.Path = "createdArgs.PatientId";
            workflowparameterbinding31.ParameterName = "patientId";
            workflowparameterbinding31.SetBinding(System.Workflow.ComponentModel.WorkflowParameterBinding.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind31)));
            activitybind32.Name = "AppointmentWorkflow";
            activitybind32.Path = "createdArgs.ProviderId";
            workflowparameterbinding32.ParameterName = "providerId";
            workflowparameterbinding32.SetBinding(System.Workflow.ComponentModel.WorkflowParameterBinding.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind32)));
            activitybind33.Name = "AppointmentWorkflow";
            activitybind33.Path = "createdArgs.InstanceId";
            workflowparameterbinding33.ParameterName = "workflowInstanceId";
            workflowparameterbinding33.SetBinding(System.Workflow.ComponentModel.WorkflowParameterBinding.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind33)));
            this.callCreateMethod.ParameterBindings.Add(workflowparameterbinding30);
            this.callCreateMethod.ParameterBindings.Add(workflowparameterbinding31);
            this.callCreateMethod.ParameterBindings.Add(workflowparameterbinding32);
            this.callCreateMethod.ParameterBindings.Add(workflowparameterbinding33);
            // 
            // handleCreateEvent
            // 
            this.handleCreateEvent.EventName = "AppointmentCreated";
            this.handleCreateEvent.InterfaceType = typeof(EAppointments.BMS.Workflow.Interfaces.IAppointmentWorkflowService);
            this.handleCreateEvent.Name = "handleCreateEvent";
            activitybind34.Name = "AppointmentWorkflow";
            activitybind34.Path = "createdArgs";
            workflowparameterbinding34.ParameterName = "e";
            workflowparameterbinding34.SetBinding(System.Workflow.ComponentModel.WorkflowParameterBinding.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind34)));
            this.handleCreateEvent.ParameterBindings.Add(workflowparameterbinding34);
            // 
            // OnDeleted2
            // 
            this.OnDeleted2.Activities.Add(this.handleDelete2);
            this.OnDeleted2.Activities.Add(this.callDelete2);
            this.OnDeleted2.Activities.Add(this.setDeletedState2);
            this.OnDeleted2.Name = "OnDeleted2";
            // 
            // onDeleted3
            // 
            this.onDeleted3.Activities.Add(this.handleDeleted3);
            this.onDeleted3.Activities.Add(this.callDelete3);
            this.onDeleted3.Activities.Add(this.setDeletedState);
            this.onDeleted3.Name = "onDeleted3";
            // 
            // OnCancelled3
            // 
            this.OnCancelled3.Activities.Add(this.handleCancelled3);
            this.OnCancelled3.Activities.Add(this.callCancelled3);
            this.OnCancelled3.Activities.Add(this.setCancelledState3);
            this.OnCancelled3.Name = "OnCancelled3";
            // 
            // OnApproved2
            // 
            this.OnApproved2.Activities.Add(this.handleApprovedEvent2);
            this.OnApproved2.Activities.Add(this.callApprove2);
            this.OnApproved2.Activities.Add(this.setApprovedState2);
            this.OnApproved2.Name = "OnApproved2";
            // 
            // OnCancelled2
            // 
            this.OnCancelled2.Activities.Add(this.handleCancelEvent2);
            this.OnCancelled2.Activities.Add(this.callCancelled2);
            this.OnCancelled2.Activities.Add(this.setCancelledState2);
            this.OnCancelled2.Name = "OnCancelled2";
            // 
            // OnRejected2
            // 
            this.OnRejected2.Activities.Add(this.handleRejected2);
            this.OnRejected2.Activities.Add(this.callReject2);
            this.OnRejected2.Activities.Add(this.setRejectedState2);
            this.OnRejected2.Name = "OnRejected2";
            // 
            // onReBooked
            // 
            this.onReBooked.Activities.Add(this.handleRebookEvent);
            this.onReBooked.Activities.Add(this.callRebookMethod);
            this.onReBooked.Activities.Add(this.setBookedState1);
            this.onReBooked.Name = "onReBooked";
            // 
            // OnCancelled
            // 
            this.OnCancelled.Activities.Add(this.handleCancelEvent);
            this.OnCancelled.Activities.Add(this.callCancel);
            this.OnCancelled.Activities.Add(this.setCancelledState);
            this.OnCancelled.Name = "OnCancelled";
            // 
            // OnRejected
            // 
            this.OnRejected.Activities.Add(this.handleRejected);
            this.OnRejected.Activities.Add(this.callReject);
            this.OnRejected.Activities.Add(this.setRejectedState);
            this.OnRejected.Name = "OnRejected";
            // 
            // OnApproved
            // 
            this.OnApproved.Activities.Add(this.handleApprovedEvent);
            this.OnApproved.Activities.Add(this.callApprove);
            this.OnApproved.Activities.Add(this.setApprovedState);
            this.OnApproved.Name = "OnApproved";
            // 
            // OnDeleted
            // 
            this.OnDeleted.Activities.Add(this.handleDeleteEvent);
            this.OnDeleted.Activities.Add(this.callDelete);
            this.OnDeleted.Activities.Add(this.setAppointmentDeleted);
            this.OnDeleted.Name = "OnDeleted";
            // 
            // OnBooked
            // 
            this.OnBooked.Activities.Add(this.handleBookEvent);
            this.OnBooked.Activities.Add(this.callBookMethod);
            this.OnBooked.Activities.Add(this.setBookedState);
            this.OnBooked.Name = "OnBooked";
            // 
            // InitializeAppointment
            // 
            this.InitializeAppointment.Activities.Add(this.handleCreateEvent);
            this.InitializeAppointment.Activities.Add(this.callCreateMethod);
            this.InitializeAppointment.Activities.Add(this.setPendingState);
            this.InitializeAppointment.Name = "InitializeAppointment";
            // 
            // Cancelled
            // 
            this.Cancelled.Activities.Add(this.OnDeleted2);
            this.Cancelled.Name = "Cancelled";
            // 
            // Rejected
            // 
            this.Rejected.Activities.Add(this.OnApproved2);
            this.Rejected.Activities.Add(this.OnCancelled3);
            this.Rejected.Activities.Add(this.onDeleted3);
            this.Rejected.Name = "Rejected";
            // 
            // Approved
            // 
            this.Approved.Activities.Add(this.OnRejected2);
            this.Approved.Activities.Add(this.OnCancelled2);
            this.Approved.Name = "Approved";
            // 
            // Booked
            // 
            this.Booked.Activities.Add(this.OnApproved);
            this.Booked.Activities.Add(this.OnRejected);
            this.Booked.Activities.Add(this.OnCancelled);
            this.Booked.Activities.Add(this.onReBooked);
            this.Booked.Name = "Booked";
            // 
            // Deleted
            // 
            this.Deleted.Name = "Deleted";
            // 
            // Pending
            // 
            this.Pending.Activities.Add(this.OnBooked);
            this.Pending.Activities.Add(this.OnDeleted);
            this.Pending.Name = "Pending";
            // 
            // InitialState
            // 
            this.InitialState.Activities.Add(this.InitializeAppointment);
            this.InitialState.Name = "InitialState";
            // 
            // AppointmentWorkflow
            // 
            this.Activities.Add(this.InitialState);
            this.Activities.Add(this.Pending);
            this.Activities.Add(this.Deleted);
            this.Activities.Add(this.Booked);
            this.Activities.Add(this.Approved);
            this.Activities.Add(this.Rejected);
            this.Activities.Add(this.Cancelled);
            this.CompletedStateName = "Deleted";
            this.DynamicUpdateCondition = null;
            this.InitialStateName = "InitialState";
            this.Name = "AppointmentWorkflow";
            this.CanModifyActivities = false;

        }

        #endregion

        private EventDrivenActivity onDeleted3;
        private SetStateActivity setDeletedState;
        private CallExternalMethodActivity callDelete3;
        private HandleExternalEventActivity handleDeleted3;
        private EventDrivenActivity onReBooked;
        private HandleExternalEventActivity handleRebookEvent;
        private SetStateActivity setBookedState1;
        private CallExternalMethodActivity callRebookMethod;
        private CallExternalMethodActivity callCreateMethod;
        private CallExternalMethodActivity callBookMethod;
        private HandleExternalEventActivity handleBookEvent;
        private HandleExternalEventActivity handleDeleteEvent;
        private CallExternalMethodActivity callDelete;
        private HandleExternalEventActivity handleApprovedEvent;
        private CallExternalMethodActivity callApprove;
        private CallExternalMethodActivity callReject;
        private HandleExternalEventActivity handleRejected;
        private CallExternalMethodActivity callCancel;
        private HandleExternalEventActivity handleCancelEvent;
        private CallExternalMethodActivity callDelete2;
        private HandleExternalEventActivity handleDelete2;
        private CallExternalMethodActivity callReject2;
        private HandleExternalEventActivity handleRejected2;
        private CallExternalMethodActivity callCancelled2;
        private HandleExternalEventActivity handleCancelEvent2;
        private CallExternalMethodActivity callApprove2;
        private HandleExternalEventActivity handleApprovedEvent2;
        private CallExternalMethodActivity callCancelled3;
        private HandleExternalEventActivity handleCancelled3;
        private HandleExternalEventActivity handleCreateEvent;
        private EventDrivenActivity InitializeAppointment;
        private StateActivity Pending;
        private SetStateActivity setBookedState;
        private SetStateActivity setPendingState;
        private EventDrivenActivity OnDeleted;
        private EventDrivenActivity OnBooked;
        private StateActivity Deleted;
        private StateActivity Booked;
        private SetStateActivity setAppointmentDeleted;
        private EventDrivenActivity OnCancelled;
        private EventDrivenActivity OnRejected;
        private EventDrivenActivity OnApproved;
        private StateActivity Approved;
        private EventDrivenActivity OnCancelled2;
        private EventDrivenActivity OnRejected2;
        private StateActivity Rejected;
        private EventDrivenActivity OnCancelled3;
        private EventDrivenActivity OnApproved2;
        private SetStateActivity setRejectedState;
        private SetStateActivity setApprovedState;
        private SetStateActivity setCancelledState;
        private EventDrivenActivity OnDeleted2;
        private StateActivity Cancelled;
        private SetStateActivity setRejectedState2;
        private SetStateActivity setCancelledState2;
        private SetStateActivity setDeletedState2;
        private SetStateActivity setApprovedState2;
        private SetStateActivity setCancelledState3;
        private StateActivity InitialState;





































































































































































































    }
}
