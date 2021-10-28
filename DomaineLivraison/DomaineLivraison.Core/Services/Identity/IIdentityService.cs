﻿using DomaineLivraison.Core.Models.Token;
using System.Threading.Tasks;

namespace DomaineLivraison.Core.Services.Identity
{
    public interface IIdentityService
    {
        string CreateAuthorizationRequest();
        string CreateLogoutRequest(string token);
        Task<UserToken> GetTokenAsync(string code);
    }
}