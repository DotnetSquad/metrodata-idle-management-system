using API.Models;

namespace API.DataTransferObjects.Jobs;

public class JobDtoCreate
{
    public string JobName { get; set; }
    public string Description { get; set; }
    public Guid CompanyGuid { get; set; }

    // implicit operator
    public static implicit operator Job(JobDtoCreate jobDtoCreate)
    {
        return new Job
        {
            JobName = jobDtoCreate.JobName,
            Description = jobDtoCreate.Description,
            CompanyGuid = jobDtoCreate.CompanyGuid
        };
    }

    // explicit operator
    public static explicit operator JobDtoCreate(Job job)
    {
        return new JobDtoCreate
        {
            JobName = job.JobName,
            Description = job.Description,
            CompanyGuid = job.CompanyGuid
        };
    }
}
