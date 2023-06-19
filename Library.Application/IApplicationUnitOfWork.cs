using Library.Application.Repositories;
using Library.Domain.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application
{
    public interface IApplicationUnitOfWork:IUnitOfWork
    {
        public IBookRepository Books { get; }
    }
}
