using System;
using ObligatorioServicio;

public partial class ABMPeriodistas : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        lblError.Text = "";

        try
        {
            int ci = Convert.ToInt32(txtCI.Text);

            if (ci <= 0 || ci >= 10000000) throw new Exception("El numero de cedula no puede sobrepasar los 7 digitos");

            PERIODISTA periodistaElegido = new ServicioEntity().BuscoPeriodista(ci);
            Session["periodistaElegido"] = periodistaElegido;

            txtCI.Enabled = false;
            btnBuscar.Enabled = false;
            txtNombre.Enabled = true;
            txtMail.Enabled = true;

            if (periodistaElegido != null)
            {
                txtNombre.Text = periodistaElegido.Nombre;
                txtMail.Text = periodistaElegido.Mail;

                btnModificar.Enabled = true;
                btnEliminar.Enabled = true;
            }
            else
                btnAgregar.Enabled = true;
        }
        catch (System.Web.Services.Protocols.SoapException ex) { lblError.Text = ex.Detail.InnerText; }
        catch (Exception ex) { lblError.Text = ex.Message; }
    }

    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        try
        {
            new ServicioEntity().AltaPeriodista(new PERIODISTA() { Cedula = Convert.ToInt32(txtCI.Text), Nombre = txtNombre.Text, Mail = txtMail.Text });
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
            PERIODISTA unP = (PERIODISTA)Session["periodistaElegido"];
            unP.Nombre = txtNombre.Text;
            unP.Mail = txtMail.Text;

            new ServicioEntity().ModificoPeriodista(unP);

            Limpio();
            lblError.Text = "Modificación con exito";
        }
        catch (System.Web.Services.Protocols.SoapException ex) { lblError.Text = ex.Detail.InnerText; }
        catch (Exception ex) { lblError.Text = ex.Message; }

    }

    protected void btnEliminar_Click(object sender, EventArgs e)
    {
        try
        {
            new ServicioEntity().EliminoPeriodista((PERIODISTA)Session["periodistaElegido"]);

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
        txtCI.Text = "";
        txtCI.Enabled = true;
        txtNombre.Text = "";
        txtNombre.Enabled = false;
        txtMail.Text = "";
        txtMail.Enabled = false;
        txtMail.Enabled = false;
        Session["periodistaElegido"] = null;
        btnBuscar.Enabled = true;
        btnAgregar.Enabled = false;
        btnModificar.Enabled = false;
        btnEliminar.Enabled = false;
        lblError.Text = "";
    }
}