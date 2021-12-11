using Base.Exceptions;
using Framework.DomainModels.Logging;
using System;
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
        public async Task<string> SearchEmployees(string log, int top, int skip, CancellationToken cancellationToken)
        {
            try
            {
                LogTable _log = new LogTable();
                _log.Code = 12312312;
                _log.CreateOn = DateTime.Now;
                _log.CreatedBY = "2";
                _log.logstatus = 1;
                _log.LogString = "Success";
                dbContext.LogTable.Add(_log);
                await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            }
            catch (Exception ex)
            {
                return await Task.FromResult<string>(ex.Message);
 
                throw;
            }


            return await Task.FromResult<string>("200");
        }
    }
}
