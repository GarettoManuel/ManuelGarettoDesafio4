using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManuelGarettoDesafio4
{
    
    internal static class ManejadorProducto
    {
        public static string cadenaConexion = "Data Source=(localdb)\\localhost;Initial Catalog=SistemaGestion;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public static Models.Producto obtenerProducto(long idUsuario)
        {
            Models.Producto producto = new Models.Producto();
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                SqlCommand comando = new SqlCommand("SELECT * FROM Producto WHERE IdUsuario = @idUsuario", conn);
                conn.Open();

                SqlParameter prodParam = new SqlParameter();
                prodParam.Value = idUsuario;
                prodParam.SqlDbType = SqlDbType.VarChar;
                prodParam.ParameterName = "IdUsuario";

                comando.Parameters.Add(prodParam);

                SqlDataReader reader = comando.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        Console.WriteLine("Id = " + Convert.ToInt64(reader["Id"]));
                        Console.WriteLine("Descripciones = " + reader["Descripciones"].ToString());
                        Console.WriteLine("Costo = " + reader.GetDecimal(2));
                        Console.WriteLine("Precio Venta = " + Convert.ToDecimal(reader["PrecioVenta"]));
                        Console.WriteLine("Stock = " + Convert.ToInt32(reader["Stock"]));
                        Console.WriteLine("Id Usuario = " + Convert.ToInt64(reader["IdUsuario"]));


                    }


                }
                return producto;
            }

        }


        public static Models.Producto obtenerProductoDescripcion(string descripcion)
        {
            Models.Producto producto = new Models.Producto();
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                SqlCommand comando = new SqlCommand("SELECT * FROM Producto WHERE descripciones = @Descripciones", conn);
                conn.Open();

                SqlParameter prodParam = new SqlParameter();
                prodParam.Value = descripcion;
                prodParam.SqlDbType = SqlDbType.VarChar;
                prodParam.ParameterName = "Descripciones";

                comando.Parameters.Add(prodParam);

                SqlDataReader reader = comando.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();

                    producto.Id = reader.GetInt64(0);
                    producto.Descripciones = reader.GetString(1);
                    producto.Costo = reader.GetDecimal(2);
                    producto.PrecioVenta = reader.GetDecimal(3);
                    producto.Stock = reader.GetInt32(4);
                    producto.IdUsuario = reader.GetInt64(5);

                    Console.WriteLine("Id = " + Convert.ToInt64(reader["Id"]));
                    Console.WriteLine("Descripciones = " + reader["Descripciones"].ToString());
                    Console.WriteLine("Costo = " + reader.GetDecimal(2));
                    Console.WriteLine("Precio Venta = " + Convert.ToDecimal(reader["PrecioVenta"]));
                    Console.WriteLine("Stock = " + Convert.ToInt32(reader["Stock"]));
                    Console.WriteLine("Id Usuario = " + Convert.ToInt64(reader["IdUsuario"]));


                }
                return producto;
            }
        }

        public static int BorrarProducto (long id)
        {
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
                try{
                    SqlCommand comando = new SqlCommand("DELETE FROM Producto WHERE id=@id", conn);
                    comando.Parameters.AddWithValue("@id", id);
                    conn.Open();
                    return comando.ExecuteNonQuery();
            }
                catch(Exception ex)
                {
                    return -1;
                }
        }

        public static int CrearProducto(Models.Producto producto)
        {
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                SqlCommand comando = new SqlCommand("INSERT INTO Producto(Descripciones, Costo, PrecioVenta, Stock, IdUsuario)" +
                    "VALUES(@descripciones, @costo, @precioVenta, @stock, @idUsuario)", conn);
                comando.Parameters.AddWithValue("@descripciones", producto.Descripciones);
                comando.Parameters.AddWithValue("@costo", producto.Costo);
                comando.Parameters.AddWithValue("@precioVenta", producto.PrecioVenta);
                comando.Parameters.AddWithValue("@stock", producto.Stock);
                comando.Parameters.AddWithValue("@idUsuario", producto.IdUsuario);

                conn.Open();
                return comando.ExecuteNonQuery();

            }
        }

        public static int ModificarProducto(Models.Producto producto, long id)
        {
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                try
                {
                    SqlCommand comando = new SqlCommand("UPDATE Producto SET Descripciones = @descripciones, Costo = @costo, PrecioVenta = @precioVenta, Stock = @stock, IdUsuario = @idUsuario WHERE Id = @id", conn);
                    comando.Parameters.AddWithValue("@id", producto.Id);
                    comando.Parameters.AddWithValue("@descripciones", producto.Descripciones);
                    comando.Parameters.AddWithValue("@costo", producto.Costo);
                    comando.Parameters.AddWithValue("@precioVenta", producto.PrecioVenta);
                    comando.Parameters.AddWithValue("@stock", producto.Stock);
                    comando.Parameters.AddWithValue("@idUsuario", producto.IdUsuario);
                    conn.Open();
                    return comando.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    return -1;
                }
            }
        }
    }
}
