using Client.Contracts;
using Client.DataTransferObjects.EmployeeJobs;
using Client.Utilities.Handlers;
using Newtonsoft.Json;

namespace Client.Repositories;

public class EmployeeJobRepository : BaseRepository<EmployeeJobDtoGet, Guid>, IEmployeeJobRepository
{
    public EmployeeJobRepository(string request = "EmployeeJob/") : base(request)
    {
    }

    public async Task<ResponseHandler<IEnumerable<EmployeeJobDtoGet>>> GetByJob(Guid guid)
    {
        ResponseHandler<IEnumerable<EmployeeJobDtoGet>> entity = null;
        using (var response = await HttpClient.GetAsync(Request + "GetByJob/" + guid))
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            entity = JsonConvert.DeserializeObject<ResponseHandler<IEnumerable<EmployeeJobDtoGet>>>(apiResponse);
        }

        return entity;
    }
}