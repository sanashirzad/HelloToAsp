using HelloToAsp.Dtos.Auth;
using Microsoft.AspNetCore.Identity;

namespace HelloToAsp.Contracts
{
    public interface IAuthManager
    {
        Task<IEnumerable<IdentityError>> Register(RegUserDto regUserDto);
        Task<AuthResponseDto> Login(LogUserDto logUserDto);
    }
}
