using BibliotecaModelos.Entities;
using FrontalBiblioteca.Utilidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;

namespace FrontalBiblioteca.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //Response.Cookies.Add(new HttpCookie("c1", "a"));
            //Response.Cookies.Add(new HttpCookie("c2", "aa"));
            //Response.Cookies.Add(new HttpCookie("c3", "aaa"));
            //Response.Cookies["c2"].Expires = DateTime.Now.AddSeconds(5);
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

            //Diccionario para meter la informacion que nos viene al logearnos con el submit
            Dictionary<string, string> infoLogin = new Dictionary<string, string>();

            //creamos el diccionario idcliente para almacenar el iddelcliente
            Dictionary<string, string> idcliente = new Dictionary<string, string>();

            infoLogin.Add("Usuario", usuario);
                infoLogin.Add("Password", password);

            //Hacemos una instacia de una lista
            List<Libro> listalibros = new List<Libro>();

            //llamamos al metodo de conectorAPI
            var infoAcceso = ConectorAPI.ValidarLoginUsuario(infoLogin, out string msgErr);


                if (infoAcceso.ContainsValue("false"))
                     {
                        Response.Cookies.Add(new HttpCookie("errorlogin", "Usuario o contraseña incorrecto"));
                        return View("Validacion");
                     }
                else
                    {
                        //diccionario para almacenar todo lo enviado a traves del navegador
                        Dictionary<string, string> requestform = new Dictionary<string, string>();
                        foreach (var key in Request.Form.Keys)
                        {
                            string keyString = (string)key;
                            string value = Request.Form[keyString];
                            requestform.Add(keyString, value);
                        }
                        listalibros = ConectorAPI.ObtenerLibros(requestform, out string msgErrLibros);
                         
                        ViewData["Libros"] = listalibros;

                        //creamos una variable para recoger el valor que contiene la Key idcliente 
                        string idCliente = infoAcceso["idCliente"].ToString();
                        //lo añado al diccionario 
                        idcliente.Add("idCliente", idCliente);

                        //Creamos una nueva cookie y añadimos la Key idCliente con el valor que contiene la variable idCliente
                        Response.Cookies.Add(new HttpCookie("idCliente", idCliente));
                        return View("listadolibros");
                      //
                    }
 
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
