using BibliotecaModelos.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaBL
{
    public class LibrosBL
    {
        public static List<Libro> ObtenerLibros(Dictionary<string, string> filtrolibros)
        {
            //aqui almaceno todo lo que me llega desde la DAL 
            List<Libro> listalibros = BibliotecaDAL.LibrosDAL.ObtenerLibros(filtrolibros); ;

            return listalibros;
        }

        public static Libro ObtenerLibroMedianteId(int idlibro)
        {
            //aqui almaceno todo lo que me llega desde la DAL 
            var DetalleDelLibro = BibliotecaDAL.LibrosDAL.ObtenerLibroMedianteId(idlibro); ;

            return DetalleDelLibro;
        }

        // Este método recibe un diccionario "filtros" y devuelve una lista de libros filtrada en función de los filtros recibidos.
        public static List<Libro> ObtenerLibrosFiltrados(Dictionary<string, string> filtros)
        {
            //Obtenemos los filtros de los valores correspondientes a las claves "Titulo", "Autor", "Editorial" y "Coleccion"
            string titulo = filtros["Titulo"];
            string autor = filtros["Autor"];
            string editorial = filtros["Editorial"];
            string coleccion = filtros["Coleccion"];

            //Llamamos al método "ObtenerLibrosfiltro" de la clase "LibrosDAL" de la BibliotecaDAL, pasando como argumentos los filtros obtenidos anteriormente. Asignamos el resultado a la lista "listalibrosfiltrados".
            List<Libro> listalibrosfiltrados = BibliotecaDAL.LibrosDAL.ObtenerLibrosfiltro(titulo, autor, editorial, coleccion);

            //Devolvemos la lista filtrada.
            return listalibrosfiltrados;
        }

    }

}
