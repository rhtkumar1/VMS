<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportViewer.aspx.cs" Inherits="IMS.Reports.ReportViewer" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" /> 
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager runat="server"></asp:ScriptManager>        
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" CssClass="reportviewer" Font-Names="Verdana" BorderColor="Black" ShowBackButton="true" ShowPrintButton="true" ShowRefreshButton="true"
        BorderStyle="None" Font-Size="8pt" InteractiveDeviceInfos="(Collection)" WaitMessageFont-Names="Verdana" AsyncRendering="false" SizeToReportContent="true" 
        WaitMessageFont-Size="14pt" Height="100%" Width="100%" ProcessingMode="Local">
        </rsweb:ReportViewer>

        

    </form>
</body></html>


