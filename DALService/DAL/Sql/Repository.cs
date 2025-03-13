
using Microsoft.Extensions.Configuration;

namespace DALService.DAL.Sql
{
    public class Repository : RepositoryBase
    {
        public Repository(IConfiguration configuration) : base(configuration)
        {
        }
    }
}
