using HelloToAsp.Data;
using HelloToAsp.Policies.Requirements;
using Microsoft.AspNetCore.Authorization;

namespace HelloToAsp.Policies.Handlers
{
    public class ToDoListOwnerHandler : AuthorizationHandler<ToDoListOwnerRequirement, ToDoList>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            ToDoListOwnerRequirement requirement,
            ToDoList resource
            )
        {
            var userId = Int32.Parse(context.User.Claims.First(x => x.Type.Equals("Id")).Value);

            if (userId == resource.UserId)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
