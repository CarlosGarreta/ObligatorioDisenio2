using System;
using ObligatorioServicio;
using System.Text.RegularExpressions;
public partial class ABMSecciones : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        lblError.Text = "";

        try
        {
            if (!Regex.IsMatch(txtCode.Text, "^[a-zA-Z]{5}$")) throw new Exception("El nombre de la seccion deben ser de 5 letras");
            SECCION seccionElegida = new ServicioEntity().BuscoSeccion(txtCode.Text);
            Session["seccionElegida"] = seccionElegida;

            txtCode.Enabled = false;
            btnBuscar.Enabled = false;
            txtNombre.Enabled = true;

            if (seccionElegida != null)
            {
                txtNombre.Text = seccionElegida.Nombre;
                btnModificar.Enabled = true;
                btnEliminar.Enabled = true;
            }
            else
            {
                btnAgregar.Enabled = true;
            }
        }
        catch (System.Web.Services.Protocols.SoapException ex) { lblError.Text = ex.Detail.InnerText; }
        catch (Exception ex) { lblError.Text = ex.Message; }
    }

    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        try
        {
            new ServicioEntity().AltaSeccion(new SECCION() { CodigoSeccion = txtCode.Text, Nombre = txtNombre.Text });
            Limpio();

            lblError.Text = "Alta con exito";
        }
        catch (System.Web.Services.Protocols.SoapException ex) { lblError.Text = ex.Detail.InnerText; }
        catch (Exception ex) { lblError.Text = ex.Message; }
    }

    protected void btnModificar_Click(object sender, EventArgs e)
    {
        try
        {
            SECCION unaS = (SECCION)Session["seccionElegida"];
            unaS.Nombre = txtNombre.Text;
            new ServicioEntity().ModificoSeccion(unaS);

            Limpio();
            lblError.Text = "Modificado con exito";
        }
        catch (System.Web.Services.Protocols.SoapException ex) { lblError.Text = ex.Detail.InnerText; }
        catch (Exception ex) { lblError.Text = ex.Message; }
    }

    protected void btnEliminar_Click(object sender, EventArgs e)
    {
        try
        {
            new ServicioEntity().EliminoSeccion((SECCION)Session["seccionElegida"]);
            Limpio();
            lblError.Text = "Eliminación con exito";
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
        txtCode.Text = "";
        txtCode.Enabled = true;
        txtNombre.Text = "";
        txtNombre.Enabled = false;
        lblError.Text = "";
        Session["seccionElegida"] = null;
        btnBuscar.Enabled = true;
        btnAgregar.Enabled = false;
        btnEliminar.Enabled = false;
        btnModificar.Enabled = false;
    }
}