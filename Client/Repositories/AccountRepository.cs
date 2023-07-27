using Client.Contracts;
using Client.DataTransferObjects.Accounts;
using Client.Utilities.Handlers;
using Newtonsoft.Json;
using System.Text;

namespace Client.Repositories;

public class AccountRepository : BaseRepository<AccountDtoRegister, string>, IAccountRepository
{
    private readonly HttpClient httpClient;
    private readonly string request;

    public AccountRepository(string request = "Account/") : base(request)
    {
        httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:7009/api/")
        };
        this.request = request;
    }

    public async Task<ResponseHandler<string>> Login(AccountDtoLogin accountDtoLogin)
    {
        ResponseHandler<string> entityVM = null;
        StringContent content = new StringContent(JsonConvert.SerializeObject(accountDtoLogin), Encoding.UTF8, "application/json");
        using (var response = httpClient.PostAsync(request + "Login", content).Result)
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            entityVM = JsonConvert.DeserializeObject<ResponseHandler<string>>(apiResponse);
        }
        return entityVM;
    }

}
