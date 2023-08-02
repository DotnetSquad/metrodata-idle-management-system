namespace Client.DataTransferObjects.Interviews;

public class InterviewDtoUpdate
{
    public Guid Guid { get; set; }
    public string Title { get; set; }
    public string? Link { get; set; }
    public DateTime InterviewDate { get; set; }
    public string Description { get; set; }
    public Guid JobGuid { get; set; }
}
