<%@ Page Title="" Language="C#" MasterPageFile="~/MPEmpleado.master" AutoEventWireup="true" CodeFile="ABMPeriodistas.aspx.cs" Inherits="ABMPeriodistas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        * {
            font-family:'Calibri Light','Arial Narrow', Arial, sans-serif;
            letter-spacing:1px;
        }
        .style4
        {
            width: 145px;
        }
        .auto-style5 {
            width: 100%;
            font-size: xx-large;
            color: #0066FF;
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
            <td colspan="2" class="auto-style5">Mantenimiento Periodistas</td>
        </tr>
        <tr>
            <td>Cedula : </td>
            <td>
                <asp:TextBox ID="txtCI" runat="server" TextMode="Number"></asp:TextBox>
&nbsp;&nbsp; <asp:Button CssClass="btn" ID="btnBuscar" runat="server" Text="Buscar" onclick="btnBuscar_Click" />
            </td>
        </tr>
        <tr>
            <td>Nombre :</td>
            <td>
                <asp:TextBox ID="txtNombre" runat="server" Enabled="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Mail :</td>
            <td>
                <asp:TextBox ID="txtMail" runat="server" Enabled="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;&nbsp;
                <asp:Button CssClass="btn" ID="btnAgregar" runat="server" Text="Agregar" 
                    onclick="btnAgregar_Click" Enabled="False" />
&nbsp;<asp:Button CssClass="btn" ID="btnModificar" runat="server" Text="Modificar" 
                    onclick="btnModificar_Click" Enabled="False" />
&nbsp;<asp:Button CssClass="btn" ID="btnEliminar" runat="server" Text="Eliminar" onclick="btnEliminar_Click" Enabled="False" />
&nbsp;&nbsp;
                <asp:Button CssClass="btn" ID="btnLimpiar" runat="server" Text="Limpiar formulario" 
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
