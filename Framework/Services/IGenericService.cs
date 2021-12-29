using Base.Services;
using Framework.DomainModels.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Framework.Services
{
    public interface IGenericService
    {
        Task<ServiceDataResponse<CountryMaster>> Search(int id, CancellationToken cancellationToken);
        Task<ServiceDataResponse<List<CountryMaster>>> SearchList(string searchcriteria, int top, int skip, CancellationToken cancellationToken);
        Task<ServiceDataResponse<string>> insert(CountryMaster _countryMaster, CancellationToken cancellationToken);
        Task<ServiceDataResponse<string>> update(CountryMaster _countryMaster, CancellationToken cancellationToken);
        Task<ServiceDataResponse<string>> delete(int id, CancellationToken cancellationToken);
        Task<ServiceDataResponse<StateMaster>> SearchState(int id, CancellationToken cancellationToken);
    }
}
