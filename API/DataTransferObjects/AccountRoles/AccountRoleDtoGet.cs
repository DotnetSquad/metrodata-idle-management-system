namespace API.DataTransferObjects.AccountRoles;

public class AccountRoleDtoGet
{
    public Guid Guid { get; set; }
    public Guid AccountGuid { get; set; }
    public Guid RoleGuid { get; set; }
}
