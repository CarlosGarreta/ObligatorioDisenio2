using System;
using ObligatorioServicio;

public partial class PaginaBienvenida : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Label1.Text = "Bienvenido " + ((EMPLEADO)Session["empleadoLogueado"]).Usuario;
        }
        catch
        {
            Session["empleadoLogueado"] = null;
            Response.Redirect("~/Default.aspx");
        }
    }
}