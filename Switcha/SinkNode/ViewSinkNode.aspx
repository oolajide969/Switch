﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Switch.Master" AutoEventWireup="true" CodeBehind="ViewSinkNode.aspx.cs" Inherits="Switcha.SinkNode.ViewSinkNode" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register assembly="AppZoneUI.Framework" namespace="AppZoneUI.Framework" tagprefix="cc1" %>


    <asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <ext:ResourceManager ID="ResourceManager1" runat="server"></ext:ResourceManager>
        <cc1:EntityUIControl ID="EntityUIControl1" runat="server" UIType = "Switcha.UI.SinkNodeUI.ViewSinkNode, Switcha.UI" />
</asp:Content>
        

        

