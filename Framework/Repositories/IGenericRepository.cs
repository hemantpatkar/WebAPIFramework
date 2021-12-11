using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Framework.Repositories
{
    public interface IGenericRepository
    {
        Task<string> SearchEmployees(string log, int top, int skip, CancellationToken cancellationToken);
    }
}
