using SolidCRM.Models;
using SolidCRM.Repo;  
using SolidCRM.Data;
namespace SolidCRM.Service
{
    public class GeneralSettingService : Repository<GeneralSetting>, IGeneralSettingService
    {
        public GeneralSettingService(ApplicationContext dbContext) : base(dbContext) {}

        //you may write more service method here as per your need.
    }
}

