using System.Data;
using System.Data.Entity;
using System.Web;
using GraduationHub.Web.Data;
using GraduationHub.Web.Infrastructure.Tasks;

namespace GraduationHub.Web.Infrastructure
{
    public class TransactionPerRequest :
        IRunOnEachRequest, IRunOnError, IRunAfterEachRequest
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly HttpContextBase _httpContext;

        public TransactionPerRequest(ApplicationDbContext dbContext,
            HttpContextBase httpContext)
        {
            _dbContext = dbContext;
            _httpContext = httpContext;
        }

        void IRunAfterEachRequest.Execute()
        {
            var transaction = (DbContextTransaction) _httpContext.Items["_Transaction"];

            if (_httpContext.Items["_Error"] != null)
            {
                transaction.Rollback();
            }
            else
            {
                transaction.Commit();
            }
        }

        void IRunOnEachRequest.Execute()
        {
            DbContextTransaction tran = _dbContext.Database.BeginTransaction(IsolationLevel.ReadCommitted);

            _httpContext.Items["_Transaction"] = tran;
        }

        void IRunOnError.Execute()
        {
            _httpContext.Items["_Error"] = true;
        }
    }
}