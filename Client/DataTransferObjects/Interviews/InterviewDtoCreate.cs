namespace Client.DataTransferObjects.Interviews;

public class InterviewDtoCreate
{
    public string Title { get; set; }
    public string? Link { get; set; }
    public DateTime InterviewDate { get; set; }
    public string Description { get; set; }
}
