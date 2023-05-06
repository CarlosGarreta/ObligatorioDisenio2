using System;
using System.Collections.Generic;
using ObligatorioServicio;
using System.Text.RegularExpressions;
using System.Linq;

public partial class AltaModificacionNoticias : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                Session["ddlseccion"] = new ServicioEntity().ListoSecciones().ToList();

                ddlSeccion.DataSource = (List<SECCION>)Session["ddlseccion"];
                ddlSeccion.DataTextField = "Nombre";
                ddlSeccion.DataValueField = "CodigoSeccion";
                ddlSeccion.DataBind();
            }
            catch (Exception ex) { lblError.Text = ex.Message; }
        }
    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        lblError.Text = "";

        try
        {
            if (!Regex.IsMatch(txtCode.Text, @"^\w{4}$")) throw new Exception("El codigo de la noticia debe ser 4 letras");

            Session["noticiaElegida"] = new ServicioEntity().BuscoSeccion(txtCode.Text);

            NOTICIA noticiaElegida = (NOTICIA)Session["noticiaElegida"];

            btnBuscar.Enabled = false;
            btnAgregarPeriodista.Enabled = true;
            btnEliminarPeriodista.Enabled = true;
            txtTitulo.Enabled = true;
            Calendar1.Enabled = true;
            ddlImportancia.Enabled = true;
            txtCuerpo.Enabled = true;
            ddlSeccion.Enabled = true;
            txtPeriodista.Enabled = true;
            lbPeriodistas.Enabled = true;

            btnBuscar.Enabled = false;

            if (noticiaElegida != null)
            {
                txtTitulo.Text = noticiaElegida.Titulo;
                Calendar1.SelectedDate = noticiaElegida.Fecha;
                ddlImportancia.SelectedIndex = noticiaElegida.Importancia - 1;
                txtCuerpo.Text = noticiaElegida.Cuerpo;

                foreach (SECCION s in (List<SECCION>)Session["ddlseccion"])
                    if (s.CodigoSeccion == noticiaElegida.SECCION.CodigoSeccion)
                    {
                        ddlSeccion.SelectedIndex = ((List<SECCION>)Session["ddlseccion"]).IndexOf(s);
                        break;
                    }

                Session["periodistasNacional"] = noticiaElegida.PERIODISTA;

                btnModificar.Enabled = true;
            }
            else
            {
                Session["periodistasNacional"] = new List<PERIODISTA>();
                btnAgregar.Enabled = true;
            }

            txtCode.Enabled = false;
            lbPeriodistas.DataSource = (List<PERIODISTA>)Session["periodistasNacional"];
            lbPeriodistas.DataBind();
        }
        catch (System.Web.Services.Protocols.SoapException ex) { lblError.Text = ex.Detail.InnerText; }
        catch (Exception ex) { lblError.Text = ex.Message; }
    }

    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        try
        {
            new ServicioEntity().AltaNoticia(
                    new NOTICIA()
                    {
                        CodigoNoticia = txtCode.Text,
                        Titulo = txtTitulo.Text,
                        Fecha = Calendar1.SelectedDate,
                        Importancia = ddlImportancia.SelectedIndex + 1,
                        Cuerpo = txtCuerpo.Text,
                        Usuario = ((EMPLEADO)Session["empleadoLogueado"]).Usuario,
                        EMPLEADO = (EMPLEADO)Session["empleadoLogueado"],
                        PERIODISTA = ((List<PERIODISTA>)Session["periodistasNacional"]).ToArray(),
                        CodigoSeccion = (((List<SECCION>)Session["ddlseccion"])[ddlSeccion.SelectedIndex]).CodigoSeccion,
                        SECCION = ((List<SECCION>)Session["ddlseccion"])[ddlSeccion.SelectedIndex]
                    });

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
            NOTICIA unaN = (NOTICIA)Session["noticiaElegida"];
            unaN.Titulo = txtTitulo.Text;
            unaN.Fecha = Calendar1.SelectedDate;
            unaN.Importancia = ddlImportancia.SelectedIndex + 1;
            unaN.Cuerpo = txtCuerpo.Text;
            unaN.EMPLEADO = (EMPLEADO)Session["empleadoLogueado"];
            unaN.PERIODISTA = ((List<PERIODISTA>)Session["periodistasNacional"]).ToArray();
            unaN.SECCION = ((List<SECCION>)Session["ddlseccion"])[ddlSeccion.SelectedIndex];

            new ServicioEntity().ModificoNoticia(unaN);
            Limpio();
            lblError.Text = "Modificación con éxito";
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
        lblError.Text = "";
        txtCode.Text = "";
        txtTitulo.Text = "";
        Calendar1.SelectedDate = DateTime.Today;
        ddlImportancia.SelectedIndex = 0;
        txtCuerpo.Text = "";
        txtTitulo.Enabled = false;
        Calendar1.Enabled = false;
        ddlImportancia.Enabled = false;
        txtCuerpo.Enabled = false;
        ddlSeccion.Enabled = false;
        txtPeriodista.Enabled = false;
        lbPeriodistas.Enabled = false;
        Session["periodistasNacional"] = null;
        lbPeriodistas.Items.Clear();
        txtPeriodista.Text = "";
        ddlSeccion.SelectedIndex = 0;

        txtCode.Enabled = true;
        btnBuscar.Enabled = true;
        btnAgregar.Enabled = false;
        btnModificar.Enabled = false;
        btnAgregarPeriodista.Enabled = false;
        btnEliminarPeriodista.Enabled = false;
    }
    protected void btnAgregarPeriodista_Click(object sender, EventArgs e)
    {
        lblError.Text = "";

        try
        {
            PERIODISTA unP = new ServicioEntity().BuscoPeriodista(Convert.ToInt32(txtPeriodista.Text));

            if (unP == null) throw new Exception("El periodista no existe");

            foreach (PERIODISTA p in (List<PERIODISTA>)Session["periodistasNacional"])
                if (p.Cedula == unP.Cedula) throw new Exception("El periodista ya se encuentra en la lista");

            ((List<PERIODISTA>)Session["periodistasNacional"]).Add(unP);

            lbPeriodistas.DataSource = (List<PERIODISTA>)Session["periodistasNacional"];
            lbPeriodistas.DataBind();
        }
        catch (Exception ex) { lblError.Text = ex.Message; }
    }

    protected void btnEliminarPeriodista_Click(object sender, EventArgs e)
    {
        lblError.Text = "";

        try
        {
            if (lbPeriodistas.SelectedIndex < 0) throw new Exception("Se debe seleccionar un periodista en la lista");

            ((List<PERIODISTA>)Session["periodistasNacional"]).RemoveAt(lbPeriodistas.SelectedIndex);
            lbPeriodistas.DataSource = (List<PERIODISTA>)Session["periodistasNacional"];
            lbPeriodistas.DataBind();
        }
        catch (Exception ex) { lblError.Text = ex.Message; }
    }
}