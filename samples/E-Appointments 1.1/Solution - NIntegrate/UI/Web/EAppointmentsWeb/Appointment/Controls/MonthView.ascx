<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MonthView.ascx.cs" Inherits="Appointment_MonthView" %>
<asp:Calendar ID="monthCalendar" runat="server" OnDayRender="MonthCalendar_DayRender" SkinID="month" ShowNextPrevMonth="true"/>
