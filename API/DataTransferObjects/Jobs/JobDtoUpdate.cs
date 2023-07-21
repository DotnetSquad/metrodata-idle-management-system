using API.Models;

namespace API.DataTransferObjects.Jobs;

public class JobDtoUpdate
{
    public Guid Guid { get; set; }
    public string JobName { get; set; }
    public string Description { get; set; }
    public Guid CompanyGuid { get; set; }

    // implicit operator
    public static implicit operator Job(JobDtoUpdate jobDtoUpdate)
    {
        return new Job
        {
            Guid = jobDtoUpdate.Guid,
            JobName = jobDtoUpdate.JobName,
            Description = jobDtoUpdate.Description,
            CompanyGuid = jobDtoUpdate.CompanyGuid
        };
    }

    // explicit operator
    public static explicit operator JobDtoUpdate(Job job)
    {
        return new JobDtoUpdate
        {
            Guid = job.Guid,
            JobName = job.JobName,
            Description = job.Description,
            CompanyGuid = job.CompanyGuid
        };
    }
}
