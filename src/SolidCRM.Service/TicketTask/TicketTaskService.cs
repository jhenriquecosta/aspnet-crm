using SolidCRM.Models;
using SolidCRM.Repo;  
using SolidCRM.Data;
namespace SolidCRM.Service
{
    public class TicketTaskService : Repository<TicketTask>, ITicketTaskService
    {
        public TicketTaskService(ApplicationContext dbContext) : base(dbContext) {}

        //you may write more service method here as per your need.
    }
}

