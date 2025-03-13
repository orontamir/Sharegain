using DAL2Service.DAL.Sql;

namespace DAL2Service.Services
{
    public abstract class DalService
    {
        protected RepositoryBase Repository { get; set; }
        protected DalService(RepositoryBase repo)
        {
            Repository = repo;
        }
    }
}
