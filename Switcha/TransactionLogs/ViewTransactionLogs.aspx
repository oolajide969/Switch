﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Switch.Master" AutoEventWireup="true" CodeBehind="ViewTransactionLog.aspx.cs" Inherits="Switcha.TransactionLogs.ViewTransactionLogs" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register assembly="AppZoneUI.Framework" namespace="AppZoneUI.Framework" tagprefix="cc1" %>


    <asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <ext:ResourceManager ID="ResourceManager1" runat="server"></ext:ResourceManager>
        <cc1:EntityUIControl ID="EntityUIControl1" runat="server" UIType = "Switcha.UI.TransactionLogsUI.ViewTransactionLog, Switcha.UI" />
</asp:Content>
        

        
