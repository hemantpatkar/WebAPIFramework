using Base.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Framework.Services
{
    public interface IGenericService
    {

        Task<ServiceDataResponse<string>> SearchEmployees(string log, int top, int skip, CancellationToken cancellationToken);
    }
}
