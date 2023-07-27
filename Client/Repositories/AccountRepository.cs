﻿using Client.Contracts;
using Client.DataTransferObjects.Accounts;
using Client.Utilities.Handlers;
using Newtonsoft.Json;
using System.Text;

namespace Client.Repositories;

public class AccountRepository : BaseRepository<AccountDtoGet, Guid>, IAccountRepository
{
    public AccountRepository(string request = "Account/") : base(request)
    {
    }

    public async Task<ResponseHandler<AccountRepository>> Register(AccountDtoRegister entity)
    {
        ResponseHandler<AccountRepository> entityVM = null;
        StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
        using (var response = _httpClient.PostAsync(_request + "register", content).Result)
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            entityVM = JsonConvert.DeserializeObject<ResponseHandler<AccountRepository>>(apiResponse);
        }
        return entityVM;
    }
}