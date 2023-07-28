using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Securities
{
    public interface ITokenService
    {
        Task<string> GetJwtToken(string username, IList<Claim> claims);
    }
}
