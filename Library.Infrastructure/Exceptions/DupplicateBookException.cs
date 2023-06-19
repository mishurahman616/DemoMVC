using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Exceptions
{
    public class DupplicateBookException:Exception
    {
        public DupplicateBookException(string message):base(message) { }
    }
}
