using Client.Utilities.Enums;

namespace Client.DataTransferObjects.Interviews;

public class InterviewDtoCreate
{
    public string Title { get; set; }
    public string? Link { get; set; }
    public DateTime InterviewDate { get; set; }
    public string Description { get; set; }
    public StatusInterviewEnum? StatusInterview { get; set; }
    public Guid JobGuid { get; set; }
}
