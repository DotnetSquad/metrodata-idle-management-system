﻿namespace Client.DataTransferObjects.AccountRoles;

public class AccountRoleDtoUpdate
{
    public Guid Guid { get; set; }
    public Guid AccountGuid { get; set; }
    public Guid RoleGuid { get; set; }
}
