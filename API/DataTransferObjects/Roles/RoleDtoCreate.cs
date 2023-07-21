namespace API.DataTransferObjects.Roles;

public class RoleDtoCreate
{
    public string Name { get; set; }

    public static implicit operator Models.Role(RoleDtoCreate roleDtoCreate)
    {
        return new()
        {
            Name = roleDtoCreate.Name
        };
    }

    public static explicit operator RoleDtoCreate(Models.Role account)
    {
        return new()
        {
            Name = account.Name
        };
    }
}
