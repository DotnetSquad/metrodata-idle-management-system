namespace API.DataTransferObjects.Roles;

public class RoleDtoGet
{
    public Guid Guid { get; set; }
    public string Name { get; set; }

    public static implicit operator Models.Role(RoleDtoGet roleDtoGet)
    {
        return new()
        {
            Guid = roleDtoGet.Guid,
            Name = roleDtoGet.Name
        };
    }

    public static explicit operator RoleDtoGet(Models.Role role)
    {
        return new()
        {
            Guid = role.Guid,
            Name = role.Name
        };
    }
}
