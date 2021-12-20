using Framework.DomainModels.Master;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Framework.Repositories
{
    public interface IGenericRepository
    {
        Task<CountryMaster> Search(int id, CancellationToken cancellationToken);
        Task<List<CountryMaster>> SearchList(string log, int top, int skip, CancellationToken cancellationToken);
        Task<string> insert(CountryMaster _countryMaster, CancellationToken cancellationToken);
        Task<string> update(CountryMaster _countryMaster, CancellationToken cancellationToken);
        Task<string> delete(int id, CancellationToken cancellationToken);
    }
}
