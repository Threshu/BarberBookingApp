using BarberBookingApp.Application.DTOs.Authentication;

namespace BarberBookingApp.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> Login(LoginDto request);
        Task<AuthResponseDto> Register(RegisterDto request);
    }
}
