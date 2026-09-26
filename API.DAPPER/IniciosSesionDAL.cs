using API.ENTIDADES;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace API.DAPPER
{
    public class IniciosSesionDAL
    {

        // Procedimientos almacenados

        private const string sp_IniciarSesion = "spIniciarSesion";

        //Instancias

        private readonly IConfiguration _Config;


        public IniciosSesionDAL(IConfiguration configuration)
        {

            _Config = configuration;

        }


        public Respuesta<InicioSesionDto> IniciarSesion(InicioSesionDto ObjUsuario)
        {

            Respuesta<InicioSesionDto> respuesta = new Respuesta<InicioSesionDto>();

            try
            {

                string? ConexionDB = _Config.GetConnectionString("Conexion");

                using (SqlConnection connection = new SqlConnection(ConexionDB))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(sp_IniciarSesion, connection))
                    {

                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        command.Parameters.Add(new SqlParameter("@pCorreo", System.Data.SqlDbType.NVarChar, 80) { Value = ObjUsuario.Correo });
                        command.Parameters.Add(new SqlParameter("@Contrasena", System.Data.SqlDbType.NVarChar, 50) { Value = ObjUsuario.Contrasena });


                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            if (reader.Read())
                            {
                                bool ExisteUsuario = (bool)reader["Existe"];

                                InicioSesionDto ObjInfo = new InicioSesionDto
                                {
                                    NombreCompleto = (string)reader["NombreCompleto"]

                                };

                                if (ExisteUsuario)
                                {

                                    respuesta.Ok = true;
                                    respuesta.Mensaje = (string)reader["Mensaje"];
                                    respuesta.ValorRetorno = ObjInfo;

                                }
                                else
                                {
                                    respuesta.Ok = false;
                                    respuesta.Mensaje = (string)reader["Mensaje"];
                                    respuesta.ValorRetorno = null;
                                }


                            }

                        }


                    }

                }

            }
            catch (Exception ex)
            {

                respuesta.Ok = false;
                respuesta.Mensaje = $"Ha ocurrido un error en el método IniciarSesion en la capa dapper {ex.Message}";
            }

            return respuesta;



        }




    }
}
