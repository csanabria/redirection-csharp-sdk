<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Retorno.aspx.cs" Inherits="PlaceToPay.Integrations.WebForms.Tests.Retorno" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="text-align: center">
            Resultado recibido de PlaceToPay:<br />
            <br />
            <asp:Label ID="lblResultado" runat="server" Font-Bold="True" Font-Size="30pt"></asp:Label>
            <br />
            <br />
        <asp:TextBox ID="txtResultado" runat="server" Height="376px" Width="1156px" TextMode="MultiLine"></asp:TextBox>
            <br />
            <br />
        </div>
    </form>
</body>
</html>
