using System;

namespace Servidor_API.Models
{
    public class Usuario
    {
        public int idUsuario { get; set; }

        public string UserName {  get; set; }
        public string PasswordHash { get; set; }
        public bool Activo { get; set; }

    }
}
