using API.Models;

namespace API.DataTransferObjects.Interviews;

public class InterviewDtoCreate
{
    public string Title { get; set; }
    public string? Link { get; set; }
    public DateTime InterviewDate { get; set; }
    public string Description { get; set; }

    // implicit operator
    public static implicit operator Interview(InterviewDtoCreate interviewDtoCreate)
    {
        return new Interview
        {
            Title = interviewDtoCreate.Title,
            Link = interviewDtoCreate.Link,
            InterviewDate = interviewDtoCreate.InterviewDate,
            Description = interviewDtoCreate.Description,
            CreatedDate = DateTime.UtcNow
        };
    }

    // explicit operator
    public static explicit operator InterviewDtoCreate(Interview interview)
    {
        return new InterviewDtoCreate
        {
            Title = interview.Title,
            Link = interview.Link,
            InterviewDate = interview.InterviewDate,
            Description = interview.Description
        };
    }
}
