using System;
using ObligatorioServicio;

public partial class ControlConsultaNoticia : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            NOTICIA n = (NOTICIA)Session["NoticiaElegida"];

            lbTitulo.Text = n.Titulo;
            lbSeccion.Text = "Seccion: "+ n.SECCION.Nombre;
            lbCuerpo.Text = n.Cuerpo;
            lbFecha.Text = n.Fecha.ToShortDateString();
            lbEmp.Text = n.EMPLEADO.Usuario;
            lblImportancia.Text = n.Importancia.ToString();
            lbperiodistas.DataSource = n.PERIODISTA;
            lbperiodistas.DataBind();
        }
        catch (System.Web.Services.Protocols.SoapException ex) { lblError.Text = ex.Detail.InnerText; }
        catch (Exception ex) { lblError.Text = ex.Message; }
    }
}