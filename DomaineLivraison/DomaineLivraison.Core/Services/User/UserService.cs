using DomaineLivraison.Core.Helpers;
using DomaineLivraison.Core.Models.User;
using DomaineLivraison.Core.Services.RequestProvider;
using System;
using System.Threading.Tasks;

namespace DomaineLivraison.Core.Services.User
{
    public class UserService : IUserService
    {
        private readonly IRequestProvider _requestProvider;

        public UserService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }

        public async Task<UserInfo> GetUserInfoAsync(string authToken)
        {
            var uri = UriHelper.CombineUri(GlobalSetting.Instance.UserInfoEndpoint);

            var userInfo = await _requestProvider.GetAsync<UserInfo>(uri, authToken);
            return userInfo;
        }
    }
}