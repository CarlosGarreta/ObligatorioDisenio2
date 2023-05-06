<%@ Page Title="" Language="C#" MasterPageFile="~/MPEmpleado.master" AutoEventWireup="true" CodeFile="ABMSecciones.aspx.cs" Inherits="ABMSecciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style5 {
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
    <table class="style1">
        <tr>
            <td colspan="3" class="auto-style5">Mantenimiento Secciones</td>
        </tr>
        <tr>
            <td class="auto-style7">Codigo :</td>
            <td class="auto-style8">
                <asp:TextBox ID="txtCode" runat="server"></asp:TextBox>
                &nbsp;&nbsp; <asp:Button CssClass ="btn" ID="btnBuscar" runat="server" Text="Buscar" onclick="btnBuscar_Click" />
            </td>
        </tr>
        <tr>
            <td class="auto-style7">Nombre :</td>
            <td class="auto-style8">
                <asp:TextBox ID="txtNombre" runat="server" Enabled="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                &nbsp;&nbsp;
                <asp:Button CssClass ="btn" ID="btnAgregar" runat="server" Text="Agregar" 
                    onclick="btnAgregar_Click" Enabled="False" />
&nbsp;<asp:Button CssClass ="btn"  ID="btnModificar" runat="server" Text="Modificar" 
                    onclick="btnModificar_Click" Enabled="False" />
&nbsp;<asp:Button CssClass ="btn"  ID="btnEliminar" runat="server" Text="Eliminar" onclick="btnEliminar_Click" Enabled="False" />
&nbsp;&nbsp;
                <asp:Button CssClass ="btn"  ID="btnLimpiar" runat="server" Text="Limpiar formulario" 
                    onclick="btnLimpiar_Click" />
            </td>
        </tr>
        <tr>
            <td colspan="3" class="auto-style6">
                <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>

