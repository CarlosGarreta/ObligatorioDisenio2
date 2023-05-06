<%@ Page Title="" Language="C#" MasterPageFile="~/MPEmpleado.master" AutoEventWireup="true" CodeFile="AltaEmpleado.aspx.cs" Inherits="AltaEmpleado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1 {
            width: 500px;
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
    <table class="style1">
        <tr>
            <td colspan="2" class="auto-style5">Mantenimiento Empleados</td>
        </tr>
        <tr>
            <td class="style4">Usuario : </td>
            <td>
                <asp:TextBox ID="txtUser" runat="server"></asp:TextBox>
&nbsp;&nbsp; <asp:Button CssClass ="btn" ID="btnBuscar" runat="server" Text="Buscar" 
                    onclick="btnBuscar_Click" />
            </td>
        </tr>
        <tr>
            <td class="style4">Contraseña :</td>
            <td>
                <asp:TextBox ID="txtPass" runat="server" Enabled="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;&nbsp;
                <asp:Button CssClass ="btn" ID="btnAgregar" runat="server" Text="Agregar" 
                    onclick="btnAgregar_Click" Enabled="False" />
&nbsp;&nbsp; <asp:Button CssClass ="btn" ID="btnLimpiar" runat="server" Text="Limpiar formulario" 
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

