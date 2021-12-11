using Base.Services;
using Framework.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Framework.Services
{
    public class GenericService : IGenericService
    {
        private readonly IGenericRepository genericRepository;

        public GenericService(IGenericRepository _genericRepository)
        {
            this.genericRepository = _genericRepository;
        }
        public async Task<ServiceDataResponse<string>> SearchEmployees(string log, int top, int skip, CancellationToken cancellationToken)
        {
            var response = new ServiceDataResponse<string>(await this.genericRepository.SearchEmployees(log, top, skip, cancellationToken).ConfigureAwait(false));


            return response;
        }
    }
}
