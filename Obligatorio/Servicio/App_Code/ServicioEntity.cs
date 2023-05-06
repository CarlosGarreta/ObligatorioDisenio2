using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml;
using Modelo;

/// <summary>
/// Descripción breve de ServicioEntity
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
// [System.Web.Script.Services.ScriptService]
public class ServicioEntity : System.Web.Services.WebService
{
    //manejo de soapException
    private void GeneroSoapException(Exception ex)
    {
        XmlDocument _undoc = new System.Xml.XmlDocument();
        XmlNode _NodoError = _undoc.CreateNode(XmlNodeType.Element, SoapException.DetailElementName.Name, SoapException.DetailElementName.Namespace);

        XmlNode _NodoDetalle = _undoc.CreateNode(XmlNodeType.Element, "Error", "");
        _NodoDetalle.InnerText = ex.Message;

        _NodoError.AppendChild(_NodoDetalle);
        SoapException _MiEx = new SoapException(ex.Message, SoapException.ClientFaultCode, Context.Request.Url.AbsoluteUri, _NodoError);

        throw _MiEx;
    }

    //operaciones
    [WebMethod]
    public EMPLEADO Logueo(string usu, string pass)
    {
        EMPLEADO unU = null;

        try
        {
            return LogicaModelo.Logueo(usu, pass);
        }
        catch (Exception ex) { GeneroSoapException(ex); }

        return unU;
    }

    [WebMethod]
    public EMPLEADO BuscoEmpleado(string usu)
    {
        EMPLEADO unU = null;

        try
        {
            return LogicaModelo.BuscoEmpleado(usu);
        }
        catch (Exception ex) { GeneroSoapException(ex); }

        return unU;
    }

    [WebMethod]
    public void AltaEmpleado(EMPLEADO unE)
    {
        try
        {
            LogicaModelo.AltaEmpleado(unE);
        }
        catch (Exception ex) { GeneroSoapException(ex); }

    }

    [WebMethod]
    public SECCION BuscoSeccion(string pcod)
    {
        SECCION unaS = null;

        try
        {
            unaS = LogicaModelo.BuscoSeccion(pcod);
        }
        catch (Exception ex) { GeneroSoapException(ex); }

        return unaS;
    }

    [WebMethod]
    public void AltaSeccion(SECCION unaS)
    {
        try
        {
            LogicaModelo.AltaSeccion(unaS);
        }
        catch (Exception ex) { GeneroSoapException(ex); }
    }

    [WebMethod]
    public void ModificoSeccion(SECCION unaS)
    {
        try
        {
            LogicaModelo.ModificoSeccion(unaS);
        }
        catch (Exception ex) { GeneroSoapException(ex); }
    }

    [WebMethod]
    public void EliminoSeccion(SECCION unaS)
    {
        try
        {
            LogicaModelo.EliminoSeccion(unaS);
        }
        catch (Exception ex) { GeneroSoapException(ex); }
    }

    [WebMethod]
    public List<SECCION> ListoSecciones()
    {
        List<SECCION> _lista = null;

        try
        {
            _lista = LogicaModelo.ListoSecciones();
        }
        catch (Exception ex) { GeneroSoapException(ex); }

        return _lista;
    }
    
    [WebMethod]
    public PERIODISTA BuscoPeriodista(int pced)
    {
        PERIODISTA unP = null;

        try
        {
            unP = LogicaModelo.BuscoPeriodista(pced);
        }
        catch (Exception ex) { GeneroSoapException(ex); }

        return unP;
    }

    [WebMethod]
    public void AltaPeriodista(PERIODISTA unP)
    {
        try
        {
            LogicaModelo.AltaPeriodista(unP);
        }
        catch (Exception ex) { GeneroSoapException(ex); }
    }

    [WebMethod]
    public void ModificoPeriodista(PERIODISTA unP)
    {
        try
        {
            LogicaModelo.ModificoPeriodista(unP);
        }
        catch (Exception ex) { GeneroSoapException(ex); }
    }

    [WebMethod]
    public void EliminoPeriodista(PERIODISTA unP)
    {
        try
        {
            LogicaModelo.EliminoPeriodista(unP);
        }
        catch (Exception ex) { GeneroSoapException(ex); }
    }
    
    [WebMethod]
    public NOTICIA BuscoNoticia(string pcod)
    {
        NOTICIA unaN = null;

        try
        {
            unaN = LogicaModelo.BuscoNoticia(pcod);
        }
        catch (Exception ex) { GeneroSoapException(ex); }

        return unaN;
    }

    [WebMethod]
    public void AltaNoticia(NOTICIA unaN)
    {
        try
        {
            LogicaModelo.AltaNoticia(unaN);
        }
        catch (Exception ex) { GeneroSoapException(ex); }
    }

    [WebMethod]
    public void ModificoNoticia(NOTICIA unaN)
    {
        try
        {
            LogicaModelo.ModificoNoticia(unaN);
        }
        catch (Exception ex) { GeneroSoapException(ex); }
    }

    [WebMethod]
    public List<NOTICIA> ListoNoticias()
    {
        List<NOTICIA> _lista = null;

        try
        {
            _lista = LogicaModelo.ListoNoticias();
        }
        catch (Exception ex) { GeneroSoapException(ex); }

        return _lista;
    }

    [WebMethod]
    public List<NOTICIA> ListoNoticiasPorFecha(DateTime fecha)
    {
        List<NOTICIA> _lista = null;

        try
        {
            _lista = LogicaModelo.ListoNoticias(fecha);
        }
        catch (Exception ex) { GeneroSoapException(ex); }

        return _lista;
    }

    [WebMethod]
    public List<NOTICIA> ListoNoticiasPorSeccion(SECCION unaS)
    {
        List<NOTICIA> _lista = null;

        try
        {
            _lista = LogicaModelo.ListoNoticias(unaS);
        }
        catch (Exception ex) { GeneroSoapException(ex); }

        return _lista;
    }

    [WebMethod]
    public List<NOTICIA> ListoNoticiasPorSeccionyFecha(SECCION unaS, DateTime fecha)
    {
        List<NOTICIA> _lista = null;

        try
        {
            _lista = LogicaModelo.ListoNoticias(unaS, fecha);
        }
        catch (Exception ex) { GeneroSoapException(ex); }

        return _lista;
    }
}