using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FrontalBiblioteca.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View("Validacion");
        }

        public ActionResult ValidarUsuario(string txtNombre, string txtPassword)
        {
            string connectionString = "Cadena de conexion";
            string consulta = "SELECT * FROM tabla_usuarios WHERE nombre_usuario =" + txtNombre + " AND contrasena = " + txtPassword + "";
            bool usuarioExiste = false;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(consulta, connection);
                command.Parameters.AddWithValue("@User", txtNombre);
                command.Parameters.AddWithValue("@Password", txtPassword);


                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    usuarioExiste = true;
                }

                reader.Close();
                connection.Close();
            }


            if (usuarioExiste)
            {
                ViewBag.Mensaje = "Bienvenido" + txtNombre;
                return View("Gestion");
            }
            else
            {
                return View("Validacion");
                Response.Cookies.Add(new HttpCookie("Validacion erronea", "Usuario o contraseña incorrecto"));
            }
            
        }

        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return View();
        //}

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}
    }
}