using System.Security.Claims;

namespace HelloToAsp.Extensions
{
    public static class ClaimsPrincipalExtentions
    {
        public static int GetAuthUserId(this ClaimsPrincipal principal)
        {
            return Int32.Parse(principal.Claims.First(x => x.Type.Equals("Id")).Value);
        }
    }
}
