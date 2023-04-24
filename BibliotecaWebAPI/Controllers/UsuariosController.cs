using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace BibliotecaWebAPI.Controllers
{
    public class UsuariosController : ApiController
    {
        [HttpPost]
        [Route("api/UsuariosController/ValidarLoginUsuario")]
        public HttpResponseMessage ValidarLoginUsuario([FromBody] Dictionary<string, object> infoAcceso)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            //Estas comprobaciones no serían necesarias ya que se supone que ambos datos son obligatorios, pero me gusta tener todo controlado.
            if (infoAcceso == null)
            {
                response.StatusCode = HttpStatusCode.Conflict;
                response.ReasonPhrase = "No se pasó información para verificar el acceso";
                return response;
            }
            else if (!infoAcceso.ContainsKey("Usuario"))
            {
                response.StatusCode = HttpStatusCode.Conflict;
                response.ReasonPhrase = "No se pasó información de usuario para verificar el acceso.";
                return response;
            }
            else if (!infoAcceso.ContainsKey("Password"))
            {
                response.StatusCode = HttpStatusCode.Conflict;
                response.ReasonPhrase = "No se pasó contraseña para verificar.";
                return response;
            }

            try
            {
                //Aquí escalaremos hasta la DAL. Si el usuario tiene acceso, se nos devolverá el resto de información necesaria.

                #region Validación
                /* 
                 * Este bloque debería hacerse de la siguiente manera:
                 *   1.- Se escala a la BL
                 *   2.- Se escala a la DAL
                 *   3.- Se llama a SQL y se obtiene la información.
                 *   4.- Se devuelve a la BL, donde se valida el usuario
                 *   5.- Se genera el diccionario de retorno con todos los valores a devolver con la información de la validación.
                 *   6.- Se devuelve a este método
                 *   7.- Se realiza la devolución del Response al ConectorAPI
                 */

                // Dictionary<string, object> infoAux = BibliotecaBL.UsuarioBL.ValidarLoginUsuario(infoAcceso);
                Dictionary<string, object> infoAux = new Dictionary<string, object>();
                infoAux.Add("Tiene Accesso", true);
                infoAux.Add("FechaUltimaConexion", DateTime.Now);
                #endregion Validación

                //Con lo que se nos devuelva desde la DAL -> BL, enviamos la respuesta.

                //Devolvemos el diccionario con toda la información adicional, serializada.
                 response.Content = new StringContent(JsonConvert.SerializeObject(infoAux), System.Text.Encoding.UTF8, "application/json");
                return response;
            }
            catch (Exception ex)
            {
                response.StatusCode = HttpStatusCode.Conflict;
                response.ReasonPhrase = ex.Message;
                return response;
            }
        }
    }
}