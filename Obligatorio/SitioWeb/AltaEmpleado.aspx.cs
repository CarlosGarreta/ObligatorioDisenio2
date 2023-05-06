using System;
using ObligatorioServicio;

using System.Text.RegularExpressions;

public partial class AltaEmpleado : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        try
        {
            if (!Regex.IsMatch(txtUser.Text, "^[a-zA-Z]{10}$")) throw new Exception("El nombre de empleado debe tener 10 letras");
            if (new ServicioEntity().BuscoEmpleado(txtUser.Text) != null) throw new Exception("El nombre de empleado ya fue inscrito");

            btnBuscar.Enabled = false;
            btnAgregar.Enabled = true;
            txtPass.Enabled = true;
            lblError.Text = "";
        }
        catch (System.Web.Services.Protocols.SoapException ex) { lblError.Text = ex.Detail.InnerText; }
        catch (Exception ex) { lblError.Text = ex.Message; }
    }

    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        try
        {
            new ServicioEntity().AltaEmpleado(new EMPLEADO { Usuario = txtUser.Text, Contrasenia = txtPass.Text });
            Limpio();
            lblError.Text = "Alta con exito";
        }
        catch (System.Web.Services.Protocols.SoapException ex) { lblError.Text = ex.Detail.InnerText; }
        catch (Exception ex) { lblError.Text = ex.Message; }
    }

    protected void btnLimpiar_Click(object sender, EventArgs e)
    {
        Limpio();
    }

    private void Limpio()
    {
        txtPass.Enabled = true;
        txtUser.Text = "";
        txtPass.Text = "";
        lblError.Text = "";
        btnAgregar.Enabled = false;
        btnBuscar.Enabled = true;
    }
}