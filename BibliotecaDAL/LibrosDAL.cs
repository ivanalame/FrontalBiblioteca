using BibliotecaModelos.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaDAL
{
    public class LibrosDAL
    {
        public static Libro ObtenerLibroMedianteId(int idlibro)
        {
            Libro detallelibro = new Libro();

            using (SqlConnection con = new SqlConnection(UtilDAL.CadenaConexion))
            {
                SqlCommand command = new SqlCommand
                {
                    Connection = con,
                    //Metemos la query para sacar la informacion que queramos
                    CommandText = "SELECT * FROM Libros WHERE idLibro = @idlibro"
                };

                command.Parameters.AddWithValue("@idlibro", idlibro);

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {

                    detallelibro.idLibro = Convert.ToInt32(reader["idLibro"]);
                    detallelibro.ISBN = reader["ISBN"].ToString();
                    detallelibro.Titulo = reader["Titulo"].ToString();
                    if (reader["Sinopsis"] != DBNull.Value)
                        detallelibro.Sinopsis = reader["Sinopsis"].ToString();
                    if (reader["Autor"] != DBNull.Value)
                        detallelibro.Autor = reader["Autor"].ToString();
                    if (reader["Editorial"] != DBNull.Value)
                        detallelibro.Editorial = reader["Editorial"].ToString();
                    if (reader["Coleccion"] != DBNull.Value)
                        detallelibro.Coleccion = reader["Coleccion"].ToString();
                    if (reader["FechaPrimeraEdicion"] != DBNull.Value)
                        detallelibro.FechaPrimeraEdicion = Convert.ToDateTime(reader["FechaPrimeraEdicion"]);
                }
            }
            return detallelibro;
        }

        public static List<Libro> ObtenerLibros(Dictionary<string, string> filtrolibros)
        {
            var listalibros = new List<Libro>();

            using (SqlConnection con = new SqlConnection(UtilDAL.CadenaConexion))
            {
                SqlCommand command = new SqlCommand
                {
                    Connection = con,
                    //Metemos la query para sacar la informacion que queramos
                    CommandText = "SELECT * FROM Libros"
                };

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var libro = new Libro();

                    libro.idLibro = Convert.ToInt32(reader["idLibro"]);
                    libro.ISBN = reader["ISBN"].ToString();
                    libro.Titulo = reader["Titulo"].ToString();
                    if (reader["Sinopsis"] != DBNull.Value)
                        libro.Sinopsis = reader["Sinopsis"].ToString();
                    if (reader["Autor"] != DBNull.Value)
                        libro.Autor = reader["Autor"].ToString();
                    if (reader["Editorial"] != DBNull.Value)
                        libro.Editorial = reader["Editorial"].ToString();
                    if (reader["Coleccion"] != DBNull.Value)
                        libro.Coleccion = reader["Coleccion"].ToString();
                    if (reader["FechaPrimeraEdicion"] != DBNull.Value)
                        libro.FechaPrimeraEdicion = Convert.ToDateTime(reader["FechaPrimeraEdicion"]);


                    listalibros.Add(libro);
                }
            }

            return listalibros;
        }

        public static List<Libro> ObtenerLibrosfiltro(string titulo, string autor, string editorial, string coleccion)
        {
            // Inicializamos una nueva lista "listalibrosfiltrados"
            var listalibrosfiltrados = new List<Libro>();

            // Utilizamos la clase SqlConnection para establecer una conexión a la base de datos, utilizando la cadena de conexión definida en la clase UtilDAL.
            using (SqlConnection con = new SqlConnection(UtilDAL.CadenaConexion))
            {
                SqlCommand command = new SqlCommand
                {
                    Connection = con,
                    //Metemos la query para sacar la informacion que queramos
                    CommandText = "SELECT * FROM Libros WHERE Titulo = @titulo AND Autor = @autor AND Editorial = @editorial AND Coleccion = @coleccion"
                };

                command.Parameters.AddWithValue("@titulo", titulo);
                command.Parameters.AddWithValue("@autor", autor);
                command.Parameters.AddWithValue("@editorial", editorial);
                command.Parameters.AddWithValue("@coleccion", coleccion);

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var libro = new Libro();

                    libro.idLibro = Convert.ToInt32(reader["idLibro"]);
                    libro.ISBN = reader["ISBN"].ToString();
                    libro.Titulo = reader["Titulo"].ToString();
                    if (reader["Sinopsis"] != DBNull.Value)
                        libro.Sinopsis = reader["Sinopsis"].ToString();
                    if (reader["Autor"] != DBNull.Value)
                        libro.Autor = reader["Autor"].ToString();
                    if (reader["Editorial"] != DBNull.Value)
                        libro.Editorial = reader["Editorial"].ToString();
                    if (reader["Coleccion"] != DBNull.Value)
                        libro.Coleccion = reader["Coleccion"].ToString();
                    if (reader["FechaPrimeraEdicion"] != DBNull.Value)
                        libro.FechaPrimeraEdicion = Convert.ToDateTime(reader["FechaPrimeraEdicion"]);



                    listalibrosfiltrados.Add(libro);
                }
            }

            return listalibrosfiltrados;
        }
    }

}
