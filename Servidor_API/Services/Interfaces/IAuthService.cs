using Servidor_DTOS.DTOS.Auth;

namespace Servidor_API.Services.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponseDTO> Login(LoginRequestDTO request);
    }
}
