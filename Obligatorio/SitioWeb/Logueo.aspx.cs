using System;
using ObligatorioServicio;

public partial class Logueo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnLogIn_Click(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = "";

            EMPLEADO emp = new ServicioEntity().Logueo(txtUser.Text, txtPass.Text);
            if (emp == null) throw new Exception("El usuario y/o contraseña es incorrecta");
            Session["empleadoLogueado"] = emp;
            Response.Redirect("~/PaginaBienvenida.aspx");
        }
        catch (System.Web.Services.Protocols.SoapException ex) { lblError.Text = ex.Detail.InnerText; }
        catch (Exception ex) { lblError.Text = ex.Message; }
    }
}