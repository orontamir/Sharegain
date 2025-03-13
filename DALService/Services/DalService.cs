using DALService.DAL.Sql;

namespace DALService.Services
{
    public abstract class DalService
    {
        protected RepositoryBase Repository { get; set; }
        protected DalService(RepositoryBase repo) {
            Repository = repo;
        }
    }
}
