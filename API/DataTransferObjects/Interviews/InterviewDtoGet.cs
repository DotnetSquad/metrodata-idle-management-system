using API.Models;

namespace API.DataTransferObjects.Interviews;

public class InterviewDtoGet
{
    public Guid Guid { get; set; }
    public string Title { get; set; }
    public string? Link { get; set; }
    public DateTime InterviewDate { get; set; }
    public string Description { get; set; }

    // implicit operator
    public static implicit operator Interview(InterviewDtoGet interviewDtoGet)
    {
        return new Interview
        {
            Guid = interviewDtoGet.Guid,
            Title = interviewDtoGet.Title,
            Link = interviewDtoGet.Link,
            InterviewDate = interviewDtoGet.InterviewDate,
            Description = interviewDtoGet.Description
        };
    }

    // explicit operator
    public static explicit operator InterviewDtoGet(Interview interview)
    {
        return new InterviewDtoGet
        {
            Guid = interview.Guid,
            Title = interview.Title,
            Link = interview.Link,
            InterviewDate = interview.InterviewDate,
            Description = interview.Description
        };
    }
}
