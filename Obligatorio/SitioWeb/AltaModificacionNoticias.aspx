<%@ Page Title="" Language="C#" MasterPageFile="~/MPEmpleado.master" AutoEventWireup="true" CodeFile="AltaModificacionNoticias.aspx.cs" Inherits="AltaModificacionNoticias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style4
        {
            width: 100px;
        }
        .auto-style4 {
            height: 23px;
        }
        .auto-style5 {
            width: 100%;
            color: #0066FF;
            font-size: xx-large;
            text-align: center;
        }
        .auto-style6 {
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table>
        <tr>
            <td colspan="2" class="auto-style5">Mantenimiento Nacionales</td>
        </tr>
        <tr>
            <td class="style4">Codigo : </td>
            <td>
                <asp:TextBox ID="txtCode" runat="server"></asp:TextBox>
&nbsp;<asp:Button CssClass ="btn" ID="btnBuscar" runat="server" Text="Buscar" onclick="btnBuscar_Click" />
            </td>
        </tr>
        <tr>
            <td class="style4">Titulo :</td>
            <td>
                <asp:TextBox ID="txtTitulo" runat="server" Enabled="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style4">Fecha :</td>
            <td>
                <asp:Calendar ID="Calendar1" runat="server" Enabled="False"></asp:Calendar>
            </td>
        </tr>
        <tr>
            <td class="style4">Importancia :</td>
            <td>
                <asp:DropDownList ID="ddlImportancia" runat="server" Enabled="False">
                    <asp:ListItem>1</asp:ListItem>
                    <asp:ListItem>2</asp:ListItem>
                    <asp:ListItem>3</asp:ListItem>
                    <asp:ListItem>4</asp:ListItem>
                    <asp:ListItem>5</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="style4">Cuerpo :</td>
            <td>
                <asp:TextBox ID="txtCuerpo" runat="server" Height="108px" Width="349px" Enabled="False" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style4">Seccion :</td>
            <td>
                <asp:DropDownList ID="ddlSeccion" runat="server" Enabled="False">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="style4" rowspan="2">Periodistas:</td>
            <td>
                <asp:ListBox ID="lbPeriodistas" runat="server" AutoPostBack="True" Enabled="False"></asp:ListBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txtPeriodista" runat="server" Enabled="False" TextMode="Number"></asp:TextBox>
                <asp:Button CssClass ="btn" ID="btnAgregarPeriodista" runat="server" Text="Agregar" 
                    onclick="btnAgregarPeriodista_Click" Enabled="False" />
&nbsp;<asp:Button CssClass ="btn" ID="btnEliminarPeriodista" runat="server" Text="Quitar" 
                    onclick="btnEliminarPeriodista_Click" Enabled="False" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button CssClass ="btn" ID="btnAgregar" runat="server" Text="Agregar noticia" 
                    onclick="btnAgregar_Click" Enabled="False" />
&nbsp;<asp:Button CssClass ="btn" ID="btnModificar" runat="server" Text="Modificar noticia" 
                    onclick="btnModificar_Click" Enabled="False" />
&nbsp;<asp:Button CssClass ="btn" ID="btnLimpiar" runat="server" Text="Limpiar formulario" 
                    onclick="btnLimpiar_Click" />
            </td>
        </tr>
        <tr>
            <td colspan="2" class="auto-style6">
                <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>

