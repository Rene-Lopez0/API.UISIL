using API.ENTIDADES;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace API.DAPPER
{
    public class UsuariosDAL
    {

        // Procedimientos almacenados

        private const string sp_InsertarUsuarios = "InsertarUsuarios";
        private const string sp_ObtenerUsuariosPorId = "ObtenerInformacionUsuariosPorId";
        private const string sp_ObtenerInformacionUsuarios = "ObtenerInformacionUsuarios";
        private const string sp_ActualizarUsuarioPorId = "ActualizarUsuarioPorId";
        private const string sp_EliminarUsuarioPorId = "EliminarUsuarioPorId";




        //Instancias

        private readonly IConfiguration _Config;


        public UsuariosDAL(IConfiguration configuration)
        {

            _Config = configuration;

        }


        public Respuesta<UsuariosDto> InsertarUsuarios(UsuariosDto ObjUsuario)
        {

            Respuesta<UsuariosDto> respuesta = new Respuesta<UsuariosDto>();

            try
            {

                string? ConexionDB = _Config.GetConnectionString("Conexion");

                using (SqlConnection connection = new SqlConnection(ConexionDB))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(sp_InsertarUsuarios, connection))
                    {

                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        command.Parameters.Add(new SqlParameter("@pCorreo", System.Data.SqlDbType.NVarChar, 100) { Value = ObjUsuario.Correo });
                        command.Parameters.Add(new SqlParameter("@pContrasena", System.Data.SqlDbType.NVarChar, 50) { Value = ObjUsuario.Contrasena });
                        command.Parameters.Add(new SqlParameter("@pIdRol", System.Data.SqlDbType.Int) { Value = ObjUsuario.IdRol });
                        command.Parameters.Add(new SqlParameter("@pNombre", System.Data.SqlDbType.NVarChar, 50) { Value = ObjUsuario.Nombre });
                        command.Parameters.Add(new SqlParameter("@pApellido1", System.Data.SqlDbType.NVarChar, 50) { Value = ObjUsuario.Apellido1 });
                        command.Parameters.Add(new SqlParameter("@pApellido2", System.Data.SqlDbType.NVarChar, 50) { Value = ObjUsuario.Apellido2 });
                        command.Parameters.Add(new SqlParameter("@pFechaNac", System.Data.SqlDbType.DateTime) { Value = ObjUsuario.FechaNac });
                        command.Parameters.Add(new SqlParameter("@pGenero", System.Data.SqlDbType.NVarChar, 30) { Value = ObjUsuario.Genero });
                        command.Parameters.Add(new SqlParameter("@pTelefono", System.Data.SqlDbType.NVarChar, 20) { Value = ObjUsuario.Telefono });
                        command.Parameters.Add(new SqlParameter("@pDireccion", System.Data.SqlDbType.NVarChar, 500) { Value = ObjUsuario.Direccion });

                        int FilasAfectadas = command.ExecuteNonQuery();

                        if (FilasAfectadas > 0)
                        {

                            respuesta.Ok = true;
                            respuesta.Mensaje = "El usuario ha sido agregado de manera exitosa";
                            respuesta.ValorRetorno = null;


                        }
                        else
                        {

                            respuesta.Ok = false;
                            respuesta.Mensaje = "Ha ocurrido un error al insertar el usuario en la base de datos";
                            respuesta.ValorRetorno = null;
                        }



                    }

                }

            }

            catch (Exception ex)
            {

                respuesta.Ok = false;
                respuesta.Mensaje = $"Ha ocurrido un error en el método InsertarUsuarios en la capa dapper {ex.Message}";
            }

            return respuesta;

        }
        public Respuesta<UsuariosDto> ObtenerUsuarioPorId(UsuariosDto ObjUsuario)
        {

            Respuesta<UsuariosDto> respuesta = new Respuesta<UsuariosDto>();

            try
            {

                string? ConexionDB = _Config.GetConnectionString("Conexion");

                using (SqlConnection connection = new SqlConnection(ConexionDB))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(sp_ObtenerUsuariosPorId, connection))
                    {

                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        command.Parameters.Add(new SqlParameter("@pIdUsuario", System.Data.SqlDbType.Int) { Value = ObjUsuario.IdUsuario });


                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            if (reader.Read())
                            {

                                UsuariosDto Obj = new UsuariosDto
                                {

                                    IdUsuario = (int)reader["IdUsuario"],
                                    Correo = (string)reader["Correo"],
                                    FechaCreacion = (DateTime)reader["FechaCreacion"],
                                    Nombre = (string)reader["Nombre"],
                                    Apellido1 = (string)reader["Apellido1"],
                                    Apellido2 = (string)reader["Apellido2"],
                                    FechaNac = (DateTime)reader["FechaNac"],
                                    Genero = (string)reader["Genero"],
                                    Telefono = (string)reader["Telefono"],
                                    Direccion = (string)reader["Direccion"],
                                    NombreRol = (string)reader["NombreRol"]


                                };

                                respuesta.Ok = true;
                                respuesta.Mensaje = "Se ha obtenido la información del usuario de manera exitosa";
                                respuesta.ValorRetorno = Obj;


                            }
                            else
                            {
                                respuesta.Ok = false;
                                respuesta.Mensaje = "El usuario que desea buscar, no ha sido encontrado";
                                respuesta.ValorRetorno = null;

                            }


                        }



                    }

                }

            }

            catch (Exception ex)
            {

                respuesta.Ok = false;
                respuesta.Mensaje = $"Ha ocurrido un error en el método ObtenerUsuarioPorId en la capa dapper {ex.Message}";
            }

            return respuesta;

        }
        public Respuesta<List<UsuariosDto>> ObtenerUsuarios()
        {

            Respuesta<List<UsuariosDto>> respuesta = new Respuesta<List<UsuariosDto>>();
            List<UsuariosDto> ListaUsuarios = new List<UsuariosDto>();
            try
            {

                string? ConexionDB = _Config.GetConnectionString("Conexion");

                using (SqlConnection connection = new SqlConnection(ConexionDB))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(sp_ObtenerInformacionUsuarios, connection))
                    {

                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            while (reader.Read())
                            {

                                UsuariosDto Obj = new UsuariosDto
                                {
                                    IdUsuario = (int)reader["IdUsuario"],
                                    Correo = (string)reader["Correo"],
                                    FechaCreacion = (DateTime)reader["FechaCreacion"],
                                    Nombre = (string)reader["Nombre"],
                                    Apellido1 = (string)reader["Apellido1"],
                                    Apellido2 = (string)reader["Apellido2"],
                                    FechaNac = (DateTime)reader["FechaNac"],
                                    Genero = (string)reader["Genero"],
                                    Telefono = (string)reader["Telefono"],
                                    Direccion = (string)reader["Direccion"],
                                    NombreRol = (string)reader["NombreRol"]


                                };

                                ListaUsuarios.Add(Obj);

                            }


                            respuesta.Ok = true;
                            respuesta.Mensaje = "Se ha obtenido la información del usuario de manera exitosa";
                            respuesta.ValorRetorno = ListaUsuarios;



                        }



                    }

                }

            }

            catch (Exception ex)
            {

                respuesta.Ok = false;
                respuesta.Mensaje = $"Ha ocurrido un error en el método ObtenerUsuarios en la capa dapper {ex.Message}";
            }

            return respuesta;

        }
        public Respuesta<UsuariosDto> ActualizarUsuarioPorId(UsuariosDto ObjUsuario)
        {

            Respuesta<UsuariosDto> respuesta = new Respuesta<UsuariosDto>();

            try
            {

                string? ConexionDB = _Config.GetConnectionString("Conexion");

                using (SqlConnection connection = new SqlConnection(ConexionDB))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(sp_ActualizarUsuarioPorId, connection))
                    {

                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        command.Parameters.Add(new SqlParameter("@pIdUsuario", System.Data.SqlDbType.Int) { Value = ObjUsuario.IdUsuario });
                        command.Parameters.Add(new SqlParameter("@pCorreo", System.Data.SqlDbType.NVarChar, 100) { Value = ObjUsuario.Correo });
                        command.Parameters.Add(new SqlParameter("@pContrasena", System.Data.SqlDbType.NVarChar, 50) { Value = ObjUsuario.Contrasena });
                        command.Parameters.Add(new SqlParameter("@pIdRol", System.Data.SqlDbType.Int) { Value = ObjUsuario.IdRol });
                        command.Parameters.Add(new SqlParameter("@pNombre", System.Data.SqlDbType.NVarChar, 50) { Value = ObjUsuario.Nombre });
                        command.Parameters.Add(new SqlParameter("@pApellido1", System.Data.SqlDbType.NVarChar, 50) { Value = ObjUsuario.Apellido1 });
                        command.Parameters.Add(new SqlParameter("@pApellido2", System.Data.SqlDbType.NVarChar, 50) { Value = ObjUsuario.Apellido2 });
                        command.Parameters.Add(new SqlParameter("@pFechaNac", System.Data.SqlDbType.DateTime) { Value = ObjUsuario.FechaNac });
                        command.Parameters.Add(new SqlParameter("@pGenero", System.Data.SqlDbType.NVarChar, 30) { Value = ObjUsuario.Genero });
                        command.Parameters.Add(new SqlParameter("@pTelefono", System.Data.SqlDbType.NVarChar, 20) { Value = ObjUsuario.Telefono });
                        command.Parameters.Add(new SqlParameter("@pDireccion", System.Data.SqlDbType.NVarChar, 500) { Value = ObjUsuario.Direccion });

                        int FilasAfectadas = command.ExecuteNonQuery();

                        if (FilasAfectadas > 0)
                        {

                            respuesta.Ok = true;
                            respuesta.Mensaje = "El usuario ha sido actualizado de manera exitosa";
                            respuesta.ValorRetorno = null;


                        }
                        else
                        {

                            respuesta.Ok = false;
                            respuesta.Mensaje = "Ha ocurrido un error al actualizar el usuario en la base de datos";
                            respuesta.ValorRetorno = null;
                        }



                    }

                }

            }

            catch (Exception ex)
            {

                respuesta.Ok = false;
                respuesta.Mensaje = $"Ha ocurrido un error en el método ActualizarUsuarioPorId en la capa dapper {ex.Message}";
            }

            return respuesta;

        }

        public Respuesta<UsuariosDto> EliminarUsuarioPorId(UsuariosDto ObjUsuario)
        {

            Respuesta<UsuariosDto> respuesta = new Respuesta<UsuariosDto>();

            try
            {

                string? ConexionDB = _Config.GetConnectionString("Conexion");

                using (SqlConnection connection = new SqlConnection(ConexionDB))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(sp_EliminarUsuarioPorId, connection))
                    {

                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        command.Parameters.Add(new SqlParameter("@pIdUsuario", System.Data.SqlDbType.Int) { Value = ObjUsuario.IdUsuario });

                        int FilasAfectadas = command.ExecuteNonQuery();

                        if (FilasAfectadas > 0)
                        {

                            respuesta.Ok = true;
                            respuesta.Mensaje = "El usuario ha sido eliminado de manera exitosa";
                            respuesta.ValorRetorno = null;


                        }
                        else
                        {

                            respuesta.Ok = false;
                            respuesta.Mensaje = "Ha ocurrido un error al eliminar el usuario en la base de datos";
                            respuesta.ValorRetorno = null;
                        }



                    }

                }

            }

            catch (Exception ex)
            {

                respuesta.Ok = false;
                respuesta.Mensaje = $"Ha ocurrido un error en el método EliminarUsuarioPorId en la capa dapper {ex.Message}";
            }

            return respuesta;

        }
    }
}









