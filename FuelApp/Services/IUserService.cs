using System.Threading.Tasks;
using FuelApp.Models;

namespace FuelApp.Services
{
    public interface IUserService
    {
        Task<bool> Login(UserModel userModel);
        Task<bool> RegisterUser(UserModel userModel);
    }
}