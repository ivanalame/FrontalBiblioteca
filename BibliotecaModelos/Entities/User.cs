using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaModelos.Entities
{
    public class Entities
    {
        public class User
        {
            public int Id { get; set; }
            public int usuario { get; set; }
            public int password { get; set; }

        }

        public class Libros
        {
            public int Id { get; set; }
            public int Titulo { get; set; }
            public int Editorial { get; set; }
            public bool reservado;

        }
    }
}
