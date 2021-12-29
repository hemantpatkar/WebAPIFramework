using Base.Exceptions;
using Framework.DomainModels.Master;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Framework.Repositories
{
    public class GenericRepository : IGenericRepository
    {
        private readonly ISharedDbContext dbContext;
        public GenericRepository(
          ISharedDbContext dbContext,
          IExceptionManagement exceptionManagement)
        {
            this.dbContext = dbContext;
        }

        public async Task<string> delete(int id, CancellationToken cancellationToken)
        {
            try
            {
                CountryMaster _countrymaster = dbContext.CountryMaster.FirstOrDefault(x => x.ID == id);
                _countrymaster.UpdatedOn = DateTime.Now;
                _countrymaster.UpdatedBY = "2";
                _countrymaster.IsActive = 0;
                await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                return await Task.FromResult<string>("200");

            }
            catch (Exception ex)
            {
                return await Task.FromResult<string>(ex.Message);
                throw;
            }
        }

        public async Task<string> insert(CountryMaster _countryMaster, CancellationToken cancellationToken)
        {
            try
            {
                CountryMaster _countrymaster = new CountryMaster();
                _countrymaster.Code = _countryMaster.Code;
                _countrymaster.Name = _countryMaster.Name;
                _countrymaster.CreatedOn = DateTime.Now;
                _countrymaster.CreatedBY = "2";
                _countrymaster.IsActive = 1;
                dbContext.CountryMaster.Add(_countrymaster);
                await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                return await Task.FromResult<string>("200");
            }
            catch (Exception ex)
            {
                return await Task.FromResult<string>(ex.Message);

                throw;
            }
        }

        public async Task<CountryMaster> Search(int id, CancellationToken cancellationToken)
        {
            try
            {
                CountryMaster _countrymaster = dbContext.CountryMaster.FirstOrDefault(x => x.ID == id);
                return await Task.FromResult<CountryMaster>(_countrymaster);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<StateMaster> SearchState(int id, CancellationToken cancellationToken)
        {
            try
            {
                StateMaster stateMaster = dbContext.stateMasters.Include(x=>x.CountryMaster).FirstOrDefault(x => x.ID == id);
                return await Task.FromResult<StateMaster>(stateMaster);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        

        public async Task<List<CountryMaster>> SearchList(string log, int top, int skip, CancellationToken cancellationToken)
        {
            var list = dbContext.CountryMaster.Where(x => x.IsActive == 1).ToList();
            return await Task.FromResult<List<CountryMaster>>(list);
        }

        public async Task<string> update(CountryMaster _countryMaster, CancellationToken cancellationToken)
        {
            try
            {
                CountryMaster _countrymaster = dbContext.CountryMaster.FirstOrDefault(x => x.ID == _countryMaster.ID);
                _countrymaster.UpdatedOn = DateTime.Now;
                _countrymaster.UpdatedBY = "2";
                _countrymaster.Code = _countryMaster.Code;
                _countrymaster.Name = _countryMaster.Name;
                await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                return await Task.FromResult<string>("200");
            }
            catch (Exception ex)
            {
                return await Task.FromResult<string>(ex.Message);

                throw;
            }
        }
    }
}
