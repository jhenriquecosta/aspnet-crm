using SolidCRM.Models;
using SolidCRM.Repo;  
using SolidCRM.Data;
namespace SolidCRM.Service
{
    public class ProjectMileStoneService : Repository<ProjectMileStone>, IProjectMileStoneService
    {
        public ProjectMileStoneService(ApplicationContext dbContext) : base(dbContext) {}

        //you may write more service method here as per your need.
    }
}

