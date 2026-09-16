using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace todo.api.Services.Auth
{
    public interface IJwtService
    {
        string GenerarToken(IEnumerable<Claim>claims);
    }
}