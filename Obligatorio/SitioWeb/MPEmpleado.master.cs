using System;
using ObligatorioServicio;

public partial class MPEmpleado : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblNombre.Text = ((EMPLEADO)Session["empleadoLogueado"]).Usuario;
        }
        catch
        {
            Session["empleadoLogueado"] = null;
            Response.Redirect("~/Default.aspx");
        }
    }

    protected void btnLogOut_Click(object sender, EventArgs e)
    {
        Session["empleadoLogueado"] = null;
        Response.Redirect("~/Default.aspx");
    }
}
