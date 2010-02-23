USE [master]
GO

IF  EXISTS (SELECT name FROM sys.databases WHERE name = N'EAppointments')
	DROP DATABASE [EAppointments]
GO

CREATE DATABASE [EAppointments] 
GO

IF EXISTS (SELECT name FROM sys.sysdatabases WHERE  name = N'EAppointments_WF')
	DROP DATABASE EAppointments_WF
GO

CREATE DATABASE EAppointments_WF
GO

