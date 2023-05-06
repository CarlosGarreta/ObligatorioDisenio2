<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

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
        .auto-style1 {
            width: 50%;
        }
        .auto-style3 {
            font-size: xx-large;
            color: #0000CC;
        }
        .auto-style4 {
            padding-top:10px;
        
            font-size: xx-large;
            color: #0066FF;
        }
        .auto-style5 {
            margin-top:0;
            width: 100%;
        }
        .auto-style6 {
            vertical-align:baseline;
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
        .auto-style7 {
            text-align: center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table class="auto-style1">
            <tr>
                <td class="auto-style6">
                    <table class="auto-style5">
                        <tr>
                            <td class="auto-style7">
                                <div class="auto-style7">
                                    <span class="auto-style4">Noticias</span><span class="auto-style3"> </span>
                    <br />
                    <br />
                                </div>
                    <br />
                    Seccion :
                    <br />
                    <asp:DropDownList ID="ddlSeccion" runat="server" Height="20px" Width="114px" AutoPostBack="False">
                    </asp:DropDownList>
                    <br />
                    <br />
                    Fecha:<br />
                    <asp:DropDownList ID="ddlFecha" runat="server" Height="20px" Width="114px" AutoPostBack="False">
                    </asp:DropDownList>
                    <br />
                    <br />
                    <asp:Button CssClass="btn" ID="btnAplicarFiltros" runat="server" OnClick="btnAplicarFiltros_Click" Text="Aplicar Filtros" />
                    <br />
                    <br />
                    <asp:Button CssClass="btn" ID="btnLimpiarFiltros" runat="server" Text="Quitar filtros" OnClick="btnLimpiarFiltros_Click" />
                            </td>
                        </tr>
                    </table>
                    <br />
                    </td>
                <td>
                    <asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl="~/Logueo.aspx">Ingreso Empleado</asp:LinkButton>
                    <br />
                    <br />
                    <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
                    <br />
                    <br />
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Height="242px" Width="1042px" OnSelectedIndexChanged="Grilla_SeleccionNoticia" CellPadding="4" ForeColor="#333333" GridLines="None">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:BoundField DataField="Importancia" />
                            <asp:BoundField DataField="Fecha" HeaderText="Fecha" DataFormatString="{0:MM/dd}" />
                            <asp:BoundField DataField="Titulo" HeaderText="Titulo" />
                            <asp:CommandField ShowSelectButton="True" SelectText="Ver noticia" />
                        </Columns>
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#EFF3FB" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                    </asp:GridView>
                </td>
            </tr>
            </table>
    
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    
    </div>
    </form>
</body>
</html>
