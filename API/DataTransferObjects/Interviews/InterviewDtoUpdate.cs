using API.Models;
using API.Utilities.Enums;

namespace API.DataTransferObjects.Interviews;

public class InterviewDtoUpdate
{
    public Guid Guid { get; set; }
    public string JobName { get; set; }
    public string? Link { get; set; }
    public DateTime InterviewDate { get; set; }
    public string Description { get; set; }
    public StatusInterviewEnum? StatusInterview { get; set; }
    public Guid JobGuid { get; set; }

    // implicit operator
    public static implicit operator Interview(InterviewDtoUpdate interviewDtoUpdate)
    {
        return new Interview
        {
            Guid = interviewDtoUpdate.Guid,
            JobName = interviewDtoUpdate.JobName,
            Link = interviewDtoUpdate.Link,
            InterviewDate = interviewDtoUpdate.InterviewDate,
            Description = interviewDtoUpdate.Description,
            StatusInterview = interviewDtoUpdate.StatusInterview,
            JobGuid = interviewDtoUpdate.JobGuid
        };
    }

    // explicit operator
    public static explicit operator InterviewDtoUpdate(Interview interview)
    {
        return new InterviewDtoUpdate
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
