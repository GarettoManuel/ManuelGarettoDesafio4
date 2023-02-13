using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManuelGarettoDesafio4
{
    internal static class ManejadorUsuario
    {
        public static string cadenaConexion = "Data Source=(localdb)\\localhost;Initial Catalog=SistemaGestion;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";


        public static Models.Usuario devolverUsuario(long id)
        {


            Models.Usuario usuario = new Models.Usuario();
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                SqlCommand comando = new SqlCommand("SELECT * FROM Usuario WHERE Id=@id", conn);
                SqlParameter idParam = new SqlParameter();
                idParam.Value = id;
                idParam.SqlDbType = SqlDbType.VarChar;
                idParam.ParameterName = "id";

                comando.Parameters.Add(idParam);

                /*SqlCommand comando = new SqlCommand("SELECT * FROM Usuario WHERE Id=1", conn);*/
                conn.Open();

                SqlDataReader reader = comando.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();

                    usuario.Id = reader.GetInt64(0);
                    usuario.Nombre = reader.GetString(1);
                    usuario.Apellido = reader.GetString(2);
                    usuario.NombreUsuario = reader.GetString(3);
                    usuario.Contraseña = reader.GetString(4);
                    usuario.Mail = reader.GetString(5);

                }

                Console.WriteLine("Id = " + usuario.Id);
                Console.WriteLine("Nombre = " + usuario.Nombre);
                Console.WriteLine("Apellido = " + usuario.Apellido);
                Console.WriteLine("Nombre Usuario = " + usuario.NombreUsuario);
                Console.WriteLine("Contraseña = " + usuario.Contraseña);
                Console.WriteLine("Mail = " + usuario.Mail);

            }
            return usuario;
        }

        public static int InsertarUsuario(Models.Usuario usuario)
        {
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Usuario(Nombre, Apellido, NombreUsuario, Contraseña, Mail) VALUES (@nombre, @apellido, @nombreUsuario, @contrasena, @mail)", conn);
                SqlParameter nombreParam = new SqlParameter();
                nombreParam.ParameterName = "nombre";
                nombreParam.SqlDbType = SqlDbType.VarChar;
                nombreParam.Value = usuario.Nombre;

                SqlParameter apellidoParam = new SqlParameter("apellido", usuario.Apellido);
                SqlParameter nombreUsuParam = new SqlParameter("nombreUsuario", usuario.NombreUsuario);
                SqlParameter passwParam = new SqlParameter("contrasena", usuario.Contraseña);
                SqlParameter mailParam = new SqlParameter("mail", usuario.Mail);

                cmd.Parameters.Add(nombreParam);
                cmd.Parameters.Add(apellidoParam);
                cmd.Parameters.Add(nombreUsuParam);
                cmd.Parameters.Add(passwParam);
                cmd.Parameters.Add(mailParam);

                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        public static Models.Usuario Login(string mail, string passw)
        {
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Usuario WHERE Mail = @mail AND Contraseña = @passw", conn);

                //Se utiliza SQL Parameter para reemplazar los @ de la consulta
                SqlParameter parameterMail = new SqlParameter();
                parameterMail.ParameterName = "mail";
                parameterMail.SqlValue = SqlDbType.VarChar;
                parameterMail.Value = mail;

                SqlParameter parameterContrasena = new SqlParameter();
                parameterContrasena.ParameterName = "passw";
                parameterContrasena.SqlValue = SqlDbType.VarChar;
                parameterContrasena.Value = passw;

                //Se aplica los parámetros al comando
                command.Parameters.Add(parameterMail);
                command.Parameters.Add(parameterContrasena);
                conn.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        Models.Usuario usuarioEncontrado = new Models.Usuario();
                        reader.Read();
                        usuarioEncontrado.Nombre = reader.GetString(1);
                        usuarioEncontrado.Apellido = reader.GetString(2);
                        usuarioEncontrado.NombreUsuario = reader.GetString(3);
                        usuarioEncontrado.Mail = reader.GetString(5);
                        return usuarioEncontrado;
                    }
                }
                //En caso de que la consulta este vacía retornara un Usuario vacio
                return null;
            }
        }

        public static int ModificarUsuario(Models.Usuario usuario, long id)
        {
            
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                try
                {
                    SqlCommand comando = new SqlCommand("UPDATE Usuario SET Nombre = @nombre, Apellido = @apellido, NombreUsuario = @nombreUsuario, Contraseña = @contraseña, Mail = @mail WHERE Id = @id", conn);
                    comando.Parameters.AddWithValue("@id", usuario.Id);
                    comando.Parameters.AddWithValue("@nombre", usuario.Nombre);
                    comando.Parameters.AddWithValue("@apellido", usuario.Apellido);
                    comando.Parameters.AddWithValue("@nombreUsuario", usuario.NombreUsuario);
                    comando.Parameters.AddWithValue("@contraseña", usuario.Contraseña);
                    comando.Parameters.AddWithValue("@mail", usuario.Mail);
                    conn.Open();
                    return comando.ExecuteNonQuery();
                }
                catch(Exception ex) 
                {
                    return -1;
                }      
            }
        }

    }
}
