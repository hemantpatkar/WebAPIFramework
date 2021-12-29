using Base.Services;
using Framework.DomainModels.Master;
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

        public async Task<ServiceDataResponse<string>> delete(int id, CancellationToken cancellationToken)
        {
            var response = new ServiceDataResponse<string>(await this.genericRepository.delete(id, cancellationToken).ConfigureAwait(false));
            return response;
        }

        public async Task<ServiceDataResponse<string>> insert(CountryMaster _countryMaster, CancellationToken cancellationToken)
        {
            var response = new ServiceDataResponse<string>(await this.genericRepository.update(_countryMaster, cancellationToken).ConfigureAwait(false));
            return response;
        }

        public async Task<ServiceDataResponse<CountryMaster>> Search(int id, CancellationToken cancellationToken)
        {
            var response = new ServiceDataResponse<CountryMaster>(await this.genericRepository.Search(id, cancellationToken).ConfigureAwait(false));
            return response;
        }


        public async Task<ServiceDataResponse<string>> update(CountryMaster _countryMaster, CancellationToken cancellationToken)
        {
            var response = new ServiceDataResponse<string>(await this.genericRepository.update(_countryMaster, cancellationToken).ConfigureAwait(false));
            return response;
        }

        public async Task<ServiceDataResponse<List<CountryMaster>>> SearchList(string searchcriteria, int top, int skip, CancellationToken cancellationToken)
        {
            var response = new ServiceDataResponse<List<CountryMaster>>(await this.genericRepository.SearchList(searchcriteria, top, skip, cancellationToken).ConfigureAwait(false));
            return response;
        }
       
        public async Task<ServiceDataResponse<StateMaster>> SearchState(int id, CancellationToken cancellationToken)
        {
            var response = new ServiceDataResponse<StateMaster>(await this.genericRepository.SearchState(id, cancellationToken).ConfigureAwait(false));
            return response;
        }
    }
}
