using System;
using System.Collections.Generic;
using System.Text;

namespace API.ENTIDADES
{
    public class InicioSesionDto
    {

        public int IdUsuario { get; set;}
        public string? Correo { get; set;}
        public string? Contrasena { get; set;}
        public string? NombreCompleto { get; set;}

    }
}
