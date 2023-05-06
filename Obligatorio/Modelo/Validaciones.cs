using System;
using System.Text.RegularExpressions;

namespace Modelo
{
    public class Validaciones
    {
        public static void validarNoticia(NOTICIA n)
        {
            if (!Regex.IsMatch(n.CodigoNoticia, @"^\w{4}$")) throw new Exception("El formato del codigo de noticia deben ser 4 caracteres alfanumericos");

            if (n.Titulo == null || n.Titulo.Length == 0) throw new Exception("La noticia debe tener un titulo");
            else if (n.Titulo.Length > 50) throw new Exception("El titulo de la noticia no puede superar los 50 caracteres");

            if (n.Fecha < DateTime.Today) throw new Exception("La fecha de la noticia no puede ser anterior a la actual");

            if (n.Importancia < 1 || n.Importancia > 5) throw new Exception("La importancia de la noticia debe ser un entero de 1 a 5");
            
            if (n.Cuerpo == null || n.Cuerpo.Length == 0) throw new Exception("La noticia debe tener un cuerpo");
            else if (n.Cuerpo.Length > 200) throw new Exception("El cuerpo de la noticia no puede superar los 200 caracteres");

            if (n.EMPLEADO == null) throw new Exception("El ultimo empleado que edito la noticia no puede ser vacio");

            if (n.PERIODISTA == null || n.PERIODISTA.Count == 0) throw new Exception("La lista de periodistas no puede estar vacia");

            if (n.SECCION == null) throw new Exception("La seccion de la noticia no puede ser vacia");
        }

        public static void validarSeccion(SECCION s)
        {
            if (!Regex.IsMatch(s.CodigoSeccion, "^[a-zA-Z]{5}$")) throw new Exception("El codigo de la seccion debe ser de 5 letras exactas");

            if (s.Nombre == null || s.Nombre.Length == 0) throw new Exception("La seccion debe tener un nombre");
            else if (s.Nombre.Length > 15) throw new Exception("El nombre de la seccion no puede exceder los 15 caracteres");
        }

        public static void validarPeriodista(PERIODISTA p)
        {
            if (p.Cedula <= 0 || p.Cedula >= 10000000) throw new Exception("El numero de la cedula no puede exceder las 7 cifras");

            if (p.Nombre == null || p.Nombre.Length == 0) throw new Exception("El periodista debe tener un nombre");
            else if (p.Nombre.Length > 20) new Exception("El nombre del periodista no puede exceder los 20 caracteres");

            if (!(Regex.IsMatch(p.Mail, @"^\w+@\w+\.com$"))) throw new Exception("El formato del mail no es correcto: **@**.com");
            else if (p.Mail.Length > 50) throw new Exception("El largo del mail no puede exceder los 50 caracteres");
        }

        public static void validarEmpleado(EMPLEADO e)
        {
            if (!Regex.IsMatch(e.Usuario, "^[a-zA-Z]{10}$")) throw new Exception("El nombre del empleado debe tener 10 letras exactas");

            if (!Regex.IsMatch(e.Contrasenia, "^[a-zA-Z]{4}[0-9]{3}$")) throw new Exception("La contraseña debe tener cuatro letras y tres numeros, en ese orden");
        }
    }
}