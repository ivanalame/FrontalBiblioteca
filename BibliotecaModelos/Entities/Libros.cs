using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaModelos.Entities
{
    public class Libros
    {
        public int Id { get; set; }
        public int Titulo { get; set; }
        public int Editorial { get; set; }
        public bool reservado { get; set; }
        public bool Comprado { get; set; }
    }
}
