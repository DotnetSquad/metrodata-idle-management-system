using Client.Contracts;
using Client.DataTransferObjects.EmployeeProjects;
using Client.DataTransferObjects.Employees;
using Client.Utilities.Handlers;
using Newtonsoft.Json;

namespace Client.Repositories;

public class EmployeeProjectRepository : BaseRepository<EmployeeProjectDtoGet, Guid>, IEmployeeProjectRepository
{
    public EmployeeProjectRepository(string request = "EmployeeProject/") : base(request)
    {
    }
    
    public async Task<ResponseHandler<IEnumerable<EmployeeProjectDtoGet>>> GetByProject(Guid guid)
    {
        ResponseHandler<IEnumerable<EmployeeProjectDtoGet>> entity = null;
        using (var response = await HttpClient.GetAsync(Request + "GetByProject/" + guid))
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            entity = JsonConvert.DeserializeObject<ResponseHandler<IEnumerable<EmployeeProjectDtoGet>>>(apiResponse);
        }

        return entity;
    }
}
