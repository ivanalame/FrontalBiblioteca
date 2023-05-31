using BibliotecaModelos.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaDAL
{
    public class UsuariosDAL
    {
        public static Dictionary<string, string> ObtenerCliente(string usuario)
        {
            Dictionary<string, string> diccRetorno = new Dictionary<string, string>();

            using (SqlConnection con = new SqlConnection(UtilDAL.CadenaConexion))
            {
                SqlCommand command = new SqlCommand
                {
                    Connection = con,
                    //Metemos la query para sacar la informacion que queramos
                    CommandText = "SELECT * FROM Clientes WHERE NombreUsuario = @usuario"
                };

                command.Parameters.AddWithValue("@usuario", usuario);

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read()) //Podría usar un while, pero asumimos que sólo habrá uno con ese nombre de usuario
                {
                    diccRetorno.Add("Password", reader["Password"].ToString());

                    if (reader["FechaBaja"] != DBNull.Value)
                    {
                        diccRetorno.Add("FechaBaja", reader["FechaBaja"].ToString());
                    }
                    diccRetorno.Add("idCliente", reader["id"].ToString());
                }
                       
                else
                    diccRetorno.Add("Existe", Boolean.FalseString);

                reader.Close();
                con.Close();
            }
            return diccRetorno;
        }

        
        
        public static List<Libro> ObtenerLibros()
        {
            List<Libro> listaLibros = new List<Libro>();

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

                while (reader.Read()) //Podría usar un while, pero asumimos que sólo habrá uno con ese nombre de usuario
                {

                    // Crear un objeto para representar el registro
                    Libro libro = new Libro();
                    //Metemos la informacion al diccionario que queramos devolver
                    libro.idLibro = reader.GetInt32(0);
                    libro.ISBN = reader.GetString(1);
                    libro.Titulo = reader.GetString(2);
                    if (reader["Sinopsis"] != DBNull.Value)
                    {
                        libro.Sinopsis = reader.GetString(3);

                    }
                    if (reader["Autor"] != DBNull.Value)
                    {
                        libro.Autor = reader.GetString(4);
                    }
                    if (reader["Editorial"] != DBNull.Value)
                    {
                        libro.Editorial = reader.GetString(5);
                    }
                    if (reader["Coleccion"] != DBNull.Value)
                    {
                        libro.Coleccion = reader.GetString(6);
                    }
                    if (reader["FechaPrimeraEdicion"] != DBNull.Value)
                    {
                        libro.FechaPrimeraEdicion = reader.GetDateTime(7);
                    }


                    //diccRetornoLibros.Add("idLibro", reader["idLibro"].ToString());
                    //diccRetornoLibros.Add("ISBN", reader["ISBN"].ToString());
                    //diccRetornoLibros.Add("Titulo", reader["Titulo"].ToString());
                    //diccRetornoLibros.Add("Sinopsis", reader["Sinopsis"].ToString());
                    //diccRetornoLibros.Add("Autor", reader["Autor"].ToString());
                    //diccRetornoLibros.Add("Coleccion", reader["Coleccion"].ToString());
                    //diccRetornoLibros.Add("FechaPrimeraEdicion", reader["FechaPrimeraEdicion"].ToString());


                    listaLibros.Add(libro);
                }
                //else
                //    //Metemos la informacion al diccionario que queramos devolver
                //    diccRetornoLibros.Add("Error", "No se ha conseguido añadir la informacion de los libros");

                reader.Close();
                con.Close();
            }


            return listaLibros;
        }
    }

}
