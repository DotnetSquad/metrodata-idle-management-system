namespace API.DataTransferObjects.Role;

public class RoleDtoCreate
{
    public string Name { get; set; }

    public static implicit operator RoleDtoGet(RoleDtoCreate roleDtoCreate)
    {
        return new()
        {
            Name = roleDtoCreate.Name
        };
    }

    public static explicit operator RoleDtoUpdate(RoleDtoCreate roleDtoCreate)
    {
        return new()
        {
            Name = roleDtoCreate.Name
        };
    }
}
