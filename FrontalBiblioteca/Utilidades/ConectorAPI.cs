using BibliotecaModelos.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Web;
using System.Web.Configuration;
using static BibliotecaModelos.Entities.Persona;

namespace FrontalBiblioteca.Utilidades
{
    public class ConectorAPI
    {
        /// <summary>
        /// Mantiene en memoria la url de acceso a la API, sacada desde el web.config
        /// </summary>
        static string baseUrlAPI;

        /// <summary>
        /// Constructor estático para inicializar lo necesario
        /// </summary>
        static ConectorAPI()
        {
            try
            {
                var request = new HttpRequestMessage();

                string url = WebConfigurationManager.AppSettings["pathBaseWebApiSql"].ToString();

                baseUrlAPI = url;

                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(baseUrlAPI);


                //Enviar solicitud
                //   HttpResponseMessage response = await httpClient.SendAsync(request);

            }
            catch (Exception ex)
            {
                throw new Exception("Error en carga de la ruta de acceso de la API", ex);
            }

            if (!baseUrlAPI.EndsWith("/"))
                baseUrlAPI += "/";
        }

        #region Métodos internos

        /// <summary>
        /// Método interno común para todas las llamadas de tipo GET.
        /// </summary>
        /// <param name="uri">Cadena con la uri relativa de llamada a la API</param>
        /// <param name="segundosTimeout">Establece los segundos de tiempo de espera de la llamada. Opcional. Por defecto, 100</param>
        /// <returns></returns>

        static HttpResponseMessage RespuestaGET(string uri, int segundosTimeout = -1)
        {
            string url = baseUrlAPI + uri;
            using (HttpClient clienteAPI = new HttpClient())
            {
                try
                {
                    if (segundosTimeout != -1)
                        clienteAPI.Timeout = TimeSpan.FromSeconds(segundosTimeout); //Por si hiciera falta reducirlo o aumentarlo, que por defecto es 100 segundos.
                    HttpResponseMessage response = clienteAPI.GetAsync(url).Result;
                    return response;
                }
                catch (Exception ex)
                {
                    string msgError = "Excepción de tipo " + ex.GetType().Name;

                    if (ex is AggregateException && ((AggregateException)ex).InnerExceptions.Count > 1)
                    {
                        foreach (var exc in ((AggregateException)ex).InnerExceptions)
                        {
                            msgError += "\n - " + exc.Message;
                            if (exc.InnerException != null)
                            {
                                msgError += " (" + exc.InnerException.Message;
                                if (exc.InnerException.InnerException != null)
                                {
                                    msgError += " -> " + exc.InnerException.InnerException.Message;
                                }
                                msgError += ")";
                            }
                        }
                    }
                    else
                    {
                        if (!(ex is AggregateException))
                        {
                            msgError += " - " + ex.Message;

                            if (ex.InnerException != null)
                            {
                                msgError += " (" + ex.InnerException.Message;
                                if (ex.InnerException.InnerException != null)
                                {
                                    msgError += " -> " + ex.InnerException.InnerException.Message;
                                }
                                msgError += ")";
                            }
                        }
                        else
                        {
                            if (ex.InnerException != null)
                            {
                                msgError += " - " + ex.InnerException.Message;
                                if (ex.InnerException.InnerException != null)
                                {
                                    msgError += " -> " + ex.InnerException.InnerException.Message;
                                }
                            }
                        }
                    }

                    //LogErrores.EscribirError("ConectorAPI/RespuestaGET", ex, msgError + " en llamada a " + url);
                    throw;
                }
            }
        }

        /// <summary>
        /// Método interno común para todas las llamadas de tipo POST.
        /// </summary>
        /// <param name="uri">Cadena con la uri relativa de llamada a la API</param>
        /// <param name="o">Objeto a pasar para su alta</param>
        /// <param name="segundosTimeout">Establece los segundos de tiempo de espera de la llamada. Opcional. Por defecto, 100</param>
        /// <returns></returns>

        static HttpResponseMessage RespuestaPOST(string uri, object o, int segundosTimeout = -1)
        {
            //Hay que chequear limitaciones de tamaño en el objeto.
            //De momento probado hasta 2Mb y funciona, pero con archivos más grandes no... 
            string url = baseUrlAPI + uri;
            using (HttpClient clienteAPI = new HttpClient())
            {
                try
                {
                    if (segundosTimeout != -1)
                        clienteAPI.Timeout = TimeSpan.FromSeconds(segundosTimeout); //Por si hiciera falta reducirlo o aumentarlo, que por defecto es 100 segundos.
                    HttpResponseMessage response = clienteAPI.PostAsJsonAsync(url, o).Result;
                    return response;
                }
                catch (Exception ex)
                {
                    string msgError = "Excepción de tipo " + ex.GetType().Name;

                    if (ex is AggregateException && ((AggregateException)ex).InnerExceptions.Count > 1)
                    {
                        foreach (var exc in ((AggregateException)ex).InnerExceptions)
                        {
                            msgError += "\n - " + exc.Message;
                            if (exc.InnerException != null)
                            {
                                msgError += " (" + exc.InnerException.Message;
                                if (exc.InnerException.InnerException != null)
                                {
                                    msgError += " -> " + exc.InnerException.InnerException.Message;
                                }
                                msgError += ")";
                            }
                        }
                    }
                    else
                    {
                        if (!(ex is AggregateException))
                        {
                            msgError += " - " + ex.Message;

                            if (ex.InnerException != null)
                            {
                                msgError += " (" + ex.InnerException.Message;
                                if (ex.InnerException.InnerException != null)
                                {
                                    msgError += " -> " + ex.InnerException.InnerException.Message;
                                }
                                msgError += ")";
                            }
                        }
                        else
                        {
                            if (ex.InnerException != null)
                            {
                                msgError += " - " + ex.InnerException.Message;
                                if (ex.InnerException.InnerException != null)
                                {
                                    msgError += " -> " + ex.InnerException.InnerException.Message;
                                }
                            }
                        }
                    }

                    //LogErrores.EscribirError("ConectorAPI/RespuestaPOST", ex, msgError + " en llamada a " + url);
                    throw;
                }
            }
        }

        //public HttpResponseMessage ObtenerDetalles([FromBody],String uri, int idLibro)
        //{
        //    RespuestaPOST(uri, idLibro);
        //}
        #endregion

        #region Métodos públicos

        //He dejado un par de ejemplos, uno de GET y otro de POST
        //Lo que se devuelva en cada función lógicamente suele depender de lo que se devuelva desde la API.
        //Eso sí... lo que tiene que coincidir es lo que recibas de la llamada (el contenido del ReadAsAsync) con lo que devuelvas desde la API.
        public static Dictionary<string, object> ValidarLoginUsuario(Dictionary<string, string> infoLogin, out string msgErr)
        {
            Dictionary<string, object> infoAcceso = new Dictionary<string, object>();
            //Dictionary<string, object> infoAcceso = null;
            msgErr = null;

            string uri = "api/UsuariosController/ValidarLoginUsuario";
            HttpResponseMessage response = RespuestaPOST(uri, infoLogin);
            if (response.IsSuccessStatusCode)
            {
                infoAcceso.Add("TieneAcceso", true);
                infoAcceso.Add("FechaUltimaConexion", DateTime.Now);

                infoAcceso = response.Content.ReadAsAsync<Dictionary<string, object>>().Result;
                if (infoAcceso == null)
                {
                    msgErr = "INFOACCESO vacío. Error en llamada a " + uri + " - Motivo: " + response.ReasonPhrase;
                }
            }
            else
            {
                msgErr = "Error en llamada a " + uri + " - Motivo: " + response.ReasonPhrase;
            }
            return infoAcceso;
        }


        public static List<Persona> VerUsuarios(out string msgErr)
        {
            msgErr = null;
            List<Persona> usuarios = null;

            string uri = "api/Usuario/VerUsuarios";
            HttpResponseMessage response = RespuestaGET(uri);

            if (response.IsSuccessStatusCode)
            {
                usuarios = response.Content.ReadAsAsync<List<Persona>>().Result;
                if (usuarios == null)
                {
                    msgErr = "Error en llamada a " + uri + " - Motivo: " + response.ReasonPhrase;
                }
            }
            else
            {
                msgErr = "Error en llamada a " + uri + " - Motivo: " + response.ReasonPhrase;
            }
            return usuarios;
        }

        public static List<Libro> ObtenerLibros(Dictionary<string, string> filtrolibros, out string msgErr)
        {
            List<Libro> listalibros = new List<Libro>();
            msgErr = null;
            string uri = "api/LibrosController/ObtenerLibros";
            HttpResponseMessage response = RespuestaPOST(uri, filtrolibros);

            if (response.IsSuccessStatusCode)
            {

                listalibros = response.Content.ReadAsAsync<List<Libro>>().Result;
                if (listalibros == null)
                {
                    msgErr = "Diccionario vacío. Error en llamada a " + uri + " - Motivo: " + response.ReasonPhrase;
                }


            }
            else
            {
                msgErr = "Error en llamada a " + uri + " - Motivo: " + response.ReasonPhrase;
            }
            return listalibros;
        }



        public static Libro ObtenerLibroMedianteId(int idlibro, out string msgErr)
        {
            Libro DetalleDelLibro = new Libro();
            msgErr = null;
            string uri = "api/LibrosController/ObtenerLibroMedianteId";
            HttpResponseMessage response = RespuestaPOST(uri, idlibro);

            if (response.IsSuccessStatusCode)
            {

                DetalleDelLibro = response.Content.ReadAsAsync<Libro>().Result;
                if (DetalleDelLibro == null)
                {
                    msgErr = "Diccionario vacío. Error en llamada a " + uri + " - Motivo: " + response.ReasonPhrase;
                }


            }
            else
            {
                msgErr = "Error en llamada a " + uri + " - Motivo: " + response.ReasonPhrase;
            }
            return DetalleDelLibro;
        }

        //Este método recibe un diccionario "filtros" y una variable de referencia "msgErr",
        //y devuelve una lista de libros "listalibros" que cumpla con los filtros proporcionados.

        public static List<Libro> ObtenerLibrosFiltrados(Dictionary<string, string> filtros, out string msgErr)
        {
            // Creamos una instancia de una lista de libros llamada "listalibros" y establecemos la variable "msgErr" en null.
            List<Libro> listalibros = new List<Libro>();
            msgErr = null;

            // Definimos una cadena "uri" que contiene la ruta al endpoint de la API que se encarga de obtener los libros filtrados.
            string uri = "api/LibrosController/ObtenerLibrosFiltrados";

            // Llamamos al método "RespuestaPOST" pasando como argumentos la cadena de ruta "uri" y el diccionario "filtros". Guardamos la respuesta en una variable "response" de tipo HttpResponseMessage.
            HttpResponseMessage response = RespuestaPOST(uri, filtros);

            // Si la respuesta se obtiene correctamente, convertimos el contenido de la respuesta a una lista de libros utilizando el método .ReadAsAsync<List<Libro>>(),
            // lo cual nos devuelve una tarea. Luego, obtenemos el resultado de la tarea mediante el método .Result y
            // asignamos la lista resultante a "listalibros". Si "listalibros" está vacía, establecemos la variable de referencia "msgErr"
            // para almacenar un mensaje de error.
            if (response.IsSuccessStatusCode)
            {
                listalibros = response.Content.ReadAsAsync<List<Libro>>().Result;
                if (listalibros == null)
                {
                    msgErr = "Diccionario vacío. Error en llamada a " + uri + " - Motivo: " + response.ReasonPhrase;
                }
            }
            // Si la respuesta no se obtiene correctamente, establecemos la variable de referencia "msgErr" con un mensaje de error que incluye la ruta de la API y el mensaje de error proporcionado por la respuesta.
            else
            {
                msgErr = "Error en llamada a " + uri + " - Motivo: " + response.ReasonPhrase;
            }

            // Devolvemos la lista resultante "listalibros".
            return listalibros;

            throw new NotImplementedException();
        }
        #endregion
    }
}