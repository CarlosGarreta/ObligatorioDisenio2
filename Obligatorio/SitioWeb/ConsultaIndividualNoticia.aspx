<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConsultaIndividualNoticia.aspx.cs" Inherits="ConsultaIndividualNoticia" %>

<%@ Register src="ControlConsultaNoticia.ascx" tagname="ControlConsultaNoticia" tagprefix="uc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <uc1:ControlConsultaNoticia ID="ControlConsultaNoticia1" runat="server" />
        <br />
        <asp:LinkButton ID="lbVolver" runat="server" PostBackUrl="~/Default.aspx">Pagina principal</asp:LinkButton>
    
    </div>
    </form>
</body>
</html>
