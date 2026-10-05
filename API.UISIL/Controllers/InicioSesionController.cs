using API.ENTIDADES;
using API.LOGICA.NEGOCIO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.UISIL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InicioSesionController : ControllerBase
    {


        //Instancias

        private readonly InicioSesionBLL _inicioSesionBLL;

        public InicioSesionController(InicioSesionBLL inicioSesionBLL)
        {
            _inicioSesionBLL = inicioSesionBLL;

        }


        [HttpPost]
        public Respuesta<InicioSesionDto> IniciarSesion([FromBody] InicioSesionDto ObjUsuario)
        {

            Respuesta<InicioSesionDto> respuesta = new Respuesta<InicioSesionDto>();

            try
            {

                var reply = _inicioSesionBLL.IniciarSesion(ObjUsuario);

                if (reply != null)
                {

                    respuesta = reply;

                }

            }
            catch (Exception ex)
            {

                respuesta.Ok = false;
                respuesta.Mensaje = $"Ha ocurrido un error en la capa del controlador InicioSesionController en el método iniciarsesion {ex.Message}";
                respuesta.ValorRetorno = null;
            }

            return respuesta;

        }



    }
}
