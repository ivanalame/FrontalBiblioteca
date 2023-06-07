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
       
    }
    
}
