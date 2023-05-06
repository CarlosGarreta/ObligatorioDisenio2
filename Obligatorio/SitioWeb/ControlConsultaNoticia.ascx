<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ControlConsultaNoticia.ascx.cs" Inherits="ControlConsultaNoticia" %>
<style type="text/css">
    * {
            font-family:'Calibri Light','Arial Narrow', Arial, sans-serif;
            letter-spacing:1px;
        }
    .auto-style1 {
        width: 100%;
    }
    .auto-style3 {
        width: 810px;
        height: 23px;
        background-color:#507CD1;
        text-align: right;
        padding-right:50px;
    }
    .auto-style4 {
        height: 23px;
        text-align: center;
        background-color: #EFF3FB;
    }
    .auto-style5 {
        width: 810px;
        background-color: #2461BF;
        text-align: center;
    }
    .auto-style6 {
        text-align: center;
        background-color:#EFF3FB;
    }
    .auto-style7 {
        color: #FFFFFF;
        font-size: x-large;
    }
    .auto-style8 {
        color: #FFFFFF;
    }
    .auto-style9 {
        text-align: center;
    }
    .auto-style10 {
        text-align: center;
        background-color: #2461BF;
    }
    .auto-style11 {
        text-align: center;
        background-color: #EFF3FB;
        width: 810px;
    }
    .auto-style12 {
        width: 810px;
    }
</style>

<table class="auto-style1">
    <tr>
        <td class="auto-style5" rowspan="2">
            <asp:Label ID="lbTitulo" runat="server" Text="Titulo" CssClass="auto-style7"></asp:Label>
        </td>
        <td class="auto-style10">
            <asp:Label ID="lbFecha" runat="server" Text="Fecha" CssClass="auto-style8"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="auto-style10"><span class="auto-style8">Importancia :
            </span>
            <asp:Label ID="lblImportancia" runat="server" Text="Label" CssClass="auto-style8"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="auto-style3">
            <asp:Label ID="lbSeccion" runat="server" Text="Seccion" CssClass="auto-style8"></asp:Label>
        </td>
        <td class="auto-style4">Empleado : <asp:Label ID="lbEmp" runat="server" Text="Empleado"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="auto-style11">
            <asp:Label ID="lbCuerpo" runat="server" Text="Cuerpo"></asp:Label>
        </td>
        <td class="auto-style6">Escrita por:<br />
            <asp:ListBox ID="lbperiodistas" runat="server" Width="253px"></asp:ListBox>
        </td>
    </tr>
    <tr>
        <td class="auto-style12"></td>
        <td class="auto-style9">
            <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
        </td>
    </tr>
</table>