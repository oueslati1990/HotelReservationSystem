using AccountApi.Models;
using System.Threading.Tasks;

namespace AccountApi.Interfaces
{
    public interface IAuthService
    {
        Task<AuthModel> RegisterAsync(RegisterModel model);
        Task<AuthModel> GetTokenAsync(TokenRequestModel model);
    }
}
