USE [ConfigurationDB]
GO
/****** Object:  Table [dbo].[Farm]    Script Date: 01/28/2010 22:51:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Farm](
	[Farm_id] [int] NOT NULL,
	[FarmAddress] [varchar](100) NOT NULL,
	[LoadBalancePath] [varchar](200) NOT NULL,
 CONSTRAINT [PK_Farm] PRIMARY KEY CLUSTERED 
(
	[Farm_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[Farm] ([Farm_id], [FarmAddress], [LoadBalancePath]) VALUES (0, N'localhost', N'/ConfigurationCenter/')
INSERT [dbo].[Farm] ([Farm_id], [FarmAddress], [LoadBalancePath]) VALUES (1, N'localhost', N'/Farm1/')
INSERT [dbo].[Farm] ([Farm_id], [FarmAddress], [LoadBalancePath]) VALUES (2, N'localhost', N'/Farm2/')
INSERT [dbo].[Farm] ([Farm_id], [FarmAddress], [LoadBalancePath]) VALUES (3, N'localhost', N'/Farm3/')
/****** Object:  Table [dbo].[ClientEndpoint]    Script Date: 01/28/2010 22:51:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ClientEndpoint](
	[ServiceContractType] [varchar](200) NOT NULL,
	[ServerName] [varchar](50) NOT NULL,
	[EndpointBehaviorXml] [xml] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[ClientEndpoint] ([ServiceContractType], [ServerName], [EndpointBehaviorXml]) VALUES (N'NIntegrate.Web.IQueryService, NIntegrate.Web', N'teddy', NULL)
INSERT [dbo].[ClientEndpoint] ([ServiceContractType], [ServerName], [EndpointBehaviorXml]) VALUES (N'DistributedEnterpriseIntegration.App1.Contracts.IApp1ReadonlyService, DistributedEnterpriseIntegration.App1.Contracts', N'teddy', NULL)
INSERT [dbo].[ClientEndpoint] ([ServiceContractType], [ServerName], [EndpointBehaviorXml]) VALUES (N'DistributedEnterpriseIntegration.App1.Contracts.IApp1ReadWriteService, DistributedEnterpriseIntegration.App1.Contracts', N'teddy', NULL)
INSERT [dbo].[ClientEndpoint] ([ServiceContractType], [ServerName], [EndpointBehaviorXml]) VALUES (N'DistributedEnterpriseIntegration.App2.Contracts.IApp2Service, DistributedEnterpriseIntegration.App2.Contracts', N'teddy', NULL)
/****** Object:  Table [dbo].[BindingType_lkp]    Script Date: 01/28/2010 22:51:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BindingType_lkp](
	[BindingTypeCode] [varchar](50) NOT NULL,
	[BndingTypeName] [varchar](50) NOT NULL,
	[Protocol] [varchar](20) NOT NULL,
	[Priority] [int] NOT NULL,
 CONSTRAINT [PK_BindingType_lkp] PRIMARY KEY CLUSTERED 
(
	[BindingTypeCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[BindingType_lkp] ([BindingTypeCode], [BndingTypeName], [Protocol], [Priority]) VALUES (N'basichttpbinding', N'basichttpbinding', N'http', 1)
INSERT [dbo].[BindingType_lkp] ([BindingTypeCode], [BndingTypeName], [Protocol], [Priority]) VALUES (N'wshttpbinding', N'wshttpbinding', N'http', 2)
/****** Object:  Table [dbo].[ServiceEndpoint]    Script Date: 01/28/2010 22:51:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ServiceEndpoint](
	[ServiceContractType] [varchar](200) NOT NULL,
	[ServiceType] [varchar](200) NOT NULL,
	[Farm_id] [int] NOT NULL,
	[BindingTypeCode] [varchar](50) NOT NULL,
	[BindingXml] [xml] NULL,
	[Address] [varchar](200) NOT NULL,
	[IdentityXml] [xml] NULL,
	[EndpointBehaviorXml] [xml] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_ServiceEndpoint] PRIMARY KEY CLUSTERED 
(
	[ServiceContractType] ASC,
	[ServiceType] ASC,
	[Farm_id] ASC,
	[BindingTypeCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[ServiceEndpoint] ([ServiceContractType], [ServiceType], [Farm_id], [BindingTypeCode], [BindingXml], [Address], [IdentityXml], [EndpointBehaviorXml], [IsActive]) VALUES (N'DistributedEnterpriseIntegration.App1.Contracts.IApp1ReadonlyService, DistributedEnterpriseIntegration.App1.Contracts', N'DistributedEnterpriseIntegration.App1.Implementation.App1ReadonlyService, DistributedEnterpriseIntegration.App1.Implementation', 1, N'wshttpbinding', N'<binding name="DefaultBinding" />', N':1624/Farm1/App1ReadonlyService.svc', NULL, NULL, 1)
INSERT [dbo].[ServiceEndpoint] ([ServiceContractType], [ServiceType], [Farm_id], [BindingTypeCode], [BindingXml], [Address], [IdentityXml], [EndpointBehaviorXml], [IsActive]) VALUES (N'DistributedEnterpriseIntegration.App1.Contracts.IApp1ReadonlyService, DistributedEnterpriseIntegration.App1.Contracts', N'DistributedEnterpriseIntegration.App1.Implementation.App1ReadonlyService, DistributedEnterpriseIntegration.App1.Implementation', 2, N'basichttpbinding', N'<binding name="DefaultBinding" />', N':1626/Farm2/App1ReadonlyService.svc', NULL, NULL, 1)
INSERT [dbo].[ServiceEndpoint] ([ServiceContractType], [ServiceType], [Farm_id], [BindingTypeCode], [BindingXml], [Address], [IdentityXml], [EndpointBehaviorXml], [IsActive]) VALUES (N'DistributedEnterpriseIntegration.App1.Contracts.IApp1ReadWriteService, DistributedEnterpriseIntegration.App1.Contracts', N'DistributedEnterpriseIntegration.App1.Implementation.App1ReadWriteService, DistributedEnterpriseIntegration.App1.Implementation', 1, N'wshttpbinding', N'<binding name="DefaultBinding" />', N':1624/Farm1/App1ReadWriteService.svc', NULL, NULL, 1)
INSERT [dbo].[ServiceEndpoint] ([ServiceContractType], [ServiceType], [Farm_id], [BindingTypeCode], [BindingXml], [Address], [IdentityXml], [EndpointBehaviorXml], [IsActive]) VALUES (N'DistributedEnterpriseIntegration.App2.Contracts.IApp2Service, DistributedEnterpriseIntegration.App2.Contracts', N'DistributedEnterpriseIntegration.App2.Implementation.App2Service, DistributedEnterpriseIntegration.App2.Implementation', 1, N'basichttpbinding', N'<binding name="DefaultBinding" />', N':1625/Farm1/App2Service.svc', NULL, NULL, 1)
INSERT [dbo].[ServiceEndpoint] ([ServiceContractType], [ServiceType], [Farm_id], [BindingTypeCode], [BindingXml], [Address], [IdentityXml], [EndpointBehaviorXml], [IsActive]) VALUES (N'DistributedEnterpriseIntegration.Framework.IWcfConfigurationService, DistributedEnterpriseIntegration.Framework', N'DistributedEnterpriseIntegration.ConfigurationCenter.WcfConfigurationService, DistributedEnterpriseIntegration.ConfigurationCenter', 0, N'basichttpbinding', N'<binding name="DefaultBinding" />', N':1473/ConfigurationCenter/WcfConfigurationService.svc', NULL, NULL, 1)
INSERT [dbo].[ServiceEndpoint] ([ServiceContractType], [ServiceType], [Farm_id], [BindingTypeCode], [BindingXml], [Address], [IdentityXml], [EndpointBehaviorXml], [IsActive]) VALUES (N'NIntegrate.Web.IQueryService, NIntegrate.Web', N'NIntegrate.Web.QueryService, NIntegrate.Web', 0, N'basichttpbinding', N'<binding name="DefaultBinding" />', N':1473/ConfigurationCenter/QueryService.svc', NULL, NULL, 1)
/****** Object:  Table [dbo].[Service]    Script Date: 01/28/2010 22:51:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Service](
	[ServiceType] [varchar](200) NOT NULL,
	[Farm_id] [int] NOT NULL,
	[ServiceName] [varchar](50) NOT NULL,
	[ServiceBehaviorXml] [xml] NULL,
 CONSTRAINT [PK_Service] PRIMARY KEY CLUSTERED 
(
	[ServiceType] ASC,
	[Farm_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[Service] ([ServiceType], [Farm_id], [ServiceName], [ServiceBehaviorXml]) VALUES (N'DistributedEnterpriseIntegration.App1.Implementation.App1ReadonlyService, DistributedEnterpriseIntegration.App1.Implementation', 1, N'App1ReadonlyService', NULL)
INSERT [dbo].[Service] ([ServiceType], [Farm_id], [ServiceName], [ServiceBehaviorXml]) VALUES (N'DistributedEnterpriseIntegration.App1.Implementation.App1ReadonlyService, DistributedEnterpriseIntegration.App1.Implementation', 2, N'App1ReadonlyService', NULL)
INSERT [dbo].[Service] ([ServiceType], [Farm_id], [ServiceName], [ServiceBehaviorXml]) VALUES (N'DistributedEnterpriseIntegration.App1.Implementation.App1ReadWriteService, DistributedEnterpriseIntegration.App1.Implementation', 1, N'App1ReadWriteService', NULL)
INSERT [dbo].[Service] ([ServiceType], [Farm_id], [ServiceName], [ServiceBehaviorXml]) VALUES (N'DistributedEnterpriseIntegration.App2.Implementation.App2Service, DistributedEnterpriseIntegration.App2.Implementation', 1, N'App2Service', NULL)
INSERT [dbo].[Service] ([ServiceType], [Farm_id], [ServiceName], [ServiceBehaviorXml]) VALUES (N'DistributedEnterpriseIntegration.ConfigurationCenter.WcfConfigurationService, DistributedEnterpriseIntegration.ConfigurationCenter', 0, N'WcfConfigurationService', NULL)
INSERT [dbo].[Service] ([ServiceType], [Farm_id], [ServiceName], [ServiceBehaviorXml]) VALUES (N'NIntegrate.Web.QueryService, NIntegrate.Web', 0, N'QueryService', NULL)
/****** Object:  Table [dbo].[Server]    Script Date: 01/28/2010 22:51:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Server](
	[ServerName] [varchar](50) NOT NULL,
	[Farm_id] [int] NOT NULL,
 CONSTRAINT [PK_Server_1] PRIMARY KEY CLUSTERED 
(
	[ServerName] ASC,
	[Farm_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[Server] ([ServerName], [Farm_id]) VALUES (N'teddy', 0)
INSERT [dbo].[Server] ([ServerName], [Farm_id]) VALUES (N'teddy', 1)
INSERT [dbo].[Server] ([ServerName], [Farm_id]) VALUES (N'teddy', 2)
INSERT [dbo].[Server] ([ServerName], [Farm_id]) VALUES (N'teddy', 3)
/****** Object:  Table [dbo].[FarmAccess]    Script Date: 01/28/2010 22:51:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[FarmAccess](
	[FromFarm_id] [int] NOT NULL,
	[ToFarm_id] [int] NOT NULL,
	[BindingTypeCode] [varchar](50) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_FarmAccess] PRIMARY KEY CLUSTERED 
(
	[FromFarm_id] ASC,
	[ToFarm_id] ASC,
	[BindingTypeCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[FarmAccess] ([FromFarm_id], [ToFarm_id], [BindingTypeCode], [IsActive]) VALUES (1, 0, N'basichttpbinding', 1)
INSERT [dbo].[FarmAccess] ([FromFarm_id], [ToFarm_id], [BindingTypeCode], [IsActive]) VALUES (2, 0, N'basichttpbinding', 1)
INSERT [dbo].[FarmAccess] ([FromFarm_id], [ToFarm_id], [BindingTypeCode], [IsActive]) VALUES (2, 1, N'wshttpbinding', 1)
INSERT [dbo].[FarmAccess] ([FromFarm_id], [ToFarm_id], [BindingTypeCode], [IsActive]) VALUES (3, 0, N'basichttpbinding', 1)
INSERT [dbo].[FarmAccess] ([FromFarm_id], [ToFarm_id], [BindingTypeCode], [IsActive]) VALUES (3, 1, N'basichttpbinding', 1)
INSERT [dbo].[FarmAccess] ([FromFarm_id], [ToFarm_id], [BindingTypeCode], [IsActive]) VALUES (3, 1, N'wshttpbinding', 1)
INSERT [dbo].[FarmAccess] ([FromFarm_id], [ToFarm_id], [BindingTypeCode], [IsActive]) VALUES (3, 2, N'basichttpbinding', 1)
/****** Object:  View [dbo].[vFarmAccess]    Script Date: 01/28/2010 22:51:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vFarmAccess]
AS
SELECT     fa.ToFarm_id, f.FarmAddress, f.LoadBalancePath, fa.BindingTypeCode
FROM         dbo.Farm AS f INNER JOIN
                      dbo.FarmAccess AS fa ON f.Farm_id = fa.FromFarm_id
WHERE     (fa.IsActive = 1)
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[41] 4[20] 2[15] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "f"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 108
               Right = 209
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "fa"
            Begin Extent = 
               Top = 6
               Left = 247
               Bottom = 123
               Right = 419
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vFarmAccess'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vFarmAccess'
GO
/****** Object:  StoredProcedure [dbo].[sp_GetWcfService]    Script Date: 01/28/2010 22:51:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetWcfService]
	@serviceType varchar(200)
	,@serverName varchar(50)
	,@loadBalancePath varchar(200)
AS
BEGIN
	SET NOCOUNT ON;

    select se.ServiceContractType, (bt.Protocol + '://localhost' +  se.[Address]) as [Address],
		se.BindingXml, se.IdentityXml, se.EndpointBehaviorXml, se.BindingTypeCode
    from dbo.ServiceEndpoint se
		inner join [Service] sc on sc.ServiceType = se.ServiceType and se.Farm_id = sc.Farm_id
		inner join Farm f on f.Farm_id = sc.Farm_id
		inner join [Server] s on s.Farm_id = sc.Farm_id
		inner join BindingType_lkp bt on bt.BindingTypeCode = se.BindingTypeCode
	where se.IsActive = 1 and s.ServerName = @serverName
		and se.ServiceType = @serviceType
		and f.LoadBalancePath like '%' + @loadBalancePath + '%'
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetWcfClientEndpoint]    Script Date: 01/28/2010 22:51:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetWcfClientEndpoint]
	@serviceContractType varchar(200)
	,@serverName varchar(50)
AS
BEGIN
	SET NOCOUNT ON;

    select top 1 se.ServiceContractType, (bt.Protocol + '://' + f.FarmAddress +  se.[Address]) as [Address],
		se.BindingXml, se.IdentityXml, ce.EndpointBehaviorXml, se.BindingTypeCode
	from ClientEndpoint ce
		inner join ServiceEndpoint se on se.ServiceContractType = ce.ServiceContractType
		inner join [Service] sc on se.ServiceType = sc.ServiceType and se.Farm_id = sc.Farm_id
		inner join Farm f on f.Farm_id = sc.Farm_id
		inner join BindingType_lkp bt on bt.BindingTypeCode = se.BindingTypeCode
	where ce.ServiceContractType = @serviceContractType and sc.Farm_id in
		(select fa.ToFarm_id 
			from FarmAccess fa 
			where fa.BindingTypeCode = bt.BindingTypeCode
				and fa.FromFarm_id in 
				(
					select Farm_id 
					from [Server] 
					where ServerName = @ServerName
				)
		)
	order by bt.Priority
END
GO
/****** Object:  Default [DF_ServiceEndpoint_BindingXml]    Script Date: 01/28/2010 22:51:06 ******/
ALTER TABLE [dbo].[ServiceEndpoint] ADD  CONSTRAINT [DF_ServiceEndpoint_BindingXml]  DEFAULT ('<binding name="DefaultBinding" />') FOR [BindingXml]
GO
/****** Object:  ForeignKey [FK_ServiceEndpoint_BindingType_lkp]    Script Date: 01/28/2010 22:51:06 ******/
ALTER TABLE [dbo].[ServiceEndpoint]  WITH CHECK ADD  CONSTRAINT [FK_ServiceEndpoint_BindingType_lkp] FOREIGN KEY([BindingTypeCode])
REFERENCES [dbo].[BindingType_lkp] ([BindingTypeCode])
GO
ALTER TABLE [dbo].[ServiceEndpoint] CHECK CONSTRAINT [FK_ServiceEndpoint_BindingType_lkp]
GO
/****** Object:  ForeignKey [FK_Service_Farm]    Script Date: 01/28/2010 22:51:06 ******/
ALTER TABLE [dbo].[Service]  WITH CHECK ADD  CONSTRAINT [FK_Service_Farm] FOREIGN KEY([Farm_id])
REFERENCES [dbo].[Farm] ([Farm_id])
GO
ALTER TABLE [dbo].[Service] CHECK CONSTRAINT [FK_Service_Farm]
GO
/****** Object:  ForeignKey [FK_Server_Farm]    Script Date: 01/28/2010 22:51:06 ******/
ALTER TABLE [dbo].[Server]  WITH CHECK ADD  CONSTRAINT [FK_Server_Farm] FOREIGN KEY([Farm_id])
REFERENCES [dbo].[Farm] ([Farm_id])
GO
ALTER TABLE [dbo].[Server] CHECK CONSTRAINT [FK_Server_Farm]
GO
/****** Object:  ForeignKey [FK_FarmAccess_BindingType_lkp]    Script Date: 01/28/2010 22:51:06 ******/
ALTER TABLE [dbo].[FarmAccess]  WITH CHECK ADD  CONSTRAINT [FK_FarmAccess_BindingType_lkp] FOREIGN KEY([BindingTypeCode])
REFERENCES [dbo].[BindingType_lkp] ([BindingTypeCode])
GO
ALTER TABLE [dbo].[FarmAccess] CHECK CONSTRAINT [FK_FarmAccess_BindingType_lkp]
GO
/****** Object:  ForeignKey [FK_FarmAccess_Farm]    Script Date: 01/28/2010 22:51:06 ******/
ALTER TABLE [dbo].[FarmAccess]  WITH CHECK ADD  CONSTRAINT [FK_FarmAccess_Farm] FOREIGN KEY([FromFarm_id])
REFERENCES [dbo].[Farm] ([Farm_id])
GO
ALTER TABLE [dbo].[FarmAccess] CHECK CONSTRAINT [FK_FarmAccess_Farm]
GO
/****** Object:  ForeignKey [FK_FarmAccess_Farm1]    Script Date: 01/28/2010 22:51:06 ******/
ALTER TABLE [dbo].[FarmAccess]  WITH CHECK ADD  CONSTRAINT [FK_FarmAccess_Farm1] FOREIGN KEY([ToFarm_id])
REFERENCES [dbo].[Farm] ([Farm_id])
GO
ALTER TABLE [dbo].[FarmAccess] CHECK CONSTRAINT [FK_FarmAccess_Farm1]
GO
