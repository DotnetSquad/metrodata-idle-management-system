using Client.Contracts;
using Client.Utilities.Handlers;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Client.Repositories;

public class BaseRepository<TEntity, TId> : IBaseRepository<TEntity, TId>
    where TEntity : class
{
    protected readonly string Request;
    protected readonly HttpClient HttpClient;
    private readonly IHttpContextAccessor _contextAccessor;

    public BaseRepository(string request)
    {
        Request = request;
        _contextAccessor = new HttpContextAccessor();
        HttpClient = new HttpClient()
        {
            BaseAddress = new Uri("https://localhost:7009/api/")
        };
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", _contextAccessor.HttpContext?.Session.GetString("JWTToken"));
    }

    public async Task<ResponseHandler<IEnumerable<TEntity>>> Get()
    {
        ResponseHandler<IEnumerable<TEntity>> entity = null;
        using (var response = await HttpClient.GetAsync(Request))
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            entity = JsonConvert.DeserializeObject<ResponseHandler<IEnumerable<TEntity>>>(apiResponse);
        }
        return entity;
    }

    public async Task<ResponseHandler<TEntity>> Get(TId id)
    {
        ResponseHandler<TEntity> entity = null!;
        using (var response = await HttpClient.GetAsync(Request + id))
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            entity = JsonConvert.DeserializeObject<ResponseHandler<TEntity>>(apiResponse);
        }
        return entity;
    }

    public async Task<ResponseHandler<TEntity>> Post(TEntity entity)
    {
        ResponseHandler<TEntity> entityDto = null!;
        StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
        using (var response = HttpClient.PostAsync(Request, content).Result)
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            entityDto = JsonConvert.DeserializeObject<ResponseHandler<TEntity>>(apiResponse);
        }
        return entityDto;
    }

    public async Task<ResponseHandler<TEntity>> Put(TId id, TEntity entity)
    {
        ResponseHandler<TEntity> entityDto = null!;
        StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
        using (var response = HttpClient.PutAsync(Request, content).Result)
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            entityDto = JsonConvert.DeserializeObject<ResponseHandler<TEntity>>(apiResponse);
        }
        return entityDto;
    }

    public async Task<ResponseHandler<TEntity>> Delete(TId id)
    {
        ResponseHandler<TEntity> entityDto = null!;
        StringContent content = new StringContent(JsonConvert.SerializeObject(id), Encoding.UTF8, "application/json");
        using (var response = HttpClient.DeleteAsync(Request + id).Result)
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            entityDto = JsonConvert.DeserializeObject<ResponseHandler<TEntity>>(apiResponse);
        }
        return entityDto;
    }
}
