﻿using Client.Contracts;
using Client.DataTransferObjects.Employees;
using Client.Utilities.Handlers;
using Newtonsoft.Json;

namespace Client.Repositories;

public class EmployeeRepository : BaseRepository<EmployeeDtoGet, Guid>, IEmployeeRepository
{
    public EmployeeRepository(string request = "Employee/") : base(request)
    {
    }

    public async Task<ResponseHandler<IEnumerable<EmployeeDtoGet>>> GetByRole(Guid guid)
    {
        ResponseHandler<IEnumerable<EmployeeDtoGet>> entity = null;
        using (var response = await HttpClient.GetAsync(Request + "GetByRole/" + guid))
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            entity = JsonConvert.DeserializeObject<ResponseHandler<IEnumerable<EmployeeDtoGet>>>(apiResponse);
        }

        return entity;
    }


    public async Task<ResponseHandler<IEnumerable<EmployeeDtoGet>>> GetExcludeRole(Guid guid)
    {
        ResponseHandler<IEnumerable<EmployeeDtoGet>> entity = null;
        using (var response = await HttpClient.GetAsync(Request + "GetExcludeRole/" + guid))
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            entity = JsonConvert.DeserializeObject<ResponseHandler<IEnumerable<EmployeeDtoGet>>>(apiResponse);
        }

        return entity;
    }

    public async Task<ResponseHandler<IEnumerable<EmployeeDtoGet>>> GetByProject(Guid guid)
    {
        ResponseHandler<IEnumerable<EmployeeDtoGet>> entity = null;
        using (var response = await HttpClient.GetAsync(Request + "GetByProject/" + guid))
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            entity = JsonConvert.DeserializeObject<ResponseHandler<IEnumerable<EmployeeDtoGet>>>(apiResponse);
        }

        return entity;
    }

    public async Task<ResponseHandler<IEnumerable<EmployeeDtoGet>>> GetExcludeProject(Guid guid)
    {
        ResponseHandler<IEnumerable<EmployeeDtoGet>> entity = null;
        using (var response = await HttpClient.GetAsync(Request + "GetExcludeProject/" + guid))
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            entity = JsonConvert.DeserializeObject<ResponseHandler<IEnumerable<EmployeeDtoGet>>>(apiResponse);
        }

        return entity;
    }

    public async Task<ResponseHandler<IEnumerable<EmployeeDtoGet>>> GetEmployeeByJob(Guid guid)
    {
        ResponseHandler<IEnumerable<EmployeeDtoGet>> entity = null;
        using (var response = await HttpClient.GetAsync(Request + "GetByJob/" + guid))
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            entity = JsonConvert.DeserializeObject<ResponseHandler<IEnumerable<EmployeeDtoGet>>>(apiResponse);
        }

        return entity;
    }

    public async Task<ResponseHandler<IEnumerable<EmployeeDtoGet>>> GetExcludeJob(Guid guid)
    {
        ResponseHandler<IEnumerable<EmployeeDtoGet>> entity = null;
        using (var response = await HttpClient.GetAsync(Request + "GetExcludeJob/" + guid))
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            entity = JsonConvert.DeserializeObject<ResponseHandler<IEnumerable<EmployeeDtoGet>>>(apiResponse);
        }

        return entity;
    }
}
