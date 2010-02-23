using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Drawing;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Serialization;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using System.Workflow.Runtime;
using System.Workflow.Activities;
using System.Workflow.Activities.Rules;
using EAppointments.BMS.Workflow.Interfaces;

namespace EAppointments.BMS.Workflow
{
	public sealed partial class AppointmentWorkflow: StateMachineWorkflowActivity
	{
		public AppointmentWorkflow()
		{
			InitializeComponent();
		}

        public AppointmentCreatedEventArgs createdArgs = default(AppointmentCreatedEventArgs);
        public AppointmentBookedEventArgs bookedArgs = default(AppointmentBookedEventArgs);
        public AppointmentCancelledEventArgs cancelledArgs = default(AppointmentCancelledEventArgs);
        public AppointmentReBookedEventArgs reBookedArgs = default(AppointmentReBookedEventArgs);
        public AppointmentApprovedEventArgs approvedArgs = default(AppointmentApprovedEventArgs);
        public AppointmentRejectedEventArgs rejectedArgs = default(AppointmentRejectedEventArgs);
        public AppointmentDeletedEventArgs deletedArgs = default(AppointmentDeletedEventArgs);
        
	}

}
