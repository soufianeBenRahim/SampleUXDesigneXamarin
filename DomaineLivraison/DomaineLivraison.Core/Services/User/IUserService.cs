using DomaineLivraison.Core.Models.User;
using System.Threading.Tasks;

namespace DomaineLivraison.Core.Services.User
{
    public interface IUserService
    {
        Task<UserInfo> GetUserInfoAsync(string authToken);
    }
}
