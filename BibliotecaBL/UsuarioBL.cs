using BibliotecaModelos.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaBL
{
    public class UsuarioBL
    {
      

        public static Dictionary<string, string> ValidarLoginUsuario(Dictionary<string, string> infoAcceso)
        {
            Dictionary<string, string> infoLogin;
            String nombreUusario = infoAcceso["Usuario"];

            //Llamar a la Dal y obtener el resultado
            infoLogin = BibliotecaDAL.UsuariosDAL.ObtenerCliente(nombreUusario);

            //infoLogin = new Dictionary<string, string>(); //Esto esta para que compile y no de error pero esto No debe ser asi

            if (infoLogin.ContainsKey("Existe")){
                infoLogin.Add("Tiene Acceso", "false");
            }
            else
            {
                infoLogin.Add("Tiene Accesso", "True");
            }
            return infoLogin;
        }
    }
}
