using API.DAPPER;
using API.ENTIDADES;

namespace API.LOGICA.NEGOCIO
{
    public class InicioSesionBLL
    {

        //Instancia 

        private readonly IniciosSesionDAL _InicioSesionDAL;


        //Constructor


        public InicioSesionBLL(IniciosSesionDAL iniciosSesiondal)
        {

            _InicioSesionDAL = iniciosSesiondal;

        }


        public Respuesta<InicioSesionDto> IniciarSesion(InicioSesionDto ObjUsuario)
        {

            Respuesta<InicioSesionDto> respuesta = new Respuesta<InicioSesionDto>();

            try
            {

                var reply = _InicioSesionDAL.IniciarSesion(ObjUsuario);

                if (reply != null)
                {
                    respuesta = reply;

                }


            }
            catch (Exception ex)
            {

                respuesta.Ok = false;
                respuesta.Mensaje = $"Ha ocurrido un error en la capa BLL en el método IniciarSesion {ex.Message}";
                respuesta.ValorRetorno = null;
            }

            return respuesta;


        }


    }
}
