using System.Threading.Tasks;
using FuelApp.Models;

namespace FuelApp.Services
{
    public interface IUserService
    {
        Task<UserModel> Login(UserModel userModel);
        Task<bool> RegisterUser(UserModel userModel);
        Task<bool> IsEmailRegistered(UserModel userModel);
        Task<bool> ConfirmEmail(string id);
    }
}