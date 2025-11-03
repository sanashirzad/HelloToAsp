using HelloToAsp.Data;
using HelloToAsp.Extensions;
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
            var userId = context.User.GetAuthUserId();

            if (userId == resource.UserId)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
