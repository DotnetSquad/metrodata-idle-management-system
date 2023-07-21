using API.Models;
using API.Utilities.Enums;

namespace API.DataTransferObjects.Interviews;

public class InterviewDtoGet
{
    public Guid Guid { get; set; }
    public string JobName { get; set; }
    public string? Link { get; set; }
    public DateTime InterviewDate { get; set; }
    public string Description { get; set; }
    public StatusInterviewEnum? StatusInterview { get; set; }
    public Guid JobGuid { get; set; }
    
    // implicit operator
    public static implicit operator Interview(InterviewDtoGet interviewDtoGet)
    {
        return new Interview
        {
            Guid = interviewDtoGet.Guid,
            JobName = interviewDtoGet.JobName,
            Link = interviewDtoGet.Link,
            InterviewDate = interviewDtoGet.InterviewDate,
            Description = interviewDtoGet.Description,
            StatusInterview = interviewDtoGet.StatusInterview,
            JobGuid = interviewDtoGet.JobGuid
        };
    }
    
    // explicit operator
    public static explicit operator InterviewDtoGet(Interview interview)
    {
        return new InterviewDtoGet
        {
            Guid = interview.Guid,
            JobName = interview.JobName,
            Link = interview.Link,
            InterviewDate = interview.InterviewDate,
            Description = interview.Description,
            StatusInterview = interview.StatusInterview,
            JobGuid = interview.JobGuid
        };
    }
}
