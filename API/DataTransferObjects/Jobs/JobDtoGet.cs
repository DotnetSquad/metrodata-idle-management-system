using API.DataTransferObjects.Accounts;
using API.Models;

namespace API.DataTransferObjects.Jobs;

public class JobDtoGet
{
    public Guid Guid { get; set; }
    public string JobName { get; set; }
    public string Description { get; set; }
    public Guid CompanyGuid { get; set; }

    // implicit operator
    public static implicit operator Job(JobDtoGet jobDtoGet)
    {
        return new Job
        {
            Guid = jobDtoGet.Guid,
            JobName = jobDtoGet.JobName,
            Description = jobDtoGet.Description,
            CompanyGuid = jobDtoGet.CompanyGuid
        };
    }

    // explicit operator
    public static explicit operator JobDtoGet(Job job)
    {
        return new JobDtoGet
        {
            Guid = job.Guid,
            JobName = job.JobName,
            Description = job.Description,
            CompanyGuid = job.CompanyGuid
        };
    }
}
