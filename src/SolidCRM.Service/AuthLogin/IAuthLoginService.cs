using SolidCRM.Models; 
using System.Threading.Tasks;
using SolidCRM.Repo;
namespace SolidCRM.Service
{
    public interface IAuthLoginService : IRepository<User>
    { 
        Task<(bool, User)> ValidateUserCredentialsAsync(string username, string password);
        Task<(bool, User)> ValidateUserCredentialsAsync(string username);
    }
}


