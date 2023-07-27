using Client.Contracts;
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

    public async Task<ResponseHandler<string>> Login(AccountDtoLogin accountDtoLogin)
    {
        ResponseHandler<string> entityVM = null!;
        StringContent content = new StringContent(JsonConvert.SerializeObject(accountDtoLogin), Encoding.UTF8,
            "application/json");
        using (var response = HttpClient.PostAsync(Request + "Login", content).Result)
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            entityVM = JsonConvert.DeserializeObject<ResponseHandler<string>>(apiResponse);
        }

        return entityVM;
    }

    public async Task<ResponseHandler<AccountDtoRegister>> Register(AccountDtoRegister entity)
    {
        ResponseHandler<AccountDtoRegister> entityVM = null;
        StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
        using (var response = HttpClient.PostAsync(Request + "register", content).Result)
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            entityVM = JsonConvert.DeserializeObject<ResponseHandler<AccountDtoRegister>>(apiResponse);
        }

        return entityVM;
    }
}
