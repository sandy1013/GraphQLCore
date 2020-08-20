using GraphQL.Types;
using GraphQLCore.Data.Models.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GraphQLCoreAPI.Authentication
{
    public class AuthenticationUtility
    {
        public bool ValidateContext(ResolveFieldContext<object> context)
        {
            ClaimsPrincipal userContext = context.UserContext as ClaimsPrincipal;

            bool IsAuthenticated = userContext.Identity.IsAuthenticated;

            var claims = userContext.Claims.ToList();
            var userName = claims.FirstOrDefault(o => o.Type == ClaimTypes.Name)?.Value;
            var email = claims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value;
            var role = claims.FirstOrDefault(o => o.Type == "Role")?.Value;

            return IsAuthenticated;
        }
    }
}
