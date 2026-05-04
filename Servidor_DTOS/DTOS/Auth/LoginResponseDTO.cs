using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servidor_API.Models;

namespace Servidor_DTOS.DTOS.Auth
{
    public class LoginResponseDTO
    {
        public string Token { get; set; }

        public DateTime Expiration { get; set; }

        public string RefreshToken { get; set; }

        public string Username { get; set; }

        public List<RolDTO>? Roles { get; set; }
    }
}
