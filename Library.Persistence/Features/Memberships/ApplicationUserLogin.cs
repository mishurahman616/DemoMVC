using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Persistence.Features.Memberships
{
    public class ApplicationUserLogin:IdentityUserLogin<Guid>
    {
    }
}
