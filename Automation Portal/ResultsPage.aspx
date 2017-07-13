<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResultsPage.aspx.cs" Inherits="Automation_Portal.ResultsPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="resultsForm" runat="server">
        <div style="text-align:center">
            <h1>Infrastructure request has been successfully fulfilled</h1>
            <%=Context.Items["outputMsg"] %>
        </div>
    </form>
</body>
</html>
