using API.ENTIDADES;
using API.LOGICA.NEGOCIO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.UISIL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {


        //Instancias

        private readonly UsuariosBLL _UsuarioBLL;

        public UsuariosController(UsuariosBLL usuariosBLL)
        {
            _UsuarioBLL = usuariosBLL;

        }


        [HttpPost]
        public Respuesta<UsuariosDto> InsertarUsuarios([FromBody] UsuariosDto ObjUsuario)
        {

            Respuesta<UsuariosDto> respuesta = new Respuesta<UsuariosDto>();

            try
            {

                var reply = _UsuarioBLL.InsertarUsuarios(ObjUsuario);

                if (reply != null)
                {

                    respuesta = reply;

                }

            }
            catch (Exception ex)
            {

                respuesta.Ok = false;
                respuesta.Mensaje = $"Ha ocurrido un error en la capa del controlador UsuariosController en el método InsertarUsuarios {ex.Message}";
                respuesta.ValorRetorno = null;
            }

            return respuesta;

        }

        [HttpPost("ObtenerUsuarioPorId")]
        public Respuesta<UsuariosDto> ObtenerUsuarioPorId([FromBody] UsuariosDto ObjUsuario)
        {

            Respuesta<UsuariosDto> respuesta = new Respuesta<UsuariosDto>();

            try
            {

                var reply = _UsuarioBLL.ObtenerUsuarioPorId(ObjUsuario);

                if (reply != null)
                {

                    respuesta = reply;

                }

            }
            catch (Exception ex)
            {

                respuesta.Ok = false;
                respuesta.Mensaje = $"Ha ocurrido un error en la capa del controlador UsuariosController en el método ObtenerUsuarioPorId {ex.Message}";
                respuesta.ValorRetorno = null;
            }

            return respuesta;

        }



    }
}
