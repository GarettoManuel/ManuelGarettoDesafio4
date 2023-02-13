using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManuelGarettoDesafio4
{
    internal static class ManejadorProductosVendidos
    {
        public static string cadenaConexion = "Data Source=(localdb)\\localhost;Initial Catalog=SistemaGestion;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public static List<Models.Producto> obtenerProductosVendidos(long idUsuario)
        {
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                List<long> idProductos = new List<long>();
                SqlCommand comando = new SqlCommand($"SELECT IdProducto FROM Venta INNER JOIN ProductoVendido ON Venta.Id = ProductoVendido.IdVenta WHERE IdUsuario = {idUsuario}", conn);

                List<Models.Producto> productoVendido = new List<Models.Producto>();
                conn.Open();

                SqlDataReader reader = comando.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        idProductos.Add(reader.GetInt64(0));

                    }
                }

                List<Models.Producto> productos = new List<Models.Producto>();
                foreach (var item in idProductos)
                {
                    Models.Producto prodTemp = ManejadorProducto.obtenerProducto(item);
                    productos.Add(prodTemp);
                }
                return productos;

            }

        }
    }
}
