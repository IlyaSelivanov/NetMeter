using Domain.ValueObjects;
using System;
using System.Threading.Tasks;
using WebUI.Services;

namespace WebUI.Repository
{
    public class AccountsRepository : IAccountsRepository
    {
        private readonly IHttpService _httpService;
        private readonly string _baseUrl = "api/accounts";

        public AccountsRepository(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<UserToken> Register(UserInfo userInfo)
        {
            var httpResponse = await _httpService.Post<UserInfo, UserToken>($"{_baseUrl}/create", userInfo);
            if (!httpResponse.Success)
                throw new ApplicationException(await httpResponse.GetBody());
            else
                return httpResponse.Response;
        }

        public async Task<UserToken> Login(UserInfo userInfo)
        {
            var httpResponse = await _httpService.Post<UserInfo, UserToken>($"{_baseUrl}/login", userInfo);
            if (!httpResponse.Success)
                throw new ApplicationException(await httpResponse.GetBody());
            else
                return httpResponse.Response;
        }
    }
}
