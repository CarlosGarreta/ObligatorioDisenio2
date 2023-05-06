<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Logueo.aspx.cs" Inherits="Logueo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        * {
            font-family:'Calibri Light','Arial Narrow', Arial, sans-serif;
            letter-spacing:1px;
        }
        .btn {
            background-color: #66AAFF;
            border:none;
            font-size:medium;
            color:#000000;
        }
        .btn:hover {
            background-color: #027ad6;
            color:#ffffff;
        }
        .auto-style1 {
            text-align: center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="auto-style1">
            Usuario&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:TextBox ID="txtUser" runat="server" Width="80px"></asp:TextBox>
                    <br />
            Contraseña&nbsp; <asp:TextBox ID="txtPass" runat="server" TextMode="Password" Width="80px"></asp:TextBox>
                    <br />
                    <br />
        <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
    
            <br />
    
            <br />
            <asp:Button CssClass ="btn"  ID="btnLogIn" runat="server" Text="Acceder" OnClick="btnLogIn_Click" />
            <br />
            <br />
            <asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl="~/Default.aspx">Pagina Principal</asp:LinkButton>
        <br />
        </div>
    </div>
    </form>
</body>
</html>
