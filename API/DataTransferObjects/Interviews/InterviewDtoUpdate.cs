using API.Models;

namespace API.DataTransferObjects.Interviews;

public class InterviewDtoUpdate
{
    public Guid Guid { get; set; }
    public string Title { get; set; }
    public string? Link { get; set; }
    public DateTime InterviewDate { get; set; }
    public string Description { get; set; }
    public Guid JobGuid { get; set; }

    // implicit operator
    public static implicit operator Interview(InterviewDtoUpdate interviewDtoUpdate)
    {
        return new Interview
        {
            Guid = interviewDtoUpdate.Guid,
            Title = interviewDtoUpdate.Title,
            Link = interviewDtoUpdate.Link,
            InterviewDate = interviewDtoUpdate.InterviewDate,
            Description = interviewDtoUpdate.Description,
            JobGuid = interviewDtoUpdate.JobGuid,
            ModifiedDate = DateTime.UtcNow
        };
    }

    // explicit operator
    public static explicit operator InterviewDtoUpdate(Interview interview)
    {
        return new InterviewDtoUpdate
        {
            Guid = interview.Guid,
            Title = interview.Title,
            Link = interview.Link,
            InterviewDate = interview.InterviewDate,
            Description = interview.Description,
            JobGuid = interview.JobGuid
        };
    }
}
