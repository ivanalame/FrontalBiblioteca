using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaModelos.Entities
{
    public class Libro
    {
        public int idLibro { get; set; }
        public string ISBN { get; set; }
        public string Titulo { get; set; }
        public string Sinopsis { get; set; }
        public string Autor { get; set; }
        public string Editorial { get; set; }
        public string Coleccion { get; set; }
        public DateTime FechaPrimeraEdicion { get; set; }
      
    }
}
