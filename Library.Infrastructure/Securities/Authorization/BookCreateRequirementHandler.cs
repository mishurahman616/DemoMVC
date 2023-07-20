using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace Library.Infrastructure.Securities.Authorization
{
    public class BookCreateRequirementHandler : AuthorizationHandler<BookCreateRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context, 
            BookCreateRequirement requirement)
        {
            if (context.User.HasClaim(x => x.Type == "ViewCreateClaim" && x.Value == "True"))
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
