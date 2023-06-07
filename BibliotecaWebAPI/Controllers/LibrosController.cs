using BibliotecaModelos.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;
using RouteAttribute = System.Web.Http.RouteAttribute;

namespace BibliotecaWebAPI.Controllers
{
    public class LibrosController : ApiController
    {
        [HttpPost]
        [Route("api/LibrosController/ObtenerLibros")]
        public HttpResponseMessage ObtenerLibros([FromBody] Dictionary<string, string> filtrolibros)
        {
            HttpResponseMessage response = new HttpResponseMessage();


            try
            {
                //llamamos al metodos ObtenerLibros de la capa BL
                List<Libro> listalibros = BibliotecaBL.LibrosBL.ObtenerLibros(filtrolibros);

                //Devolvemos el diccionario con toda la información adicional, serializada.
                response.Content = new StringContent(JsonConvert.SerializeObject(listalibros), System.Text.Encoding.UTF8, "application/json");
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