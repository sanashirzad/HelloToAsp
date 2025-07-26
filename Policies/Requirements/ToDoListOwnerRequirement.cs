using Microsoft.AspNetCore.Authorization;

namespace HelloToAsp.Policies.Requirements
{
    public class ToDoListOwnerRequirement : IAuthorizationRequirement
    {
    }
}
