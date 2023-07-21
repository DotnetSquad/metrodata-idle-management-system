namespace API.DataTransferObjects.AccountRole;

public class AccountRoleDtoUpdate
{
    public Guid Guid { get; set; }
    public Guid AccountGuid { get; set; }
    public Guid RoleGuid { get; set; }
}
