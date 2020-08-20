using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GraphQLCoreAPI.Authentication.Interfaces
{
    public interface IJwtAuthenticationHandler
    {
        public Tuple<string, DateTime> GenerateJwtToken(Claim[] claims);

        public string GenerateRefreshToken();

    }
}
