using HelloToAsp.Core.Dtos.Auth;
using Microsoft.AspNetCore.Identity;

namespace HelloToAsp.Core.Contracts
{
    public interface IAuthManager
    {
        Task<IEnumerable<IdentityError>> Register(RegUserDto regUserDto);
        Task<AuthResponseDto> Login(LogUserDto logUserDto);
        Task<AuthResponseDto> VerifyRefreshToken(AuthResponseDto request);
    }
}
