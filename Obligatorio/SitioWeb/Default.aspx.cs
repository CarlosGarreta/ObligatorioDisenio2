using System;
using System.Collections.Generic;
using System.Linq;
using ObligatorioServicio;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                Session["ddlSeccion"] = new ServicioEntity().ListoSecciones().ToList();

                ddlSeccion.DataSource = (List<SECCION>)Session["ddlseccion"];
                ddlSeccion.DataTextField = "Nombre";
                ddlSeccion.DataValueField = "CodigoSeccion";
                ddlSeccion.DataBind();
                ddlSeccion.Items.Insert(0, "Todas");
                ddlSeccion.SelectedIndex = 0;

                List<DateTime> fechas = new List<DateTime>();
                ddlFecha.Items.Add("Todas");
                for (int i = 0; i < 5; i++)
                {
                    fechas.Add(DateTime.Today.AddDays(-i));
                    ddlFecha.Items.Add(DateTime.Today.AddDays(-i).ToString("yyyy-MM-dd"));
                }

                Session["ddlFechas"] = fechas;

                Session["NoticiasDesplegadas"] = new ServicioEntity().ListoNoticias().ToList();

                ActualizoGrilla();
            }
            catch (System.Web.Services.Protocols.SoapException ex) { lblError.Text = ex.Detail.InnerText; }
            catch (Exception ex) { lblError.Text = ex.Message; }
        }
    }

    protected void btnAplicarFiltros_Click(object sender, EventArgs e)
    {
        lblError.Text = "";

        try
        {
            if (ddlSeccion.SelectedIndex == 0) //sin seccion
            {
                if (ddlFecha.SelectedIndex == 0)
                    Session["NoticiasDesplegadas"] = new ServicioEntity().ListoNoticias().ToList();
                else
                    Session["NoticiasDesplegadas"] = new ServicioEntity().ListoNoticiasPorFecha(
                            ((List<DateTime>)Session["ddlFechas"])[ddlFecha.SelectedIndex]
                        ).ToList();
            }
            else //con seccion
            {
                if (ddlFecha.SelectedIndex == 0)
                    Session["NoticiasDesplegadas"] = new ServicioEntity().ListoNoticiasPorSeccion(
                            ((List<SECCION>)Session["ddlseccion"])[ddlSeccion.SelectedIndex]
                        ).ToList();
                else
                    Session["NoticiasDesplegadas"] = new ServicioEntity().ListoNoticiasPorSeccionyFecha(
                            ((List<SECCION>)Session["ddlseccion"])[ddlSeccion.SelectedIndex],
                            ((List<DateTime>)Session["ddlFechas"])[ddlFecha.SelectedIndex]
                        ).ToList();
            }

            ActualizoGrilla();
        }
        catch (System.Web.Services.Protocols.SoapException ex) { lblError.Text = ex.Detail.InnerText; }
        catch (Exception ex) { lblError.Text = ex.Message; }
    }

    protected void btnLimpiarFiltros_Click(object sender, EventArgs e)
    {
        lblError.Text = "";

        try
        {
            ddlSeccion.SelectedIndex = ddlFecha.SelectedIndex = 0;

            Session["NoticiasDesplegadas"] = new ServicioEntity().ListoNoticias().ToList();
            ActualizoGrilla();
        }
        catch (System.Web.Services.Protocols.SoapException ex) { lblError.Text = ex.Detail.InnerText; }
        catch (Exception ex) { lblError.Text = ex.Message; }
    }

    private void ActualizoGrilla()
    {
        try
        {
            GridView1.DataSource = Session["NoticiasDesplegadas"];
            GridView1.DataBind();
        }
        catch (System.Web.Services.Protocols.SoapException ex) { lblError.Text = ex.Detail.InnerText; }
        catch (Exception ex) { lblError.Text = ex.Message; }
    }

    protected void Grilla_SeleccionNoticia(object sender, EventArgs e)
    {
        Session["NoticiaElegida"] = ((List<NOTICIA>)Session["NoticiasDesplegadas"])[GridView1.SelectedIndex];
        Response.Redirect("~/ConsultaIndividualNoticia.aspx");
    }
}
