using SolidCRM.Models;
using SolidCRM.Repo;  
using SolidCRM.Data;
using System.Linq;

namespace SolidCRM.Service
{
    public class CompanyService : Repository<Company>, ICompanyService
    {
        public CompanyService(ApplicationContext dbContext) : base(dbContext) {}
         
        //you may write more service method here as per your need.
    }
}

