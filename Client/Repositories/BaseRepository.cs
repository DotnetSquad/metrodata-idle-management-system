using Client.Contracts;
using Client.Utilities.Handlers;
using Newtonsoft.Json;
using System.Text;

namespace Client.Repositories;

public class BaseRepository<TEntity, TId> : IBaseRepository<TEntity, TId>
    where TEntity : class
{
    private readonly string _request;
    private readonly HttpClient _httpClient;
    private readonly IHttpContextAccessor contextAccessor;

    public BaseRepository(string request)
    {
        _request = request;
        contextAccessor = new HttpContextAccessor();
        _httpClient = new HttpClient()
        {
            BaseAddress = new Uri("https://localhost:7009/api/")
        };
        /*_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", contextAccessor.HttpContext?.Session.GetString("JWToken"));*/
    }

    public async Task<ResponseHandler<IEnumerable<TEntity>>> Get()
    {
        ResponseHandler<IEnumerable<TEntity>> entity = null;
        using (var response = await _httpClient.GetAsync(_request))
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            entity = JsonConvert.DeserializeObject<ResponseHandler<IEnumerable<TEntity>>>(apiResponse);
        }
        return entity;
    }

    public async Task<ResponseHandler<TEntity>> Get(TId id)
    {
        ResponseHandler<TEntity> entity = null;
        using (var response = await _httpClient.GetAsync(_request + id))
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            entity = JsonConvert.DeserializeObject<ResponseHandler<TEntity>>(apiResponse);
        }
        return entity;
    }

    public async Task<ResponseHandler<TEntity>> Post(TEntity entity)
    {
        ResponseHandler<TEntity> entityDto = null;
        StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
        using (var response = _httpClient.PostAsync(_request, content).Result)
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            entityDto = JsonConvert.DeserializeObject<ResponseHandler<TEntity>>(apiResponse);
        }
        return entityDto;
    }

    public async Task<ResponseHandler<TEntity>> Put(TId id, TEntity entity)
    {
        ResponseHandler<TEntity> entityDto = null;
        StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
        using (var response = _httpClient.PutAsync(_request, content).Result)
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            entityDto = JsonConvert.DeserializeObject<ResponseHandler<TEntity>>(apiResponse);
        }
        return entityDto;
    }

    public async Task<ResponseHandler<TEntity>> Delete(TId id)
    {
        ResponseHandler<TEntity> entityDto = null;
        StringContent content = new StringContent(JsonConvert.SerializeObject(id), Encoding.UTF8, "application/json");
        using (var response = _httpClient.DeleteAsync(_request + "?guid=" + id).Result)
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            entityDto = JsonConvert.DeserializeObject<ResponseHandler<TEntity>>(apiResponse);
        }
        return entityDto;
    }
}
