namespace API.DataTransferObjects.Roles;

public class RoleDtoUpdate
{
    public Guid Guid { get; set; }
    public string Name { get; set; }

    public static implicit operator Models.Role(RoleDtoUpdate roleDtoUpdate)
    {
        return new()
        {
            Guid = roleDtoUpdate.Guid,
            Name = roleDtoUpdate.Name
        };
    }

    public static explicit operator RoleDtoUpdate(Models.Role role)
    {
        return new()
        {
            Guid = role.Guid,
            Name = role.Name
        };
    }
}
