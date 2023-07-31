using Client.Contracts;
using Client.DataTransferObjects.Dashboards;
using Client.Utilities.Handlers;
using Newtonsoft.Json;

namespace Client.Repositories;

public class DashboardRepository : BaseRepository<DashboardsDtoGetStatus, Guid>, IDashboardRepository
{
    public DashboardRepository(string request = "dashboard") : base(request)
    {
    }

    public async Task<ResponseHandler<DashboardsDtoGetStatus>> GetStatisticEmployee()
    {
        ResponseHandler<DashboardsDtoGetStatus> entity = null!;
        using (var response = await HttpClient.GetAsync(Request))
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            entity = JsonConvert.DeserializeObject<ResponseHandler<DashboardsDtoGetStatus>>(apiResponse);
        }
        return entity;
    }
}
