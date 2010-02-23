USE [EAppointments]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Appointment_Patient]') AND parent_object_id = OBJECT_ID(N'[dbo].[Appointment]'))
ALTER TABLE [dbo].[Appointment] DROP CONSTRAINT [FK_Appointment_Patient]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Appointment_Referrer]') AND parent_object_id = OBJECT_ID(N'[dbo].[Appointment]'))
ALTER TABLE [dbo].[Appointment] DROP CONSTRAINT [FK_Appointment_Referrer]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ClinicType_Specialty]') AND parent_object_id = OBJECT_ID(N'[dbo].[ClinicType]'))
ALTER TABLE [dbo].[ClinicType] DROP CONSTRAINT [FK_ClinicType_Specialty]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProviderClinicType_Provider]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProviderClinicType]'))
ALTER TABLE [dbo].[ProviderClinicType] DROP CONSTRAINT [FK_ProviderClinicType_Provider]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ServiceClinicType_ClinicType]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProviderClinicType]'))
ALTER TABLE [dbo].[ProviderClinicType] DROP CONSTRAINT [FK_ServiceClinicType_ClinicType]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Session_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[Session]'))
ALTER TABLE [dbo].[Session] DROP CONSTRAINT [FK_Session_User]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Slot_ProviderClinicType]') AND parent_object_id = OBJECT_ID(N'[dbo].[Slot]'))
ALTER TABLE [dbo].[Slot] DROP CONSTRAINT [FK_Slot_ProviderClinicType]
GO
/****** Object:  Table [dbo].[Appointment]    Script Date: 09/25/2007 13:13:15 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Appointment]') AND type in (N'U'))
DROP TABLE [dbo].[Appointment]
GO
/****** Object:  Table [dbo].[ClinicType]    Script Date: 09/25/2007 13:13:15 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ClinicType]') AND type in (N'U'))
DROP TABLE [dbo].[ClinicType]
GO
/****** Object:  Table [dbo].[Patient]    Script Date: 09/25/2007 13:13:15 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Patient]') AND type in (N'U'))
DROP TABLE [dbo].[Patient]
GO
/****** Object:  Table [dbo].[ProviderClinicType]    Script Date: 09/25/2007 13:13:15 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProviderClinicType]') AND type in (N'U'))
DROP TABLE [dbo].[ProviderClinicType]
GO
/****** Object:  Table [dbo].[Referrer]    Script Date: 09/25/2007 13:13:15 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Referrer]') AND type in (N'U'))
DROP TABLE [dbo].[Referrer]
GO
/****** Object:  Table [dbo].[Session]    Script Date: 09/25/2007 13:13:15 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Session]') AND type in (N'U'))
DROP TABLE [dbo].[Session]
GO
/****** Object:  Table [dbo].[Slot]    Script Date: 09/25/2007 13:13:15 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Slot]') AND type in (N'U'))
DROP TABLE [dbo].[Slot]
GO
/****** Object:  Table [dbo].[Specialty]    Script Date: 09/25/2007 13:13:15 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Specialty]') AND type in (N'U'))
DROP TABLE [dbo].[Specialty]
GO
/****** Object:  Table [dbo].[User]    Script Date: 09/25/2007 13:13:15 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[User]') AND type in (N'U'))
DROP TABLE [dbo].[User]
GO
/****** Object:  Table [dbo].[ZipCode]    Script Date: 09/25/2007 13:13:15 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ZipCode]') AND type in (N'U'))
DROP TABLE [dbo].[ZipCode]
GO
/****** Object:  Table [dbo].[Provider]    Script Date: 09/25/2007 13:13:15 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Provider]') AND type in (N'U'))
DROP TABLE [dbo].[Provider]
GO
/****** Object:  Table [dbo].[Appointment]    Script Date: 09/25/2007 13:13:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Appointment]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Appointment](
	[UBRN] [int] NOT NULL,
	[PatientId] [uniqueidentifier] NOT NULL,
	[ReferrerId] [uniqueidentifier] NOT NULL,
	[ProviderId] [uniqueidentifier] NOT NULL,
	[SlotId] [uniqueidentifier] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[StartDateTime] [datetime] NULL,
	[EndDateTime] [datetime] NULL,
	[UpdatedDate] [datetime] NOT NULL,
	[CancelledBy] [uniqueidentifier] NULL,
	[CancelledDate] [datetime] NULL,
	[CancellationReason] [nvarchar](100) NULL,
	[Status] [int] NOT NULL,
	[Comments] [nvarchar](max) NULL,
	[ReminderDate] [datetime] NULL,
	[WorkflowId] [uniqueidentifier] NULL,
	[ClinicTypeId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Appointment] PRIMARY KEY CLUSTERED 
(
	[UBRN] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Booking Reference Number - Primary Key for the appointment table. Format NNNN-NNNN-NNNN. Stored in the database without the dashes' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Appointment', @level2type=N'COLUMN', @level2name=N'UBRN'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier for a patient' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Appointment', @level2type=N'COLUMN', @level2name=N'PatientId'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Referrer who referred the patient' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Appointment', @level2type=N'COLUMN', @level2name=N'ReferrerId'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Service Provider selected by the referrer (in consultation with the patient)' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Appointment', @level2type=N'COLUMN', @level2name=N'ProviderId'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Slot identifier at the Service Provider end.' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Appointment', @level2type=N'COLUMN', @level2name=N'SlotId'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date of creation of the appointment' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Appointment', @level2type=N'COLUMN', @level2name=N'CreatedDate'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Start Date and Time of the appointment' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Appointment', @level2type=N'COLUMN', @level2name=N'StartDateTime'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'End Date and Time of the appointment' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Appointment', @level2type=N'COLUMN', @level2name=N'EndDateTime'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Last Updated Date and Time of the appointment' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Appointment', @level2type=N'COLUMN', @level2name=N'UpdatedDate'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Referrer who cancelled the appointment. Can be null' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Appointment', @level2type=N'COLUMN', @level2name=N'CancelledBy'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date of cancellation of the appointment. Can be null' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Appointment', @level2type=N'COLUMN', @level2name=N'CancelledDate'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Reason for cancelling the appointment. Can be null' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Appointment', @level2type=N'COLUMN', @level2name=N'CancellationReason'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Appointment Status - Booked, Pending, Cancelled' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Appointment', @level2type=N'COLUMN', @level2name=N'Status'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Appointment Table' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Appointment'

GO
/****** Object:  Table [dbo].[ClinicType]    Script Date: 09/25/2007 13:13:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ClinicType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ClinicType](
	[Id] [uniqueidentifier] NOT NULL,
	[SpecialtyId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_ClinicType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Clinic Type Primary Key' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ClinicType', @level2type=N'COLUMN', @level2name=N'Id'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Parent Speciality Id' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ClinicType', @level2type=N'COLUMN', @level2name=N'SpecialtyId'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name of the clinic type' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ClinicType', @level2type=N'COLUMN', @level2name=N'Name'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Clinic Types Lookup Table. Also known as sub-specialities' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ClinicType'

GO
/****** Object:  Table [dbo].[Patient]    Script Date: 09/25/2007 13:13:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Patient]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Patient](
	[Id] [uniqueidentifier] NOT NULL,
	[PatientNo] [nchar](10) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Gender] [nchar](1) NOT NULL,
	[DOB] [datetime] NULL,
	[AddressLine1] [nvarchar](50) NOT NULL,
	[AddressLine2] [nvarchar](50) NULL,
	[City] [nvarchar](50) NOT NULL,
	[State] [nvarchar](50) NOT NULL,
	[Country] [nvarchar](50) NOT NULL,
	[ZipCode] [nvarchar](50) NOT NULL,
	[ConsentToCallback] [bit] NOT NULL,
	[ContactNumber] [nvarchar](50) NULL,
	[Email] [nvarchar](100) NOT NULL,
	[ReferrerId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Patient_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO

/****** Object:  Index [IX_Patient_NHSNo]    Script Date: 09/25/2007 13:13:16 ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Patient]') AND name = N'IX_Patient_NHSNo')
CREATE UNIQUE NONCLUSTERED INDEX [IX_Patient_NHSNo] ON [dbo].[Patient] 
(
	[PatientNo] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier for a patient. Format NNN-NNN-NNNN. Stored in the database without the dashes' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Patient', @level2type=N'COLUMN', @level2name=N'PatientNo'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Title - Mr., Mrs.' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Patient', @level2type=N'COLUMN', @level2name=N'Title'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'First Name' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Patient', @level2type=N'COLUMN', @level2name=N'FirstName'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Last Name' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Patient', @level2type=N'COLUMN', @level2name=N'LastName'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Gender' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Patient', @level2type=N'COLUMN', @level2name=N'Gender'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date of Birth. Can be null' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Patient', @level2type=N'COLUMN', @level2name=N'DOB'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Address Line 1' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Patient', @level2type=N'COLUMN', @level2name=N'AddressLine1'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Address Line 2' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Patient', @level2type=N'COLUMN', @level2name=N'AddressLine2'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'City' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Patient', @level2type=N'COLUMN', @level2name=N'City'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'State' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Patient', @level2type=N'COLUMN', @level2name=N'State'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Country' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Patient', @level2type=N'COLUMN', @level2name=N'Country'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Zip Code' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Patient', @level2type=N'COLUMN', @level2name=N'ZipCode'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'True if the patient has elected to be called back in case the booking system personnel want to contact him/her' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Patient', @level2type=N'COLUMN', @level2name=N'ConsentToCallback'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Contact number' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Patient', @level2type=N'COLUMN', @level2name=N'ContactNumber'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Email Address' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Patient', @level2type=N'COLUMN', @level2name=N'Email'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Referrer' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Patient', @level2type=N'COLUMN', @level2name=N'ReferrerId'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Patient Information Table' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Patient'

GO
/****** Object:  Table [dbo].[ProviderClinicType]    Script Date: 09/25/2007 13:13:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProviderClinicType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ProviderClinicType](
	[Id] [uniqueidentifier] NOT NULL,
	[ProviderId] [uniqueidentifier] NOT NULL,
	[ClinicTypeId] [uniqueidentifier] NOT NULL,
	[SlotsAvailable] [int] NOT NULL,
	[SlotDuration] [int] NOT NULL,
	[DayStartTime] [datetime] NOT NULL,
	[DayEndTime] [datetime] NOT NULL,
	[WeekDays] [int] NOT NULL,
 CONSTRAINT [PK_ServiceClinicTypes_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary Key' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ProviderClinicType', @level2type=N'COLUMN', @level2name=N'Id'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Provider Id' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ProviderClinicType', @level2type=N'COLUMN', @level2name=N'ProviderId'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Clinic Type Id' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ProviderClinicType', @level2type=N'COLUMN', @level2name=N'ClinicTypeId'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Number of Slots Available for this Provider - Clinic Type combination' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ProviderClinicType', @level2type=N'COLUMN', @level2name=N'SlotsAvailable'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Duration of each slot' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ProviderClinicType', @level2type=N'COLUMN', @level2name=N'SlotDuration'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Start Time of each day' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ProviderClinicType', @level2type=N'COLUMN', @level2name=N'DayStartTime'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'End Time of each day' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ProviderClinicType', @level2type=N'COLUMN', @level2name=N'DayEndTime'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Week Days when the service will be available. Bit mask of week days.' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ProviderClinicType', @level2type=N'COLUMN', @level2name=N'WeekDays'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Provider Clinic Type association Table' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ProviderClinicType'

GO
/****** Object:  Table [dbo].[Referrer]    Script Date: 09/25/2007 13:13:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Referrer]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Referrer](
	[Id] [uniqueidentifier] NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[ClinicName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Referrer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Referring Clinician Identifier' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Referrer', @level2type=N'COLUMN', @level2name=N'Id'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name of the Clinician' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Referrer', @level2type=N'COLUMN', @level2name=N'FirstName'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name of the Clinic' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Referrer', @level2type=N'COLUMN', @level2name=N'ClinicName'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Role' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Referrer', @level2type=N'COLUMN', @level2name=N'LastName'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Email Address' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Referrer', @level2type=N'COLUMN', @level2name=N'Email'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Referrer Table' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Referrer'

GO
/****** Object:  Table [dbo].[Session]    Script Date: 09/25/2007 13:13:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Session]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Session](
	[Id] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[Length] [int] NOT NULL,
	[LastAccessedTime] [datetime] NOT NULL,
	[LoginTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Session] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Slot]    Script Date: 09/25/2007 13:13:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Slot]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Slot](
	[Id] [uniqueidentifier] NOT NULL,
	[ProviderClinicTypeId] [uniqueidentifier] NOT NULL,
	[StartDateTime] [datetime] NOT NULL,
	[EndDateTime] [datetime] NOT NULL,
	[UBRN] [int] NULL,
	[Status] [int] NULL,
 CONSTRAINT [PK_Slot] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Slot Identifier' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Slot', @level2type=N'COLUMN', @level2name=N'Id'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Provider clinic type association Id' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Slot', @level2type=N'COLUMN', @level2name=N'ProviderClinicTypeId'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Start Date Time of the slot' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Slot', @level2type=N'COLUMN', @level2name=N'StartDateTime'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'End Date Time of the slot' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Slot', @level2type=N'COLUMN', @level2name=N'EndDateTime'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'UBRN - if booked, else null' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Slot', @level2type=N'COLUMN', @level2name=N'UBRN'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Appointment Slots Table' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Slot'

GO
/****** Object:  Table [dbo].[Specialty]    Script Date: 09/25/2007 13:13:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Specialty]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Specialty](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Speciality] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Speciality Id' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Specialty', @level2type=N'COLUMN', @level2name=N'Id'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Specialty', @level2type=N'COLUMN', @level2name=N'Name'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Speciality Lookup Table' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Specialty'

GO
/****** Object:  Table [dbo].[User]    Script Date: 09/25/2007 13:13:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[User]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[User](
	[Id] [uniqueidentifier] NOT NULL,
	[UserName] [nvarchar](20) NOT NULL,
	[Password] [nvarchar](20) NOT NULL,
	[FirstName] [nvarchar](20) NOT NULL,
	[LastName] [nvarchar](20) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Role] [int] NOT NULL,
	[RefUserId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[ZipCode]    Script Date: 09/25/2007 13:13:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ZipCode]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ZipCode](
	[Zip] [float] NOT NULL,
	[Country] [nchar](3) NOT NULL,
	[Lat] [float] NOT NULL,
	[Lon] [float] NOT NULL
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Provider]    Script Date: 09/25/2007 13:13:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Provider]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Provider](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Location] [nvarchar](50) NOT NULL,
	[Organization] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[Latitude] [float] NOT NULL,
	[Longitude] [float] NOT NULL,
	[ConditionsTreated] [nvarchar](250) NULL,
	[ProceduresPerformed] [nvarchar](250) NULL,
	[Exclusions] [nvarchar](250) NULL,
	[AlternativeServices] [nvarchar](250) NULL,
 CONSTRAINT [PK_Service] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Provider Id' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Provider', @level2type=N'COLUMN', @level2name=N'Id'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name of the Provider' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Provider', @level2type=N'COLUMN', @level2name=N'Name'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Location - Single Line Address' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Provider', @level2type=N'COLUMN', @level2name=N'Location'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Organization to which the service provider belongs' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Provider', @level2type=N'COLUMN', @level2name=N'Organization'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Email Address to which the notification of new/cancelled/updated appointments will be sent' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Provider', @level2type=N'COLUMN', @level2name=N'Email'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Latitude - Used for proximity search' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Provider', @level2type=N'COLUMN', @level2name=N'Latitude'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Longitude - Used for proximity search' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Provider', @level2type=N'COLUMN', @level2name=N'Longitude'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Conditions Treated - Free Text - Will be used for searching based on keyword' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Provider', @level2type=N'COLUMN', @level2name=N'ConditionsTreated'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Procedures Performed - Free Text - Will be used for searching based on keyword' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Provider', @level2type=N'COLUMN', @level2name=N'ProceduresPerformed'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Exclusions - Free Text - Will be used for searching based on keyword' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Provider', @level2type=N'COLUMN', @level2name=N'Exclusions'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Alternative Services - Free Text - Will be used for searching based on keyword' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Provider', @level2type=N'COLUMN', @level2name=N'AlternativeServices'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Service Provider Information Table' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Provider'

GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Appointment_Patient]') AND parent_object_id = OBJECT_ID(N'[dbo].[Appointment]'))
ALTER TABLE [dbo].[Appointment]  WITH CHECK ADD  CONSTRAINT [FK_Appointment_Patient] FOREIGN KEY([PatientId])
REFERENCES [dbo].[Patient] ([Id])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Appointment_Referrer]') AND parent_object_id = OBJECT_ID(N'[dbo].[Appointment]'))
ALTER TABLE [dbo].[Appointment]  WITH CHECK ADD  CONSTRAINT [FK_Appointment_Referrer] FOREIGN KEY([ReferrerId])
REFERENCES [dbo].[Referrer] ([Id])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ClinicType_Specialty]') AND parent_object_id = OBJECT_ID(N'[dbo].[ClinicType]'))
ALTER TABLE [dbo].[ClinicType]  WITH CHECK ADD  CONSTRAINT [FK_ClinicType_Specialty] FOREIGN KEY([SpecialtyId])
REFERENCES [dbo].[Specialty] ([Id])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProviderClinicType_Provider]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProviderClinicType]'))
ALTER TABLE [dbo].[ProviderClinicType]  WITH CHECK ADD  CONSTRAINT [FK_ProviderClinicType_Provider] FOREIGN KEY([ProviderId])
REFERENCES [dbo].[Provider] ([Id])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ServiceClinicType_ClinicType]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProviderClinicType]'))
ALTER TABLE [dbo].[ProviderClinicType]  WITH CHECK ADD  CONSTRAINT [FK_ServiceClinicType_ClinicType] FOREIGN KEY([ClinicTypeId])
REFERENCES [dbo].[ClinicType] ([Id])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Session_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[Session]'))
ALTER TABLE [dbo].[Session]  WITH CHECK ADD  CONSTRAINT [FK_Session_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Slot_ProviderClinicType]') AND parent_object_id = OBJECT_ID(N'[dbo].[Slot]'))
ALTER TABLE [dbo].[Slot]  WITH CHECK ADD  CONSTRAINT [FK_Slot_ProviderClinicType] FOREIGN KEY([ProviderClinicTypeId])
REFERENCES [dbo].[ProviderClinicType] ([Id])
GO

/****** Object:  StoredProcedure [dbo].[AppointmentDelete]    Script Date: 09/25/2007 13:13:52 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AppointmentDelete]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[AppointmentDelete]
GO
/****** Object:  StoredProcedure [dbo].[AppointmentGet]    Script Date: 09/25/2007 13:13:52 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AppointmentGet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[AppointmentGet]
GO
/****** Object:  StoredProcedure [dbo].[AppointmentSave]    Script Date: 09/24/2007 13:25:35 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AppointmentSave]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[AppointmentSave]
GO
/****** Object:  StoredProcedure [dbo].[ClinicTypeGet]    Script Date: 09/25/2007 13:13:52 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ClinicTypeGet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ClinicTypeGet]
GO
/****** Object:  StoredProcedure [dbo].[PatientDelete]    Script Date: 09/25/2007 13:13:52 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PatientDelete]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PatientDelete]
GO
/****** Object:  StoredProcedure [dbo].[GetMaxUBRN]    Script Date: 09/25/2007 13:13:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetMaxUBRN]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetMaxUBRN]
GO
/****** Object:  StoredProcedure [dbo].[PatientGet]    Script Date: 09/25/2007 13:13:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PatientGet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PatientGet]
GO
/****** Object:  StoredProcedure [dbo].[PatientSave]    Script Date: 09/25/2007 13:13:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PatientSave]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PatientSave]
GO
/****** Object:  StoredProcedure [dbo].[ProviderClinicTypeDelete]    Script Date: 09/25/2007 13:13:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProviderClinicTypeDelete]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ProviderClinicTypeDelete]
GO
/****** Object:  StoredProcedure [dbo].[ProviderClinicTypeGet]    Script Date: 09/25/2007 13:13:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProviderClinicTypeGet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ProviderClinicTypeGet]
GO
/****** Object:  StoredProcedure [dbo].[ProviderClinicTypeSave]    Script Date: 09/25/2007 13:13:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProviderClinicTypeSave]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ProviderClinicTypeSave]
GO
/****** Object:  StoredProcedure [dbo].[ProviderDelete]    Script Date: 09/25/2007 13:13:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProviderDelete]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ProviderDelete]
GO
/****** Object:  StoredProcedure [dbo].[ProviderGet]    Script Date: 09/25/2007 13:13:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProviderGet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ProviderGet]
GO
/****** Object:  StoredProcedure [dbo].[ProviderSave]    Script Date: 09/25/2007 13:13:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProviderSave]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ProviderSave]
GO
/****** Object:  StoredProcedure [dbo].[ReferrerDelete]    Script Date: 09/25/2007 13:13:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ReferrerDelete]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ReferrerDelete]
GO
/****** Object:  StoredProcedure [dbo].[ReferrerGet]    Script Date: 09/25/2007 13:13:55 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ReferrerGet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ReferrerGet]
GO
/****** Object:  StoredProcedure [dbo].[ReferrerSave]    Script Date: 09/25/2007 13:13:55 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ReferrerSave]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ReferrerSave]
GO
/****** Object:  StoredProcedure [dbo].[SessionDelete]    Script Date: 09/25/2007 13:13:55 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SessionDelete]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SessionDelete]
GO
/****** Object:  StoredProcedure [dbo].[SessionGet]    Script Date: 09/25/2007 13:13:55 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SessionGet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SessionGet]
GO
/****** Object:  StoredProcedure [dbo].[SessionSave]    Script Date: 09/25/2007 13:13:55 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SessionSave]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SessionSave]
GO
/****** Object:  StoredProcedure [dbo].[SlotDelete]    Script Date: 09/25/2007 13:13:56 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SlotDelete]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SlotDelete]
GO
/****** Object:  StoredProcedure [dbo].[SlotGet]    Script Date: 09/25/2007 13:13:56 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SlotGet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SlotGet]
GO
/****** Object:  StoredProcedure [dbo].[SlotSave]    Script Date: 09/25/2007 13:13:56 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SlotSave]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SlotSave]
GO
/****** Object:  StoredProcedure [dbo].[SpecialtyGet]    Script Date: 09/25/2007 13:13:56 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpecialtyGet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SpecialtyGet]
GO
/****** Object:  StoredProcedure [dbo].[UserDelete]    Script Date: 09/25/2007 13:13:56 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserDelete]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[UserDelete]
GO
/****** Object:  StoredProcedure [dbo].[UserGet]    Script Date: 09/25/2007 13:13:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserGet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[UserGet]
GO
/****** Object:  StoredProcedure [dbo].[UserSave]    Script Date: 09/25/2007 13:13:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserSave]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[UserSave]
GO
/****** Object:  StoredProcedure [dbo].[AppointmentDelete]    Script Date: 09/25/2007 13:13:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AppointmentDelete]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[AppointmentDelete] 
@UBRN		INT

AS

DELETE 
FROM	Appointment
WHERE	UBRN = @UBRN
	
RETURN @@ROWCOUNT' 
END
GO
/****** Object:  StoredProcedure [dbo].[AppointmentGet]    Script Date: 09/25/2007 13:13:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AppointmentGet]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[AppointmentGet]
	@UBRN			INT,
	@PatientId		UNIQUEIDENTIFIER,
	@ReferrerId		UNIQUEIDENTIFIER,
	@ProviderId		UNIQUEIDENTIFIER,
	@Status			INT,
	@StartDateTime	DATETIME,
	@EndDateTime	DATETIME,
	@WorkflowId		UNIQUEIDENTIFIER
AS

SELECT	A.*
FROM	Appointment A
WHERE	(@UBRN			IS NULL OR A.UBRN = @UBRN)
	AND (@PatientId		IS NULL OR A.PatientId = @PatientId)
	AND (@ReferrerId	IS NULL OR A.ReferrerId = @ReferrerId)
	AND	(@ProviderId	IS NULL OR A.ProviderId = @ProviderId)
	AND	(@Status		IS NULL OR A.Status & @Status > 0)
	AND	(@StartDateTime	IS NULL OR ((DatePart(yy,A.StartDateTime) >= DatePart(yy,@StartDateTime)) AND (DatePart(dy,A.StartDateTime) >= DatePart(dy, @StartDateTime))))
	AND	(@EndDateTime	IS NULL OR ((DatePart(yy,A.EndDateTime) <= DatePart(yy,@EndDateTime)) AND (DatePart(dy,A.EndDateTime) <= DatePart(dy, @EndDateTime))))
	AND (@WorkflowId	IS NULL OR A.WorkflowId = @WorkflowId)
ORDER BY A.UBRN
	
RETURN @@ROWCOUNT' 
END
GO
/****** Object:  StoredProcedure [dbo].[AppointmentSave]    Script Date: 09/24/2007 13:25:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AppointmentSave]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[AppointmentSave]
@UBRN				INT, 
@PatientId			UNIQUEIDENTIFIER,
@ReferrerId			UNIQUEIDENTIFIER,
@ProviderId			UNIQUEIDENTIFIER,
@ClinicTypeId		UNIQUEIDENTIFIER,
@SlotId				UNIQUEIDENTIFIER,
@CreatedDate		DATETIME,
@StartDateTime		DATETIME,
@EndDateTime		DATETIME,
@UpdatedDate		DATETIME,
@CancelledBy		UNIQUEIDENTIFIER,
@CancelledDate		DATETIME,
@CancellationReason	NVARCHAR(100),
@Status				INT,
@Comments			NVARCHAR(MAX),
@ReminderDate		DATETIME,
@WorkflowId			UNIQUEIDENTIFIER

AS

DECLARE	@RowCount AS INT
DECLARE @Error AS INT

UPDATE	Appointment
SET		PatientId			= ISNULL(@PatientId,PatientId),
		ReferrerId			= ISNULL(@ReferrerId,ReferrerId),
		ProviderId			= ISNULL(@ProviderId,ProviderId),
		ClinicTypeId		= ISNULL(@ClinicTypeId, ClinicTypeId),
		SlotId				= @SlotId,
		CreatedDate			= ISNULL(@CreatedDate, CreatedDate),
		StartDateTime		= ISNULL(@StartDateTime, StartDateTime),
		EndDateTime			= ISNULL(@EndDateTime, EndDateTime),
		CancelledBy			= ISNULL(@CancelledBy, CancelledBy),
		CancelledDate		= ISNULL(@CancelledDate, CancelledDate),
		CancellationReason	= ISNULL(@CancellationReason, CancellationReason),
		Status				= ISNULL(@Status, Status),
		UpdatedDate			= ISNULL(@UpdatedDate, UpdatedDate),
		Comments			= ISNULL(@Comments, Comments),
		ReminderDate		= ISNULL(@ReminderDate, ReminderDate),
		WorkflowId			= ISNULL(@WorkflowId, WorkflowId)
WHERE	UBRN = @UBRN

SELECT @Error = @@ERROR, @RowCount = @@ROWCOUNT

IF @Error  <> 0
BEGIN
	RAISERROR(''ERROR::AppointmentSave:Error in'',16,1)
END

IF @RowCount  > 0
BEGIN
	RETURN @RowCount 
END

--	If adding for first time enter all the details including UBRN
INSERT	INTO Appointment (UBRN, PatientId, ReferrerId, ProviderId, ClinicTypeId, SlotId, CreatedDate, StartDateTime, EndDateTime, CancelledBy, CancelledDate, CancellationReason, Status, UpdatedDate, Comments, ReminderDate, WorkflowId)		
VALUES	(@UBRN, @PatientId, @ReferrerId, @ProviderId, @ClinicTypeId, @SlotId, @CreatedDate, @StartDateTime, @EndDateTime, @CancelledBy, @CancelledDate, @CancellationReason, @Status, @UpdatedDate, @Comments, @ReminderDate, @WorkflowId)		

SELECT	@Error = @@ERROR, @RowCount = @@ROWCOUNT

IF @Error  <> 0
BEGIN
	RAISERROR(''ERROR::AppointmentNew:Error in'',16,1)
END

RETURN @RowCount'
END
GO
/****** Object:  StoredProcedure [dbo].[ClinicTypeGet]    Script Date: 09/25/2007 13:13:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ClinicTypeGet]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N' CREATE PROCEDURE [dbo].[ClinicTypeGet]
	@Id					UNIQUEIDENTIFIER,
	@SpecialtyId		UNIQUEIDENTIFIER
AS

SELECT	CT.*
FROM	ClinicType	CT
INNER JOIN Specialty S
	ON	CT.SpecialtyId = S.Id
WHERE	
		(@Id IS NULL OR CT.Id = @Id)
	AND (@SpecialtyId IS NULL OR @SpecialtyId = CT.SpecialtyId)
					
	
RETURN @@ROWCOUNT' 
END
GO
/****** Object:  StoredProcedure [dbo].[PatientDelete]    Script Date: 09/25/2007 13:13:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PatientDelete]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[PatientDelete] 
@Id		UNIQUEIDENTIFIER
AS

DELETE 
FROM	Patient
WHERE	Id	= @Id
	
RETURN @@ROWCOUNT' 
END
GO
/****** Object:  StoredProcedure [dbo].[GetMaxUBRN]    Script Date: 09/25/2007 13:14:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetMaxUBRN]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[GetMaxUBRN]
AS

Select ISNULL(MAX(UBRN), 1000) from Appointment
	
RETURN @@RowCount' 
END
GO
/****** Object:  StoredProcedure [dbo].[PatientGet]    Script Date: 09/25/2007 13:14:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PatientGet]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[PatientGet]
@Id			UNIQUEIDENTIFIER,	
@ReferrerId	UNIQUEIDENTIFIER,
@FirstName	NVARCHAR(50),
@LastName	NVARCHAR(50)
AS

SELECT	P.*
FROM	Patient P
WHERE	P.Id = ISNULL(@Id,Id)
AND		(@ReferrerId IS NULL OR P.ReferrerId = @ReferrerId)
AND		(@FirstName IS NULL OR P.FirstName = @FirstName)
AND		(@LastName IS NULL OR P.LastName = @LastName)
ORDER BY P.LastName, P.FirstName
	
RETURN @@ROWCOUNT' 
END
GO
/****** Object:  StoredProcedure [dbo].[PatientSave]    Script Date: 09/25/2007 13:14:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PatientSave]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[PatientSave]
@Id					UNIQUEIDENTIFIER,
@PatientNo			NVARCHAR(10),
@Title				NVARCHAR(50),
@FirstName			NVARCHAR(50),
@LastName			NVARCHAR(50),
@Gender				NCHAR(1),
@DOB				DATETIME,
@AddressLine1		NVARCHAR(50),
@AddressLine2		NVARCHAR(50),
@City				NVARCHAR(50),
@State				NVARCHAR(50),
@Country			NVARCHAR(50),
@ZipCode			NVARCHAR(50),
@ConsentToCallBack	BIT,
@ContactNumber		NVARCHAR(50),
@Email				NVARCHAR(100),
@ReferrerId			UNIQUEIDENTIFIER
AS

DECLARE	@RowCount AS INT
DECLARE @Error AS INT

UPDATE	Patient
SET		PatientNo			= ISNULL(@PatientNo, PatientNo),
		Title				= ISNULL(@Title, Title), 	
		FirstName			= ISNULL(@FirstName, FirstName),
		LastName			= ISNULL(@LastName, LastName), 
		Gender				= ISNULL(@Gender, Gender), 
		DOB					= ISNULL(@DOB, DOB), 	
		AddressLine1		= ISNULL(@AddressLine1, AddressLine1),
		AddressLine2		= ISNULL(@AddressLine2, AddressLine2),
		City				= ISNULL(@City, City),
		State				= ISNULL(@State, State)	, 
		Country				= ISNULL(@Country, Country),
		ZipCode				= ISNULL(@ZipCode, ZipCode), 
		ConsentToCallback	= ISNULL(@ConsentToCallback,ConsentToCallback),
		ContactNumber		= ISNULL(@ContactNumber,ContactNumber),
		Email				= ISNULL(@Email, Email),
		ReferrerId			= ISNULL(@ReferrerId, ReferrerId)
WHERE	Id = @Id

SELECT @Error = @@ERROR, @RowCount = @@ROWCOUNT

IF @Error  <> 0
BEGIN
	RAISERROR(''ERROR::PatientSave:Error in'',16,1)
END

IF @RowCount  > 0
BEGIN
	RETURN @RowCount 
END

--	If adding for first time enter all the details including Id
INSERT	INTO Patient (Id, PatientNo, Title,  FirstName, LastName, Gender, DOB, AddressLine1, AddressLine2, City, State, Country, ZipCode, ConsentToCallback, ContactNumber, Email, ReferrerId)
VALUES	(@Id, @PatientNo, @Title, @FirstName, @LastName, @Gender, @DOB, @AddressLine1, @AddressLine2, @City, @State, @Country, @ZipCode, @ConsentToCallback, @ContactNumber, @Email, @ReferrerId)

SELECT	@Error = @@ERROR, @RowCount = @@ROWCOUNT

IF @Error  <> 0
BEGIN
	RAISERROR(''ERROR::PatientNew:Error in'',16,1)
END

RETURN @RowCount' 
END
GO
/****** Object:  StoredProcedure [dbo].[ProviderClinicTypeDelete]    Script Date: 09/25/2007 13:14:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProviderClinicTypeDelete]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[ProviderClinicTypeDelete]
	@ProviderId		UNIQUEIDENTIFIER,
	@ClinicTypeId	UNIQUEIDENTIFIER
AS

DELETE 
FROM	ProviderClinicType
WHERE	NOT(@ProviderId IS NULL AND @ClinicTypeId IS NULL)
	AND (@ProviderId IS NULL OR ProviderId = @ProviderId)
	AND (@ClinicTypeId IS NULL OR ClinicTypeId = @ClinicTypeId)
	
RETURN @@ROWCOUNT ' 
END
GO
/****** Object:  StoredProcedure [dbo].[ProviderClinicTypeGet]    Script Date: 09/25/2007 13:14:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProviderClinicTypeGet]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[ProviderClinicTypeGet]
	@Id				UNIQUEIDENTIFIER,
	@ProviderId		UNIQUEIDENTIFIER,
	@ClinicTypeId	UNIQUEIDENTIFIER
AS

SELECT	PCT.*
FROM	ClinicType	CT
INNER JOIN ProviderClinicType PCT
	ON	CT.Id = PCT.ClinicTypeId
INNER JOIN Provider P
	ON	PCT.ProviderId = P.Id
WHERE	NOT(@Id IS NULL AND @ProviderId IS NULL)
	AND (@Id IS NULL OR PCT.Id = @Id)
	AND (@ProviderId IS NULL OR PCT.ProviderId = @ProviderId)
	AND (@ClinicTypeId IS NULL OR PCT.ClinicTypeId = @ClinicTypeId)
					
	
RETURN @@ROWCOUNT ' 
END
GO
/****** Object:  StoredProcedure [dbo].[ProviderClinicTypeSave]    Script Date: 09/25/2007 13:14:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProviderClinicTypeSave]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N' CREATE PROCEDURE [dbo].[ProviderClinicTypeSave]
	@Id				UNIQUEIDENTIFIER,
	@ProviderId		UNIQUEIDENTIFIER,
	@ClinicTypeId	UNIQUEIDENTIFIER,
	@SlotsAvailable INT,
	@SlotDuration	INT,
	@DayStartTime	DATETIME,
	@DayEndTime		DATETIME,
	@WeekDays		INT
AS

DECLARE	@RowCount AS INT
DECLARE @Error AS INT

UPDATE	ProviderClinicType
SET		ProviderId			= ISNULL(@ProviderId,ProviderId),
		ClinicTypeId		= ISNULL(@ClinicTypeId, ClinicTypeId),
		SlotsAvailable		= ISNULL(@SlotsAvailable, SlotsAvailable),
		SlotDuration		= ISNULL(@SlotDuration, SlotDuration),
		DayStartTime		= ISNULL(@DayStartTime, DayStartTime),
		DayEndTime			= ISNULL(@DayEndTime, DayEndTime),
		WeekDays			= ISNULL(@WeekDays, WeekDays)
WHERE	Id = @Id

SELECT @Error = @@ERROR, @RowCount = @@ROWCOUNT

IF @Error  <> 0
BEGIN
	RAISERROR(''ERROR::ProviderClinicTypeSave:Error in'',16,1)
END

IF @RowCount  > 0
BEGIN
	RETURN @RowCount 
END

--	If adding for first time enter all the details including UBRN
INSERT	INTO ProviderClinicType (Id, ProviderId, ClinicTypeId, SlotsAvailable, SlotDuration, DayStartTime, DayEndTime, WeekDays)		
VALUES	(@Id, @ProviderId, @ClinicTypeId, @SlotsAvailable, @SlotDuration, @DayStartTime, @DayEndTime, @WeekDays)		

SELECT	@Error = @@ERROR, @RowCount = @@ROWCOUNT

IF @Error  <> 0
BEGIN
	RAISERROR(''ERROR::ProviderClinicTypeNew:Error in'',16,1)
END

RETURN @RowCount' 
END
GO
/****** Object:  StoredProcedure [dbo].[ProviderDelete]    Script Date: 09/25/2007 13:14:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProviderDelete]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[ProviderDelete] 
@Id		UNIQUEIDENTIFIER
AS

DELETE 
FROM	Provider
WHERE	Id	= @Id
	
RETURN @@ROWCOUNT' 
END
GO
/****** Object:  StoredProcedure [dbo].[ProviderGet]    Script Date: 09/25/2007 13:14:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProviderGet]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[ProviderGet]
	@Id				UNIQUEIDENTIFIER,
	@SpecialtyId	UNIQUEIDENTIFIER,
	@ClinicTypeId	UNIQUEIDENTIFIER,
	@ZipCode		FLOAT,
	@Proximity		FLOAT,
	@Keywords		NVARCHAR(50)
	
AS

DECLARE @FromLat FLOAT
DECLARE @FromLong FLOAT
DECLARE @pProximity FLOAT


Select @FromLat = lat,
@FromLong = lon
from ZipCode
Where Zip IS NULL OR Zip = @ZipCode

Select A.* from (select dbo.fnProximityGet(@FromLat,@FromLong,Latitude,Longitude) AS Proximity ,*
from Provider P) AS A
INNER JOIN ProviderClinicType PCT ON PCT.ProviderId = A.Id
INNER JOIN ClinicType CT ON PCT.ClinicTypeId = CT.Id
INNER JOIN Specialty S ON CT.SpecialtyId = S.Id
WHERE 
	(@Id IS NULL OR A.Id = @Id)
	AND (@SpecialtyId IS NULL OR S.Id = @SpecialtyId)
	AND (@ClinicTypeId IS NULL OR CT.Id = @ClinicTypeId)
	AND (@Keywords IS NULL OR (A.ConditionsTreated LIKE @Keywords OR A.ProceduresPerformed LIKE @Keywords OR A.Exclusions LIKE @Keywords OR A.AlternativeServices LIKE @Keywords))
	AND (@Proximity IS NULL OR A.Proximity <= @Proximity)
	ORDER BY A.Proximity
	
RETURN @@ROWCOUNT' 
END
GO
/****** Object:  StoredProcedure [dbo].[ProviderSave]    Script Date: 09/25/2007 13:14:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProviderSave]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[ProviderSave]
@Id						UNIQUEIDENTIFIER,
@Name					NVARCHAR(50),
@Location				NVARCHAR(50),
@Organization			NVARCHAR(50),
@Email					NVARCHAR(100),
@Latitude				FLOAT,
@Longitude				FLOAT,
@ConditionsTreated		NVARCHAR(250),
@ProceduresPerformed	NVARCHAR(250),
@Exclusions				NVARCHAR(250),
@AlternativeServices	NVARCHAR(250)

AS

DECLARE	@RowCount AS INT
DECLARE @Error AS INT

UPDATE	Provider
SET		[Name]				= ISNULL(@Name, [Name]), 
		Location			= ISNULL(@Location, Location), 	
		Organization		= ISNULL(@Organization, Organization),
		Email				= ISNULL(@Email, Email),
		Latitude			= ISNULL(@Latitude, Latitude),
		Longitude			= ISNULL(@Longitude, Longitude),
		ConditionsTreated	= ISNULL(@ConditionsTreated, ConditionsTreated), 	
		ProceduresPerformed	= ISNULL(@ProceduresPerformed, ProceduresPerformed), 	
		Exclusions			= ISNULL(@Exclusions, Exclusions), 	
		AlternativeServices	= ISNULL(@AlternativeServices, AlternativeServices)
WHERE	Id = @Id

SELECT @Error = @@ERROR, @RowCount = @@ROWCOUNT

IF @Error  <> 0
BEGIN
	RAISERROR(''ERROR::ProviderSave:Error in'',16,1)
END

IF @RowCount  > 0
BEGIN
	RETURN @RowCount 
END

--	If adding for first time enter all the details including Id
INSERT	INTO Provider (Id, [Name], Location, Organization, Email, Latitude, Longitude, ConditionsTreated, ProceduresPerformed, Exclusions, AlternativeServices)
VALUES	(@Id, @Name, @Location, @Organization, @Email, @Latitude, @Longitude, @ConditionsTreated, @ProceduresPerformed, @Exclusions, @AlternativeServices)

SELECT	@Error = @@ERROR, @RowCount = @@ROWCOUNT

IF @Error  <> 0
BEGIN
	RAISERROR(''ERROR::ProviderNew:Error in'',16,1)
END

RETURN @RowCount' 
END
GO
/****** Object:  StoredProcedure [dbo].[ReferrerDelete]    Script Date: 09/25/2007 13:14:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ReferrerDelete]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[ReferrerDelete] 
@Id		UNIQUEIDENTIFIER

AS

DELETE 
FROM	Referrer
WHERE	Id	= @Id
	
RETURN @@ROWCOUNT' 
END
GO
/****** Object:  StoredProcedure [dbo].[ReferrerGet]    Script Date: 09/25/2007 13:14:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ReferrerGet]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[ReferrerGet]
	@Id				UNIQUEIDENTIFIER,
	@FirstName		NVARCHAR(50),
	@LastName		NVARCHAR(50)
AS

SELECT	R.*
FROM	Referrer R
WHERE	(@Id IS NULL OR R.Id = @Id)
	AND (@FirstName IS NULL OR R.FirstName = @FirstName)
	AND (@LastName IS NULL OR R.LastName = @LastName)
ORDER By R.LastName, R.FirstName
	
RETURN @@ROWCOUNT' 
END
GO
/****** Object:  StoredProcedure [dbo].[ReferrerSave]    Script Date: 09/25/2007 13:14:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ReferrerSave]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[ReferrerSave]
@Id				UNIQUEIDENTIFIER,
@FirstName		NVARCHAR(50),
@ClinicName		NVARCHAR(50),
@LastName		NVARCHAR(50),
@Email			NVARCHAR(100)
AS

DECLARE	@RowCount AS INT
DECLARE @Error AS INT

UPDATE	Referrer
SET		[FirstName]		= ISNULL(@FirstName, [FirstName]), 
		ClinicName		= ISNULL(@ClinicName, ClinicName), 	
		LastName		= ISNULL(@LastName, LastName),
		Email			= ISNULL(@Email, Email)
WHERE	Id = @Id

SELECT @Error = @@ERROR, @RowCount = @@ROWCOUNT

IF @Error  <> 0
BEGIN
	RAISERROR(''ERROR::ReferrerSave:Error in'',16,1)
END

IF @RowCount  > 0
BEGIN
	RETURN @RowCount 
END

--	If adding for first time enter all the details including Id
INSERT	INTO Referrer (Id, [FirstName], ClinicName, LastName, Email)
VALUES	(@Id, @FirstName, @ClinicName, @LastName, @Email)

SELECT	@Error = @@ERROR, @RowCount = @@ROWCOUNT

IF @Error  <> 0
BEGIN
	RAISERROR(''ERROR::ReferrerNew:Error in'',16,1)
END

RETURN @RowCount' 
END
GO
/****** Object:  StoredProcedure [dbo].[SessionDelete]    Script Date: 09/25/2007 13:14:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SessionDelete]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SessionDelete]
@Id		    	UNIQUEIDENTIFIER ,
@LogOutTime		DATETIME
AS

DELETE FROM Session
WHERE [Id]=@Id	

RETURN @@ROWCOUNT
' 
END
GO
/****** Object:  StoredProcedure [dbo].[SessionGet]    Script Date: 09/25/2007 13:14:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SessionGet]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SessionGet]
@Id		    UNIQUEIDENTIFIER 
AS
SELECT		*
FROM		Session
WHERE 
		 (@Id IS NULL OR [Id] = @Id)

RETURN @@ROWCOUNT
' 
END
GO
/****** Object:  StoredProcedure [dbo].[SessionSave]    Script Date: 09/25/2007 13:14:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SessionSave]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SessionSave] 
@Id					UNIQUEIDENTIFIER,
@UserId				UNIQUEIDENTIFIER,
@Length				INT,
@LastAccessedTime	DATETIME,
@LoginTime			DATETIME

AS

DECLARE  @ROWCOUNT AS INT 
DECLARE  @ERROR    AS INT
-- Check whether already record present --
UPDATE Session
SET	
	UserId           	= ISNULL(@UserId,UserId),
	LastAccessedTime    = ISNULL(@LastAccessedTime,LastAccessedTime),
	LoginTime			= ISNULL(@LoginTime,LoginTime)
WHERE  
  	[Id]= @Id

SELECT @ROWCOUNT =  @@ROWCOUNT,@ERROR = @@ERROR

IF @ERROR<>0 
BEGIN
	RAISERROR(''ERROR::SessionSave'',16,1)
END

IF @RowCount  > 0
BEGIN
	RETURN @RowCount 
END

-- Insert into Session --
INSERT INTO Session (Id, UserId, Length, LastAccessedTime, LoginTime)
VALUES (@Id, @UserId, @Length, @LastAccessedTime, @LoginTime)

SELECT @RowCount =@@ROWCOUNT,@Error=@@ERROR

IF @Error<>0
BEGIN
RAISERROR(''ERROR::SessionSave'',16,1)
END

RETURN @RowCount
' 
END
GO
/****** Object:  StoredProcedure [dbo].[SlotDelete]    Script Date: 09/25/2007 13:14:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SlotDelete]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N' CREATE PROCEDURE [dbo].[SlotDelete]
@Id				UNIQUEIDENTIFIER
AS

DELETE 
FROM	Slot
WHERE	Id = @Id

RETURN @@RowCount' 
END
GO
/****** Object:  StoredProcedure [dbo].[SlotGet]    Script Date: 09/25/2007 13:14:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SlotGet]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SlotGet]
@Id				UNIQUEIDENTIFIER,
@ProviderId			UNIQUEIDENTIFIER,
@SpecialtyId			UNIQUEIDENTIFIER,
@ClinicTypeId			UNIQUEIDENTIFIER,
@StartDateTime			DATETIME,
@EndDateTime			DATETIME,
@WeekDays			INT,
@UBRN				INT,
@Status				INT

AS

-- Set Status as NULL to retrieve both Booked and Available Slots
IF @Status = 0
BEGIN
		SET @Status = NULL	
END

SET DATEFIRST 7

SELECT	S.*, CT.Id AS ClinicTypeId, PCT.ProviderId AS ProviderId, SP.Id AS SpecialtyId
FROM	Slot S
INNER JOIN ProviderClinicType PCT ON PCT.Id = S.ProviderClinicTypeId
INNER JOIN ClinicType CT ON PCT.ClinicTypeId = CT.Id
INNER JOIN Specialty SP ON CT.SpecialtyId = SP.Id
WHERE	
	(@Id IS NULL OR S.Id = @Id)
	AND (@ProviderId IS NULL OR PCT.ProviderId = @ProviderId)
	AND (@SpecialtyId IS NULL OR SP.Id = @SpecialtyId)
	AND (@ClinicTypeId IS NULL OR PCT.ClinicTypeId = @ClinicTypeId)
	AND (@StartDateTime IS NULL OR DATEDIFF(mi,S.StartDateTime,@StartDateTime) <= 0)
	AND (@EndDateTime IS NULL OR DATEDIFF(mi,S.EndDateTime,@EndDateTime) >= 0)
	AND (@WeekDays IS NULL OR POWER(2,DatePart(dw,S.StartDateTime)) & @WeekDays > 0 )
	AND (@UBRN IS NULL OR S.UBRN = @UBRN)
	AND (@Status IS NULL OR S.Status = @Status)

RETURN @@RowCount
' 
END
GO
/****** Object:  StoredProcedure [dbo].[SlotSave]    Script Date: 09/25/2007 13:14:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SlotSave]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SlotSave]
@Id						UNIQUEIDENTIFIER,
@ProviderId				UNIQUEIDENTIFIER,
@SpecialtyId			UNIQUEIDENTIFIER,
@ClinicTypeId			UNIQUEIDENTIFIER,
@StartDateTime			DATETIME,
@EndDateTime			DATETIME,
@UBRN					INT,
@Status					INT
AS

DECLARE	@RowCount AS INT
DECLARE @Error AS INT
DECLARE @ProviderClinicTypeId AS UNIQUEIDENTIFIER

SET @ProviderClinicTypeId = (Select Id from ProviderClinicType WHERE ProviderId = @ProviderId AND ClinicTypeId = @ClinicTypeId)

UPDATE	Slot
SET		ProviderClinicTypeId	= ISNULL(@ProviderClinicTypeId, ProviderClinicTypeId),
		StartDateTime			= ISNULL(@StartDateTime, StartDateTime),
		EndDateTime				= ISNULL(@EndDateTime, EndDateTime),
		UBRN					= @UBRN,
		Status					= ISNULL(@Status, Status)
WHERE	Id = @Id

SELECT @Error = @@ERROR, @RowCount = @@ROWCOUNT

IF @Error  <> 0
BEGIN
	RAISERROR(''ERROR::SlotSave:Error in'',16,1)
END

IF @RowCount  > 0
BEGIN
	RETURN @RowCount 
END

INSERT INTO Slot(Id, ProviderClinicTypeId, StartDateTime, EndDateTime, UBRN, Status)
VALUES(@Id, @ProviderClinicTypeId, @StartDateTime, @EndDateTime, @UBRN, @Status)

SELECT	@Error = @@ERROR, @RowCount = @@ROWCOUNT

IF @Error  <> 0
BEGIN
	RAISERROR(''ERROR::SlotNew:Error in'',16,1)
END

RETURN @RowCount' 
END
GO
/****** Object:  StoredProcedure [dbo].[SpecialtyGet]    Script Date: 09/25/2007 13:14:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpecialtyGet]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SpecialtyGet]
AS

SELECT	* 
FROM	Specialty

RETURN @@RowCount' 
END
GO
/****** Object:  StoredProcedure [dbo].[UserDelete]    Script Date: 09/25/2007 13:14:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserDelete]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[UserDelete] 
@Id		UNIQUEIDENTIFIER

AS

DELETE 
FROM	[User]
WHERE	[Id] = @Id
	
RETURN @@ROWCOUNT
 ' 
END
GO
/****** Object:  StoredProcedure [dbo].[UserGet]    Script Date: 09/25/2007 13:14:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserGet]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[UserGet]
@Id			UNIQUEIDENTIFIER, 
@UserName	NVARCHAR(20)

AS

SELECT	*
FROM	[User]
WHERE
		 (@Id			IS NULL OR Id = @Id)
	AND  (@UserName		IS NULL OR UserName = @UserName)

RETURN @@ROWCOUNT
  ' 
END
GO
/****** Object:  StoredProcedure [dbo].[UserSave]    Script Date: 09/25/2007 13:14:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserSave]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[UserSave]
@Id				UNIQUEIDENTIFIER, 
@UserName			NVARCHAR(20),
@Password		NVARCHAR(20),
@FirstName		NVARCHAR(20),
@LastName		NVARCHAR(20),
@Email			NVARCHAR(50),
@Role			INT,
@RefUserId		UNIQUEIDENTIFIER

AS

DECLARE	@RowCount AS INT
DECLARE @Error AS INT

UPDATE	[User]
SET		UserName		= ISNULL(@UserName,[UserName]),
		Password		= ISNULL(@Password,Password),
		FirstName		= ISNULL(@FirstName,FirstName),
		LastName		= ISNULL(@LastName, LastName),
		Email			= ISNULL(@Email, Email),
		Role			= ISNULL(@Role, Role),
		RefUserId		= ISNULL(@RefUserId, RefUserId)
WHERE	Id = @Id

SELECT @Error = @@ERROR, @RowCount = @@ROWCOUNT

IF @Error  <> 0
BEGIN
	RAISERROR(''ERROR::UserSave'',16,1)
END

IF @RowCount  > 0
BEGIN
	RETURN @RowCount 
END

--	If adding for first time entering all the details
INSERT	INTO [User] (Id, UserName, Password, FirstName, LastName, Email, Role, RefUserId)
VALUES	(@Id, @UserName, @Password, @FirstName, @LastName, @Email, @Role, @RefUserId)

SELECT	@Error = @@ERROR, @RowCount = @@ROWCOUNT

IF @Error  <> 0
BEGIN
	RAISERROR(''ERROR::UserNew'',16,1)
END

RETURN @RowCount
   ' 
END
GO

/****** Object:  UserDefinedFunction [dbo].[fnProximityGet]    Script Date: 09/24/2007 13:40:44 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fnProximityGet]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[fnProximityGet]
GO
/****** Object:  UserDefinedFunction [dbo].[fnProximityGet]    Script Date: 09/24/2007 13:40:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fnProximityGet]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION dbo.fnProximityGet(@pFromLat AS FLOAT, @pFromLong AS FLOAT, @pToLat AS FLOAT, @pToLong AS FLOAT)
RETURNS FLOAT
AS 

BEGIN
 	DECLARE @TotalDistance 	FLOAT
	DECLARE @ReturnVal 	INT

	SET @ReturnVal = 0

	IF @pFromLat = 0 OR @pFromLong = 0 OR @pToLat = 0 OR @pToLong = 0
		RETURN 222
	 
	IF @pFromLat = @pToLat AND @pFromLong = @pToLong
		RETURN 1
	 
	SET @pFromLat= (PI()/180)*@pFromLat
	SET @pFromLong= (PI()/180)*@pFromLong
	SET @pToLat= (PI()/180)*@pToLat
	SET @pToLong= (PI()/180)*@pToLong
	 
	SET @TotalDistance = POWER(SIN((@pToLat-@pFromLat)/2),2) + COS(@pFromLat) * COS(@pToLat) * POWER(SIN((@pToLong - @pFromLong)/2),2)
	
	SET @TotalDistance = 2 * ATN2(SQRT(@TotalDistance), SQRT(1-@TotalDistance)) 
	
	-- Convert distance into Miles
	SELECT @TotalDistance = 6373 * @TotalDistance * 0.621371 * 1.6
  
	RETURN @TotalDistance
END  
' 
END

GO

