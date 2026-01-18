using LibraryManagementAPI.DTOs;

namespace LibraryManagementAPI.IServices
{
    public interface IAuthService
    {
        Task<RegResponseDto?> RegisterAsync(RegisterDto registerDto);
        Task<AuthResponseDto?> LoginAsync(LoginDto loginDto);
    }
}
