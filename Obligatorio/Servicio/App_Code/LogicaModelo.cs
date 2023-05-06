using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using Modelo;

public class LogicaModelo
{
    private static ObligatorioEntities _contexto = null;

    public static ObligatorioEntities Contexto
    {
        get
        {
            if (_contexto == null) _contexto = new ObligatorioEntities();
            return _contexto;
        }
    }

    //empleados
    public static EMPLEADO Logueo(string puser, string ppass)
    {
        EMPLEADO unE = Contexto.EMPLEADO.Where(e => e.Usuario == puser && e.Contrasenia == ppass).FirstOrDefault();

        if (unE == null) throw new Exception("Usuario/contraseña incorrectos");
        else return unE;
    }

    public static EMPLEADO BuscoEmpleado(string puser)
    {
        return Contexto.EMPLEADO.Where(e => e.Usuario == puser).FirstOrDefault();
    }

    public static void AltaEmpleado(EMPLEADO unE)
    {
        try
        {
            Validaciones.validarEmpleado(unE);
        }
        catch (Exception ex) { throw ex; }

        try
        {
            Contexto.EMPLEADO.Add(unE);
            Contexto.SaveChanges();
        }
        catch (Exception ex)
        {
            Contexto.Entry(unE).State = System.Data.Entity.EntityState.Detached;
            throw ex;
        }
    }

    //seccion
    public static SECCION BuscoSeccion(string pcod)
    {
        return Contexto.SECCION.Where(s => s.CodigoSeccion == pcod).FirstOrDefault();
    }

    public static void AltaSeccion(SECCION unaS)
    {
        try
        {
            Validaciones.validarSeccion(unaS);
        }
        catch (Exception ex) { throw ex; }

        try
        {
            Contexto.SECCION.Add(unaS);
            Contexto.SaveChanges();
        }
        catch (Exception ex)
        {
            Contexto.Entry(unaS).State = System.Data.Entity.EntityState.Detached;
            throw ex;
        }
    }

    public static void ModificoSeccion(SECCION unaS)
    {
        try
        {
            SECCION S = Contexto.SECCION.Where(s => s.CodigoSeccion == unaS.CodigoSeccion).FirstOrDefault();

            S.CodigoSeccion = unaS.CodigoSeccion;
            S.Nombre = unaS.Nombre;

            Validaciones.validarSeccion(unaS);

            Contexto.SaveChanges();
        }
        catch (Exception ex) { throw ex; }
    }

    public static void EliminoSeccion(SECCION unaS)
    {
        try
        {
            SqlParameter _code = new SqlParameter("@code", unaS.CodigoSeccion);
            SqlParameter _retorno = new SqlParameter("@ret", System.Data.SqlDbType.Int);
            _retorno.Direction = System.Data.ParameterDirection.Output;

            Contexto.Database.ExecuteSqlCommand("exec EliminoSeccion @code, @ret output", _code, _retorno);

            if ((int)_retorno.Value == -1) throw new Exception("No existe la seccion");
            else if ((int)_retorno.Value == -2) throw new Exception("La seccion tiene noticias asignadas");
            else if ((int)_retorno.Value == -3) throw new Exception("Error en eliminar seccion");

            Contexto.SaveChanges();
        }
        catch (Exception ex) { throw ex; }
    }

    public static List<SECCION> ListoSecciones()
    {
        return Contexto.SECCION.ToList();
    }

    //periodista
    public static PERIODISTA BuscoPeriodista(int pced)
    {
        return Contexto.PERIODISTA.Where(p => p.Cedula == pced).FirstOrDefault();
    }

    public static void AltaPeriodista(PERIODISTA unP)
    {
        try
        {
            Validaciones.validarPeriodista(unP);
        }
        catch (Exception ex) { throw ex; }

        try
        {
            Contexto.PERIODISTA.Add(unP);
            Contexto.SaveChanges();
        }
        catch (Exception ex)
        {
            Contexto.Entry(unP).State = System.Data.Entity.EntityState.Detached;
            throw ex;
        }
    }

    public static void ModificoPeriodista(PERIODISTA unP)
    {
        try
        {
            PERIODISTA P = Contexto.PERIODISTA.Where(p => p.Cedula == unP.Cedula).FirstOrDefault();

            P.Cedula = unP.Cedula;
            P.Nombre = unP.Nombre;
            P.Mail = unP.Mail;

            Validaciones.validarPeriodista(unP);

            Contexto.SaveChanges();
        }
        catch (Exception ex) { throw ex; }
    }

    public static void EliminoPeriodista(PERIODISTA unP)
    {
        try
        {
            SqlParameter _ced = new SqlParameter("@ced", unP.Cedula);
            SqlParameter _retorno = new SqlParameter("@ret", System.Data.SqlDbType.Int);
            _retorno.Direction = System.Data.ParameterDirection.Output;

            Contexto.Database.ExecuteSqlCommand("exec EliminoPeriodista @ced, @ret output", _ced, _retorno);

            if ((int)_retorno.Value == -1) throw new Exception("No existe el perioidsta");
            else if ((int)_retorno.Value == -2) throw new Exception("El periodista tiene noticias asignadas");
            else if ((int)_retorno.Value == -3) throw new Exception("Error en eliminar periodista");

            Contexto.SaveChanges();
        }
        catch (Exception ex) { throw ex; }
    }

    //noticia 
    public static NOTICIA BuscoNoticia(string pcod)
    {
        return Contexto.NOTICIA.Where(n => n.CodigoNoticia == pcod).FirstOrDefault();
    }

    public static void AltaNoticia(NOTICIA unaN)
    {
        try
        {
            EMPLEADO _emp = Contexto.EMPLEADO.Where(e => e.Usuario == unaN.EMPLEADO.Usuario).FirstOrDefault();
            SECCION _sec = Contexto.SECCION.Where(s => s.CodigoSeccion == unaN.SECCION.CodigoSeccion).FirstOrDefault();

            HashSet<PERIODISTA> _lista = new HashSet<PERIODISTA>();

            for (int i = 0; i < unaN.PERIODISTA.Count; i++)
            {
                int cedula = unaN.PERIODISTA.ElementAt(i).Cedula;
                _lista.Add(Contexto.PERIODISTA.Where(p => p.Cedula == cedula).FirstOrDefault());
            }


            unaN.SECCION = _sec;
            unaN.EMPLEADO = _emp;
            unaN.PERIODISTA = _lista;

            Validaciones.validarNoticia(unaN);
        }
        catch (Exception ex) { throw ex; }

        try
        {
            Contexto.NOTICIA.Add(unaN);
            Contexto.SaveChanges();
        }
        catch (Exception ex)
        {
            Contexto.Entry(unaN).State = System.Data.Entity.EntityState.Detached;
            throw ex;
        }
    }

    public static void ModificoNoticia(NOTICIA unaN)
    {
        try
        {
            NOTICIA N = Contexto.NOTICIA.Where(n => n.CodigoNoticia == unaN.CodigoNoticia).FirstOrDefault();

            N.Titulo = unaN.Titulo;
            N.Cuerpo = unaN.Cuerpo;
            N.Fecha = unaN.Fecha;
            N.Importancia = unaN.Importancia;


            N.SECCION = Contexto.SECCION.Where(s => s.CodigoSeccion == unaN.SECCION.CodigoSeccion).FirstOrDefault();

            N.EMPLEADO = Contexto.EMPLEADO.Where(e => e.Usuario == unaN.EMPLEADO.Usuario).FirstOrDefault();


            HashSet<PERIODISTA> _lista = new HashSet<PERIODISTA>();

            for (int i = 0; i < unaN.PERIODISTA.Count; i++)
            {
                int cedula = unaN.PERIODISTA.ElementAt(i).Cedula;
                _lista.Add(Contexto.PERIODISTA.Where(p => p.Cedula == cedula).FirstOrDefault());
            }

            unaN.PERIODISTA = _lista;

            

            Validaciones.validarNoticia(unaN);

            Contexto.SaveChanges();
        }
        catch (Exception ex) { throw ex; }
    }
    

    public static List<NOTICIA> ListoNoticias()
    {
        DateTime fecha2 = DateTime.Today.AddDays(-5);
        return (from unaN in Contexto.NOTICIA
                where unaN.Fecha < DateTime.Today && unaN.Fecha > fecha2
                select unaN).ToList();
    }

    public static List<NOTICIA> ListoNoticias(DateTime fecha)
    {
        return (from unaN in Contexto.NOTICIA
                where unaN.Fecha == fecha
                select unaN).ToList();
    }

    public static List<NOTICIA> ListoNoticias(SECCION unaS)
    {
        DateTime fecha2 = DateTime.Today.AddDays(-5);
        return (from unaN in Contexto.NOTICIA
                where unaN.Fecha < DateTime.Today && unaN.Fecha > fecha2
                    && unaN.CodigoSeccion == unaS.CodigoSeccion
                select unaN).ToList();
    }

    public static List<NOTICIA> ListoNoticias(SECCION unaS, DateTime fecha)
    {
        return (from unaN in Contexto.NOTICIA
                where unaN.CodigoSeccion == unaS.CodigoSeccion
                    && unaN.Fecha == fecha
                select unaN).ToList();
    }
}