<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="usoRestSharp.aspx.cs" Inherits="PlaceToPay.Integrations.WebForms.Tests.usoRestSharp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <asp:Button ID="btn1" runat="server" OnClick="btn1_Click" Text="Uso1" />
            <br />
            <asp:Button ID="btn2" runat="server" OnClick="btn2_Click" Text="Uso2" />
            <br />

        </div>
        <asp:TextBox ID="txtResponse" runat="server" Height="376px" Width="1156px" TextMode="MultiLine"></asp:TextBox>
    </form>
</body>
</html>
