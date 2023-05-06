<%@ Page Title="" Language="C#" MasterPageFile="~/MPEmpleado.master" AutoEventWireup="true" CodeFile="PaginaBienvenida.aspx.cs" Inherits="PaginaBienvenida" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            font-size: x-large;
            margin-left:100px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Label ID="Label1" runat="server" Text="Label" CssClass="auto-style1"></asp:Label>
</asp:Content>

