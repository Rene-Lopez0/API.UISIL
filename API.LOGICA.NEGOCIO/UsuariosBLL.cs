using API.DAPPER;
using API.ENTIDADES;
using System;
using System.Collections.Generic;
using System.Text;

namespace API.LOGICA.NEGOCIO
{
    public class UsuariosBLL
    {

        //Instancia 

        private readonly UsuariosDAL _UsuarioDAL;


        //Constructor


        public UsuariosBLL(UsuariosDAL usuariosDAL)
        {

            _UsuarioDAL = usuariosDAL;

        }


        public Respuesta<UsuariosDto> InsertarUsuarios(UsuariosDto ObjUsuario)
        {

            Respuesta<UsuariosDto> respuesta = new Respuesta<UsuariosDto>();

            try
            {


                if (ObjUsuario.Correo != null && ObjUsuario.Contrasena != null && ObjUsuario.FechaNac != null && ObjUsuario.IdRol != 0 && ObjUsuario.Nombre != null &&
                    ObjUsuario.Apellido1 != null && ObjUsuario.Apellido2 != null && ObjUsuario.Genero != null && ObjUsuario.Telefono != null && ObjUsuario.Direccion != null)
                {

                    var reply = _UsuarioDAL.InsertarUsuarios(ObjUsuario);

                    if (reply != null)
                    {
                        respuesta = reply;

                    }


                }
                else
                {

                    respuesta.Ok = false;
                    respuesta.Mensaje = "Uno o varios de los valores están nulos, favor revisar!";
                    respuesta.ValorRetorno = null;
                }




            }
            catch (Exception ex)
            {

                respuesta.Ok = false;
                respuesta.Mensaje = $"Ha ocurrido un error en la capa BLL en el método InsertarUsuarios {ex.Message}";
                respuesta.ValorRetorno = null;
            }

            return respuesta;


        }

        public Respuesta<UsuariosDto> ObtenerUsuarioPorId(UsuariosDto ObjUsuario)
        {

            Respuesta<UsuariosDto> respuesta = new Respuesta<UsuariosDto>();

            try
            {

                var reply = _UsuarioDAL.ObtenerUsuarioPorId(ObjUsuario);

                if (reply != null)
                {
                    respuesta = reply;

                }


            }
            catch (Exception ex)
            {

                respuesta.Ok = false;
                respuesta.Mensaje = $"Ha ocurrido un error en la capa BLL en el método ObtenerUsuarioPorId {ex.Message}";
                respuesta.ValorRetorno = null;
            }

            return respuesta;


        }


    }
}
