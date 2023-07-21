﻿using API.Utilities.Enums;

namespace API.DataTransferObjects.Interviews;

public class InterviewDtoUpdate
{
    public string JobName { get; set; }
    public string? Link { get; set; }
    public DateTime InterviewDate { get; set; }
    public string Description { get; set; }
    public StatusInterviewEnum? StatusInterview { get; set; }
    public Guid JobGuid { get; set; }
}