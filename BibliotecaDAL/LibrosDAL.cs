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

                    for (var i = 0; i < reader.FieldCount; i++)
                    {
                        var columnName = reader.GetName(i);
                        var columnValue = reader.GetValue(i);

                        // Utilizar un switch para asignar los valores a las propiedades segÃºn el nombre de la columna
                        switch (columnName)
                        {
                            case "idLibro":
                                libro.idLibro = (int)(columnValue as int?);
                                break;
                            case "ISBN":
                                libro.ISBN = columnValue as string;
                                break;
                            case "Titulo":
                                libro.Titulo = columnValue as string;
                                break;
                            case "Sinopsis":
                                libro.Sinopsis = columnValue as string;
                                break;
                            case "Autor":
                                libro.Autor = columnValue as string;
                                break;
                            case "Editorial":
                                libro.Editorial = columnValue as string;
                                break;
                            case "Coleccion":
                                libro.Coleccion = columnValue as string;
                                break;
                            case "FechaPrimeraEdicion":
                                libro.FechaPrimeraEdicion = columnValue as DateTime?;
                                break;

                        }
                    }

                    listalibros.Add(libro);
                }
            }

            return listalibros;
        }
    }
}
