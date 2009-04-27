
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Environment](
	[Environment_id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Description] [nvarchar](500) NULL,
 CONSTRAINT [PK_Environment_lkp] PRIMARY KEY CLUSTERED 
(
	[Environment_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO






SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ConnectionString](
	[Name] [varchar](100) NOT NULL,
	[Environment_id] [int] NOT NULL,
	[Value] [varchar](200) NOT NULL,
	[ProviderName] [varchar](200) NULL,
 CONSTRAINT [PK_ConnectionString] PRIMARY KEY CLUSTERED 
(
	[Name] ASC,
	[Environment_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[ConnectionString]  WITH CHECK ADD  CONSTRAINT [FK_ConnectionString_Environment] FOREIGN KEY([Environment_id])
REFERENCES [dbo].[Environment] ([Environment_id])
GO

ALTER TABLE [dbo].[ConnectionString] CHECK CONSTRAINT [FK_ConnectionString_Environment]
GO






SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Farm](
	[Farm_id] [int] IDENTITY(1,1) NOT NULL,
	[FarmName] [varchar](50) NOT NULL,
	[FarmAddress] [varchar](255) NOT NULL,
	[Environment_id] [int] NOT NULL,
 CONSTRAINT [PK_Farm] PRIMARY KEY CLUSTERED 
(
	[Farm_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

EXEC sys.sp_addextendedproperty @name=N'MS_ColumnHidden', @value=0 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Farm', @level2type=N'COLUMN',@level2name=N'Farm_id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_ColumnOrder', @value=0 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Farm', @level2type=N'COLUMN',@level2name=N'Farm_id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_ColumnWidth', @value=-1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Farm', @level2type=N'COLUMN',@level2name=N'Farm_id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_ColumnHidden', @value=0 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Farm', @level2type=N'COLUMN',@level2name=N'FarmName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_ColumnOrder', @value=0 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Farm', @level2type=N'COLUMN',@level2name=N'FarmName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_ColumnWidth', @value=1710 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Farm', @level2type=N'COLUMN',@level2name=N'FarmName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_ColumnHidden', @value=0 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Farm', @level2type=N'COLUMN',@level2name=N'FarmAddress'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_ColumnOrder', @value=0 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Farm', @level2type=N'COLUMN',@level2name=N'FarmAddress'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_ColumnWidth', @value=-1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Farm', @level2type=N'COLUMN',@level2name=N'FarmAddress'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DefaultView', @value=0x02 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Farm'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Filter', @value=NULL , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Farm'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_OrderBy', @value=NULL , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Farm'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_OrderByOn', @value=0 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Farm'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Orientation', @value=NULL , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Farm'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_TableMaxRecords', @value=10000 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Farm'
GO

ALTER TABLE [dbo].[Farm]  WITH CHECK ADD  CONSTRAINT [FK_Farm_Environment] FOREIGN KEY([Environment_id])
REFERENCES [dbo].[Environment] ([Environment_id])
GO

ALTER TABLE [dbo].[Farm] CHECK CONSTRAINT [FK_Farm_Environment]
GO






SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Server](
	[Server_id] [int] IDENTITY(1,1) NOT NULL,
	[ServerName] [varchar](50) NOT NULL,
	[ServerAddress] [varchar](255) NOT NULL,
	[Farm_id] [int] NOT NULL,
 CONSTRAINT [PK_Server] PRIMARY KEY CLUSTERED 
(
	[Server_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Server]  WITH CHECK ADD  CONSTRAINT [FK_Server_Farm] FOREIGN KEY([Farm_id])
REFERENCES [dbo].[Farm] ([Farm_id])
GO

ALTER TABLE [dbo].[Server] CHECK CONSTRAINT [FK_Server_Farm]
GO






SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ChannelType_lkp](
	[ChannelType_id] [int] IDENTITY(1,1) NOT NULL,
	[ChannelTypeName] [varchar](50) NOT NULL,
 CONSTRAINT [PK_ChannelType_lkp] PRIMARY KEY CLUSTERED 
(
	[ChannelType_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO






SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[FarmAccessibility](
	[FarmAccessibility_id] [int] IDENTITY(1,1) NOT NULL,
	[ClientFarm_id] [int] NOT NULL,
	[ServerFarm_id] [int] NOT NULL,
	[ChannelType_id] [int] NOT NULL,
 CONSTRAINT [PK_FarmAccessibility] PRIMARY KEY CLUSTERED 
(
	[FarmAccessibility_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[FarmAccessibility]  WITH CHECK ADD  CONSTRAINT [FK_FarmAccessibility_ChannelType_lkp] FOREIGN KEY([ChannelType_id])
REFERENCES [dbo].[ChannelType_lkp] ([ChannelType_id])
GO

ALTER TABLE [dbo].[FarmAccessibility] CHECK CONSTRAINT [FK_FarmAccessibility_ChannelType_lkp]
GO

ALTER TABLE [dbo].[FarmAccessibility]  WITH CHECK ADD  CONSTRAINT [FK_FarmAccessibility_ClientFarm] FOREIGN KEY([ClientFarm_id])
REFERENCES [dbo].[Farm] ([Farm_id])
GO

ALTER TABLE [dbo].[FarmAccessibility] CHECK CONSTRAINT [FK_FarmAccessibility_ClientFarm]
GO

ALTER TABLE [dbo].[FarmAccessibility]  WITH CHECK ADD  CONSTRAINT [FK_FarmAccessibility_ServerFarm] FOREIGN KEY([ServerFarm_id])
REFERENCES [dbo].[Farm] ([Farm_id])
GO

ALTER TABLE [dbo].[FarmAccessibility] CHECK CONSTRAINT [FK_FarmAccessibility_ServerFarm]
GO






SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[BehaviorCategory_lkp](
	[BehaviorCategory_id] [int] IDENTITY(1,1) NOT NULL,
	[BehaviorCategoryName] [varchar](50) NOT NULL,
 CONSTRAINT [PK_BehaviorTypeGroup_lkp] PRIMARY KEY CLUSTERED 
(
	[BehaviorCategory_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO






SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CustomBehaviorType_lkp](
	[BehaviorType_id] [int] IDENTITY(1,1) NOT NULL,
	[BehaviorTypeExtensionName] [varchar](100) NULL,
	[BehaviorTypeFriendlyName] [varchar](100) NOT NULL,
	[BehaviorTypeClassName] [varchar](255) NOT NULL,
	[BehaviorConfigurationElementTypeClassName] [varchar](255) NOT NULL,
	[BehaviorCategory_id] [int] NOT NULL,
 CONSTRAINT [PK_BehaviorType_lkp] PRIMARY KEY CLUSTERED 
(
	[BehaviorType_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[CustomBehaviorType_lkp]  WITH CHECK ADD  CONSTRAINT [FK_BehaviorType_lkp_BehaviorCategory_lkp] FOREIGN KEY([BehaviorCategory_id])
REFERENCES [dbo].[BehaviorCategory_lkp] ([BehaviorCategory_id])
GO

ALTER TABLE [dbo].[CustomBehaviorType_lkp] CHECK CONSTRAINT [FK_BehaviorType_lkp_BehaviorCategory_lkp]
GO






SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Behavior](
	[Behavior_id] [int] IDENTITY(1,1) NOT NULL,
	[BehaviorName] [varchar](100) NOT NULL,
	[BehaviorXML] [varchar](max) NOT NULL,
	[BehaviorCategory_id] [int] NOT NULL,
 CONSTRAINT [PK_Behavior] PRIMARY KEY CLUSTERED 
(
	[Behavior_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Behavior]  WITH CHECK ADD  CONSTRAINT [FK_Behavior_BehaviorCategory_lkp] FOREIGN KEY([BehaviorCategory_id])
REFERENCES [dbo].[BehaviorCategory_lkp] ([BehaviorCategory_id])
GO

ALTER TABLE [dbo].[Behavior] CHECK CONSTRAINT [FK_Behavior_BehaviorCategory_lkp]
GO






SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[BindingType_lkp](
	[BindingType_id] [int] IDENTITY(1,1) NOT NULL,
	[BindingTypeFriendlyName] [varchar](100) NOT NULL,
	[BindingTypeClassName] [varchar](255) NOT NULL,
	[BindingConfigurationElementTypeClassName] [varchar](255) NOT NULL,
	[ChannelType_id] [int] NOT NULL,
 CONSTRAINT [PK_BindingType_lkp] PRIMARY KEY CLUSTERED 
(
	[BindingType_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[BindingType_lkp]  WITH CHECK ADD  CONSTRAINT [FK_BindingType_lkp_ChannelType_lkp] FOREIGN KEY([ChannelType_id])
REFERENCES [dbo].[ChannelType_lkp] ([ChannelType_id])
GO

ALTER TABLE [dbo].[BindingType_lkp] CHECK CONSTRAINT [FK_BindingType_lkp_ChannelType_lkp]
GO






SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Binding](
	[Binding_id] [int] IDENTITY(1,1) NOT NULL,
	[BindingType_id] [int] NOT NULL,
	[BindingName] [varchar](100) NOT NULL,
	[BindingXML] [varchar](max) NULL,
	[MexBindingEnabled] [bit] NOT NULL,
	[AddMexBindingOnly] [bit] NOT NULL,
 CONSTRAINT [PK_Binding] PRIMARY KEY CLUSTERED 
(
	[Binding_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Binding]  WITH CHECK ADD  CONSTRAINT [FK_Binding_BindingType_lkp] FOREIGN KEY([BindingType_id])
REFERENCES [dbo].[BindingType_lkp] ([BindingType_id])
GO

ALTER TABLE [dbo].[Binding] CHECK CONSTRAINT [FK_Binding_BindingType_lkp]
GO

ALTER TABLE [dbo].[Binding] ADD  CONSTRAINT [DF_Binding_MexBindingEnabled]  DEFAULT ((0)) FOR [MexBindingEnabled]
GO

ALTER TABLE [dbo].[Binding] ADD  CONSTRAINT [DF_Binding_AddMexBindingOnly]  DEFAULT ((0)) FOR [AddMexBindingOnly]
GO






SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ServiceCategory](
	[ServiceCategory_id] [int] IDENTITY(1,1) NOT NULL,
	[ServiceCategoryName] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_ServiceCategory] PRIMARY KEY CLUSTERED 
(
	[ServiceCategory_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO






SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ServiceHostType_lkp](
	[ServiceHostType_id] [int] IDENTITY(1,1) NOT NULL,
	[ServiceHostTypeFriendlyName] [varchar](50) NOT NULL,
	[ServiceHostTypeClassName] [varchar](255) NOT NULL,
 CONSTRAINT [PK_ServiceHostType_lkp] PRIMARY KEY CLUSTERED 
(
	[ServiceHostType_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO






SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[App](
	[AppCode] [varchar](10) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](500) NULL,
 CONSTRAINT [PK_App] PRIMARY KEY CLUSTERED 
(
	[AppCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO






SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Service](
	[Service_id] [int] IDENTITY(1,1) NOT NULL,
	[ServiceName] [varchar](255) NOT NULL,
	[AppCode] [varchar](10) NOT NULL,
	[ServiceCategory_id] [int] NULL,
	[ServiceBehavior_id] [int] NULL,
	[ServiceHostType_id] [int] NOT NULL,
	[HostXML] [varchar](max) NULL,
 CONSTRAINT [PK_Service] PRIMARY KEY CLUSTERED 
(
	[Service_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Service]  WITH CHECK ADD  CONSTRAINT [FK_Service_App] FOREIGN KEY([AppCode])
REFERENCES [dbo].[App] ([AppCode])
GO

ALTER TABLE [dbo].[Service] CHECK CONSTRAINT [FK_Service_App]
GO

ALTER TABLE [dbo].[Service]  WITH CHECK ADD  CONSTRAINT [FK_Service_ServiceHostType_lkp] FOREIGN KEY([ServiceHostType_id])
REFERENCES [dbo].[ServiceHostType_lkp] ([ServiceHostType_id])
GO

ALTER TABLE [dbo].[Service] CHECK CONSTRAINT [FK_Service_ServiceHostType_lkp]
GO






SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ServiceContractCompatibility](
	[ServiceContract] [varchar](255) NOT NULL,
	[CompatibleServiceContract] [varchar](255) NOT NULL,
 CONSTRAINT [PK_ServiceContractCompatibility] PRIMARY KEY CLUSTERED 
(
	[ServiceContract] ASC,
	[CompatibleServiceContract] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO






SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[EndpointListenUriMode_lkp](
	[ListenUriMode_id] [int] IDENTITY(1,1) NOT NULL,
	[ListenUriModeName] [varchar](50) NOT NULL,
 CONSTRAINT [PK_ListenUriMode_lkp] PRIMARY KEY CLUSTERED 
(
	[ListenUriMode_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO






SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Endpoint](
	[Endpoint_id] [int] IDENTITY(1,1) NOT NULL,
	[EndpointName] [varchar](100) NOT NULL,
	[EndpointAddress] [varchar](255) NOT NULL,
	[Binding_id] [int] NOT NULL,
	[ServiceContract] [varchar](255) NOT NULL,
	[EndpointBehavior_id] [int] NULL,
	[BindingNamespace] [varchar](255) NULL,
	[IdentityXML] [varchar](max) NULL,
	[ListenUri] [varchar](255) NULL,
	[ListenUriMode_id] [int] NULL,
 CONSTRAINT [PK_Endpoint] PRIMARY KEY CLUSTERED 
(
	[Endpoint_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Endpoint]  WITH CHECK ADD  CONSTRAINT [FK_Endpoint_Binding] FOREIGN KEY([Binding_id])
REFERENCES [dbo].[Binding] ([Binding_id])
GO

ALTER TABLE [dbo].[Endpoint] CHECK CONSTRAINT [FK_Endpoint_Binding]
GO






SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ServiceEndpoint_lnk](
	[Service_id] [int] NOT NULL,
	[Endpoint_id] [int] NOT NULL,
	[Farm_id] [int] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_ServiceEndpoint_lnk] PRIMARY KEY CLUSTERED 
(
	[Service_id] ASC,
	[Endpoint_id] ASC,
	[Farm_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[ServiceEndpoint_lnk]  WITH CHECK ADD  CONSTRAINT [FK_ServiceEndpoint_lnk_Endpoint] FOREIGN KEY([Endpoint_id])
REFERENCES [dbo].[Endpoint] ([Endpoint_id])
GO

ALTER TABLE [dbo].[ServiceEndpoint_lnk] CHECK CONSTRAINT [FK_ServiceEndpoint_lnk_Endpoint]
GO

ALTER TABLE [dbo].[ServiceEndpoint_lnk]  WITH CHECK ADD  CONSTRAINT [FK_ServiceEndpoint_lnk_Farm] FOREIGN KEY([Farm_id])
REFERENCES [dbo].[Farm] ([Farm_id])
GO

ALTER TABLE [dbo].[ServiceEndpoint_lnk] CHECK CONSTRAINT [FK_ServiceEndpoint_lnk_Farm]
GO

ALTER TABLE [dbo].[ServiceEndpoint_lnk]  WITH CHECK ADD  CONSTRAINT [FK_ServiceEndpoint_lnk_Service] FOREIGN KEY([Service_id])
REFERENCES [dbo].[Service] ([Service_id])
GO

ALTER TABLE [dbo].[ServiceEndpoint_lnk] CHECK CONSTRAINT [FK_ServiceEndpoint_lnk_Service]
GO

ALTER TABLE [dbo].[ServiceEndpoint_lnk] ADD  CONSTRAINT [DF_ServiceEndpoint_lnk_Active]  DEFAULT ((1)) FOR [Active]
GO






SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[AppVariable](
	[AppVariableName] [varchar](255) NOT NULL,
	[AppCode] [varchar](10) NOT NULL,
	[Environment_id] [int] NOT NULL,
	[Value] [nvarchar](max) NULL,
	[Description] [nvarchar](500) NULL,
 CONSTRAINT [PK_Variable] PRIMARY KEY CLUSTERED 
(
	[AppVariableName] ASC,
	[AppCode] ASC,
	[Environment_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[AppVariable]  WITH CHECK ADD  CONSTRAINT [FK_AppVariable_AppVariable] FOREIGN KEY([AppCode])
REFERENCES [dbo].[App] ([AppCode])
GO

ALTER TABLE [dbo].[AppVariable] CHECK CONSTRAINT [FK_AppVariable_AppVariable]
GO

ALTER TABLE [dbo].[AppVariable]  WITH CHECK ADD  CONSTRAINT [FK_AppVariable_Environment] FOREIGN KEY([Environment_id])
REFERENCES [dbo].[Environment] ([Environment_id])
GO

ALTER TABLE [dbo].[AppVariable] CHECK CONSTRAINT [FK_AppVariable_Environment]
GO






SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[EndpointClient](
	[Endpoint_id] [int] NOT NULL,
	[ClientFarm_id] [int] NOT NULL,
	[ClientEndpointBehavior_id] [int] NULL,
 CONSTRAINT [PK_EndpointClient] PRIMARY KEY CLUSTERED 
(
	[Endpoint_id] ASC,
	[ClientFarm_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[EndpointClient]  WITH CHECK ADD  CONSTRAINT [FK_EndpointClient_Behavior] FOREIGN KEY([ClientEndpointBehavior_id])
REFERENCES [dbo].[Behavior] ([Behavior_id])
GO

ALTER TABLE [dbo].[EndpointClient] CHECK CONSTRAINT [FK_EndpointClient_Behavior]
GO

ALTER TABLE [dbo].[EndpointClient]  WITH CHECK ADD  CONSTRAINT [FK_EndpointClient_Endpoint] FOREIGN KEY([Endpoint_id])
REFERENCES [dbo].[Endpoint] ([Endpoint_id])
GO

ALTER TABLE [dbo].[EndpointClient] CHECK CONSTRAINT [FK_EndpointClient_Endpoint]
GO

ALTER TABLE [dbo].[EndpointClient]  WITH CHECK ADD  CONSTRAINT [FK_EndpointClient_Farm] FOREIGN KEY([ClientFarm_id])
REFERENCES [dbo].[Farm] ([Farm_id])
GO

ALTER TABLE [dbo].[EndpointClient] CHECK CONSTRAINT [FK_EndpointClient_Farm]
GO





GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Teddy Ma
-- Create date: 2009-3-2
-- Description:	Get Connection String from 
-- ConnectionString table by Server Name
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetConnectionString]
	@ConnectionStringName as varchar(100)
	,@ServerName as varchar(50)
AS
BEGIN
	SET NOCOUNT ON;

    select [Value], [ProviderName] from ConnectionString
    where Name = @ConnectionStringName
		and Environment_id = (
			select Environment_id from Farm where Farm_id = (
				select Farm_id from [Server] where ServerName = @ServerName
			)
		)
END

GO






SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Teddy Ma
-- Create date: 2009-3-24
-- Description:	Get App Variable from 
-- AppVariable table by AppCode and Server Name
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetAppVariable]
	@AppVariableName as varchar(255)
	,@AppCode as varchar(10)
	,@ServerName as varchar(50)
AS
BEGIN
	SET NOCOUNT ON;

    select [Value] from AppVariable 
    where AppVariableName = @AppVariableName AND AppCode = @AppCode
		and Environment_id = (
			select Environment_id from Farm where Farm_id = (
				select Farm_id from [Server] where ServerName = @ServerName
			)
		)
END




SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Teddy Ma
-- Create date: 2009-4-6
-- Description:	Get all binding types.
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetAllBindingTypes]
AS
BEGIN
	SET NOCOUNT ON;

	SELECT * FROM BindingType_lkp
END

GO






SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Teddy Ma
-- Create date: 2009-4-6
-- Description:	Get all custom behavior types.
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetAllCustomBehaviorTypes]
AS
BEGIN
	SET NOCOUNT ON;

	SELECT * FROM CustomBehaviorType_lkp
END

GO






SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Teddy Ma
-- Create date: 2009-4-6
-- Description:	Get all service host types.
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetAllServiceHostTypes]
AS
BEGIN
	SET NOCOUNT ON;

	SELECT * FROM ServiceHostType_lkp
END

GO






SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Teddy Ma
-- Create date: 2009-4-8
-- Description:	Get client configuration 
--              by service contract, server name
--				and appCode
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetClientConfiguration]
	@ServiceContract as varchar(255)
	,@ServerName as varchar(50)
	,@AppCode as varchar(10)
AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT TOP 1 s.HostXML, f.FarmAddress, e.EndpointAddress, e.ListenUri, 
		e.ListenUriMode_id, e.BindingNamespace,
		(select BehaviorXML from Behavior where Behavior_id in
			(select ClientEndpointBehavior_id from EndpointClient where Endpoint_id = e.Endpoint_id and ClientFarm_id in 
					(select Farm_id from [Server] where ServerName = @ServerName)
			)
		) as EndpointBehaviorXML,
		e.IdentityXML, bd.BindingType_id, bd.BindingXML 
	FROM [Service] s
		inner join ServiceEndpoint_lnk se on se.Active = 1
			and s.Service_id = se.Service_id
		inner join Farm f on f.Farm_id = se.Farm_id
		inner join [Endpoint] e on se.Endpoint_id = e.Endpoint_id
		inner join [Binding] bd on bd.Binding_id = e.Binding_id 
		inner join BindingType_lkp bdt on bdt.BindingType_id = bd.BindingType_id		
	WHERE (e.ServiceContract = @ServiceContract
			or e.ServiceContract in
				(select ServiceContract
					from ServiceContractCompatibility
					where CompatibleServiceContract = @ServiceContract
				)
		)
		and se.Farm_id in
			(select fa.ServerFarm_id 
				from FarmAccessibility fa 
				where fa.ChannelType_id = bdt.ChannelType_id
					and fa.ClientFarm_id in 
					(
						select Farm_id 
						from [Server] 
						where ServerName = @ServerName
					)
			)
	ORDER BY bdt.ChannelType_id DESC, s.Service_id DESC
END





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Teddy Ma
-- Create date: 2009-4-8
-- Description:	Get service configuration 
--              by service contract and server name
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetServiceConfiguration]
	@ServiceName as varchar(255)
	,@ServerName as varchar(50)
	,@AppCode as varchar(10)
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @Service_id as int
	DECLARE @ServiceBehaviorXML as varchar(MAX)
	DECLARE @ServiceHostType_id as int
	DECLARE @HostXML as varchar(MAX)

	SELECT TOP 1 @Service_id = s.Service_id, 
		@ServiceBehaviorXML = sb.BehaviorXML, 
		@ServiceHostType_id = s.ServiceHostType_id, 
		@HostXML = s.HostXML
	FROM [Service] s
		left join Behavior sb on sb.Behavior_id = s.ServiceBehavior_id 
	WHERE s.AppCode = @AppCode and s.ServiceName = @ServiceName 
		and s.Service_id in
			(
				SELECT se.Service_id FROM ServiceEndpoint_lnk se
					inner join [Endpoint] e on se.Active = 1
						and se.Endpoint_id = e.Endpoint_id
				WHERE se.Farm_id in
						(SELECT Farm_id FROM [Server] WHERE ServerName = @ServerName)
			)
			
	SELECT @ServiceBehaviorXML as ServiceBehaviorXML, 
		@ServiceHostType_id as ServiceHostType_id, 
		@HostXML as HostXML
			
	SELECT e.EndpointAddress, e.ServiceContract,
		eb.BehaviorXML as EndpointBehaviorXML,
		e.BindingNamespace, e.IdentityXML, 
		bd.BindingType_id, bd.BindingXML, 
		bd.MexBindingEnabled, bd.AddMexBindingOnly,
		e.ListenUri, e.ListenUriMode_id
	FROM [Endpoint] e
		inner join ServiceEndpoint_lnk se on se.Active = 1
			and se.Endpoint_id = e.Endpoint_id
		inner join [Binding] bd on bd.Binding_id = e.Binding_id 
		left join Behavior eb on e.EndpointBehavior_id = eb.Behavior_id
	WHERE se.Service_id = @Service_id
END




SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Teddy Ma
-- Create date: 2009-4-21
-- Description:	Get Service Deployment
-- Configuration by ServerName & AppCode
-- =============================================
CREATE PROCEDURE sp_GetServiceDeploymentConfiguration
	@ServerName as varchar(50)
	,@AppCode as varchar(10)
AS
BEGIN
	SELECT s.ServiceName, s.HostXML, e.EndpointAddress, e.ListenUri FROM [Service] s
	inner join dbo.ServiceEndpoint_lnk se on s.Service_id = se.Service_id 
		and se.Active = 1 and se.Farm_id in 
		(SELECT Farm_id FROM [Server] WHERE ServerName = @ServerName)
	inner join [Endpoint] e on se.Endpoint_id = e.Endpoint_id
	WHERE s.AppCode = @AppCode
END
GO



GO

Insert Environment ([Name], [Description]) values ('Dev', N'Development Environment')



Insert   Farm   (FarmName,FarmAddress, Environment_id)      Values   (   'No Farm','localhost',1)



Insert   ChannelType_lkp   (ChannelTypeName)      Values   (   'MSMQ')
Insert   ChannelType_lkp   (ChannelTypeName)      Values   (   'HTTP')
Insert   ChannelType_lkp   (ChannelTypeName)      Values   (   'TCP')
Insert   ChannelType_lkp   (ChannelTypeName)      Values   (   'IPC')



Insert   FarmAccessibility   (ClientFarm_id,ServerFarm_id,ChannelType_id)      Values   (1,1,1)
Insert   FarmAccessibility   (ClientFarm_id,ServerFarm_id,ChannelType_id)      Values   (1,1,2)
Insert   FarmAccessibility   (ClientFarm_id,ServerFarm_id,ChannelType_id)      Values   (1,1,3)
Insert   FarmAccessibility   (ClientFarm_id,ServerFarm_id,ChannelType_id)      Values   (1,1,4)




Insert   BehaviorCategory_lkp   (BehaviorCategoryName)      Values   ('Service')
Insert   BehaviorCategory_lkp   (BehaviorCategoryName)      Values   ('Endpoint')



Insert   BindingType_lkp   (BindingTypeFriendlyName,BindingTypeClassName,BindingConfigurationElementTypeClassName,ChannelType_id)      Values   ('BasicHttpBinding','System.ServiceModel.BasicHttpBinding, System.ServiceModel, Version=3.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089','System.ServiceModel.Configuration.BasicHttpBindingElement, System.ServiceModel, Version=3.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089',2)
Insert   BindingType_lkp   (BindingTypeFriendlyName,BindingTypeClassName,BindingConfigurationElementTypeClassName,ChannelType_id)      Values   ('BasicHttpContextBinding','System.ServiceModel.BasicHttpContextBinding, System.WorkflowServices, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35','System.ServiceModel.Configuration.BasicHttpContextBindingElement, System.WorkflowServices, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35',2)
Insert   BindingType_lkp   (BindingTypeFriendlyName,BindingTypeClassName,BindingConfigurationElementTypeClassName,ChannelType_id)      Values   ('MsmqIntegrationBinding','System.ServiceModel.MsmqIntegration.MsmqIntegrationBinding, System.ServiceModel, Version=3.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089','System.ServiceModel.Configuration.MsmqIntegrationBindingElement, System.ServiceModel, Version=3.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089',1)
Insert   BindingType_lkp   (BindingTypeFriendlyName,BindingTypeClassName,BindingConfigurationElementTypeClassName,ChannelType_id)      Values   ('NetNamedPipeBinding','System.ServiceModel.NetNamedPipeBinding, System.ServiceModel, Version=3.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089','System.ServiceModel.Configuration.NetNamedPipeBindingElement, System.ServiceModel, Version=3.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089',4)
Insert   BindingType_lkp   (BindingTypeFriendlyName,BindingTypeClassName,BindingConfigurationElementTypeClassName,ChannelType_id)      Values   ('NetPeerTcpBinding','System.ServiceModel.NetPeerTcpBinding, System.ServiceModel, Version=3.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089','System.ServiceModel.Configuration.NetPeerTcpBindingElement, System.ServiceModel, Version=3.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089',3)
Insert   BindingType_lkp   (BindingTypeFriendlyName,BindingTypeClassName,BindingConfigurationElementTypeClassName,ChannelType_id)      Values   ('NetTcpBinding','System.ServiceModel.NetTcpBinding, System.ServiceModel, Version=3.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089','System.ServiceModel.Configuration.NetTcpBindingElement, System.ServiceModel, Version=3.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089',3)
Insert   BindingType_lkp   (BindingTypeFriendlyName,BindingTypeClassName,BindingConfigurationElementTypeClassName,ChannelType_id)      Values   ('NetTcpContextBinding','System.ServiceModel.NetTcpContextBinding, System.WorkflowServices, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35','System.ServiceModel.Configuration.NetTcpContextBindingElement, System.WorkflowServices, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35',3)
Insert   BindingType_lkp   (BindingTypeFriendlyName,BindingTypeClassName,BindingConfigurationElementTypeClassName,ChannelType_id)      Values   ('WebHttpBinding','System.ServiceModel.WebHttpBinding, System.ServiceModel.Web, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35','System.ServiceModel.Configuration.WebHttpBindingElement, System.ServiceModel.Web, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35',2)
Insert   BindingType_lkp   (BindingTypeFriendlyName,BindingTypeClassName,BindingConfigurationElementTypeClassName,ChannelType_id)      Values   ('WSDualHttpBinding','System.ServiceModel.WSDualHttpBinding, System.ServiceModel, Version=3.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089','System.ServiceModel.Configuration.WSDualHttpBindingElement, System.ServiceModel, Version=3.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089',2)
Insert   BindingType_lkp   (BindingTypeFriendlyName,BindingTypeClassName,BindingConfigurationElementTypeClassName,ChannelType_id)      Values   ('WSFederationHttpBinding','System.ServiceModel.WSFederationHttpBinding, System.ServiceModel, Version=3.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089','System.ServiceModel.Configuration.WSFederationHttpBindingElement, System.ServiceModel, Version=3.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089',2)
Insert   BindingType_lkp   (BindingTypeFriendlyName,BindingTypeClassName,BindingConfigurationElementTypeClassName,ChannelType_id)      Values   ('WSHttpBinding','System.ServiceModel.WSHttpBinding, System.ServiceModel, Version=3.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089','System.ServiceModel.Configuration.WSHttpBindingElement, System.ServiceModel, Version=3.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089',2)
Insert   BindingType_lkp   (BindingTypeFriendlyName,BindingTypeClassName,BindingConfigurationElementTypeClassName,ChannelType_id)      Values   ('NetMsmqBinding','System.ServiceModel.NetMsmqBinding, System.ServiceModel, Version=3.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089','System.ServiceModel.Configuration.NetMsmqBindingElement, System.ServiceModel, Version=3.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089',1)
Insert   BindingType_lkp   (BindingTypeFriendlyName,BindingTypeClassName,BindingConfigurationElementTypeClassName,ChannelType_id)      Values   ('CustomMsmqBinding','System.ServiceModel.CustomBinding, System.ServiceModel, Version=3.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089','System.ServiceModel.Configuration.CustomBindingElement, System.ServiceModel, Version=3.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089',1)
Insert   BindingType_lkp   (BindingTypeFriendlyName,BindingTypeClassName,BindingConfigurationElementTypeClassName,ChannelType_id)      Values   ('CustomHttpBinding','System.ServiceModel.CustomBinding, System.ServiceModel, Version=3.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089','System.ServiceModel.Configuration.CustomBindingElement, System.ServiceModel, Version=3.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089',2)
Insert   BindingType_lkp   (BindingTypeFriendlyName,BindingTypeClassName,BindingConfigurationElementTypeClassName,ChannelType_id)      Values   ('CustomTcpBinding','System.ServiceModel.CustomBinding, System.ServiceModel, Version=3.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089','System.ServiceModel.Configuration.CustomBindingElement, System.ServiceModel, Version=3.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089',3)
Insert   BindingType_lkp   (BindingTypeFriendlyName,BindingTypeClassName,BindingConfigurationElementTypeClassName,ChannelType_id)      Values   ('CustomIpcBinding','System.ServiceModel.CustomBinding, System.ServiceModel, Version=3.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089','System.ServiceModel.Configuration.CustomBindingElement, System.ServiceModel, Version=3.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089',4)




Insert   ServiceHostType_lkp   (ServiceHostTypeFriendlyName,ServiceHostTypeClassName)      Values   ('ServiceHost','System.ServiceModel.ServiceHost, System.ServiceModel, Version=3.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089')
Insert   ServiceHostType_lkp   (ServiceHostTypeFriendlyName,ServiceHostTypeClassName)      Values   ('WebServiceHost','System.ServiceModel.WebServiceHost, System.ServiceModel.Web, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35')
Insert   ServiceHostType_lkp   (ServiceHostTypeFriendlyName,ServiceHostTypeClassName)      Values   ('WorkflowServiceHost','System.ServiceModel.WorkflowServiceHost, System.WorkflowServices, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35')



insert [EndpointListenUriMode_lkp] ([ListenUriModeName]) values ('Explicit')
insert [EndpointListenUriMode_lkp] ([ListenUriModeName]) values ('Unique')



GO
