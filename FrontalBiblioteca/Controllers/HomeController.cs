using FrontalBiblioteca.Utilidades;
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

        public ActionResult ValidarUsuario(string usuario, string password)
        {
            //string connectionString = "Cadena de conexion";
            //string consulta = "SELECT * FROM tabla_usuarios WHERE nombre_usuario =" + txtUsuario + " AND contrasena = " + txtPassword + "";
            //bool usuarioExiste = false;

            //using (SqlConnection connection = new SqlConnection(connectionString))
            //{
            //    SqlCommand command = new SqlCommand(consulta, connection);
            //    command.Parameters.AddWithValue("@user", txtUsuario);
            //    command.Parameters.AddWithValue("@password", txtPassword);

            //    connection.Open();
            //    SqlDataReader reader = command.ExecuteReader();

            //    if (reader.HasRows)
            //    {
            //        usuarioExiste = true;
            //    }

            //    reader.Close();
            //    connection.Close();
            //}


            //Borrar 

            // string usuario = Request.Form["usuario"];
            // string contraseña = Request.Form["password"];
           
                ViewBag.Mensaje = "Bienvenido" + usuario; 

                Dictionary<string, string> infoLogin = new Dictionary<string, string>();
                 infoLogin.Add("Usuario", usuario);
                infoLogin.Add("Password", password);

                 //llamamos al metodo de conectorAPI
                  var infoAcceso = ConectorAPI.ValidarLoginUsuario(infoLogin, out string msgErr);
              
                return View("Gestion");

     

                //Response.Cookies.Add(new HttpCookie("Validacion erronea", "Usuario o contraseña incorrecto"));
                //return View("Validacion");
               

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