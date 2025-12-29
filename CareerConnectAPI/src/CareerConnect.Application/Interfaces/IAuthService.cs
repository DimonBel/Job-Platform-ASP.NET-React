using CareerConnect.Application.DTOs.Auth;

namespace CareerConnect.Application.Interfaces;

public interface IAuthService
{
    Task<AuthResponseDto> RegisterAsync(RegisterDto dto, CancellationToken cancellationToken = default);
    Task<AuthResponseDto> LoginAsync(LoginDto dto, CancellationToken cancellationToken = default);
    Task<UserInfoDto?> GetUserByIdAsync(int userId, CancellationToken cancellationToken = default);
    Task<AuthResponseDto> RefreshTokenAsync(int userId, CancellationToken cancellationToken = default);
    Task<UserInfoDto?> UpdateProfileAsync(int userId, UpdateProfileDto dto, CancellationToken cancellationToken = default);
}
