﻿namespace Client.DataTransferObjects.Placements;

public class PlacementDtoUpdate
{
    public Guid Guid { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Guid EmployeeGuid { get; set; }
    public Guid CompanyGuid { get; set; }
}
