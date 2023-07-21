using API.Models;

namespace API.DataTransferObjects.Profiles;

public class ProfileDtoCreate
{
    public string Skills { get; set; }
    public string Linkedin { get; set; }
    public string Resume { get; set; }
    
    // implicit operator
    public static implicit operator Profile(ProfileDtoCreate profileDtoCreate)
    {
        return new Profile
        {
            Skills = profileDtoCreate.Skills,
            Linkedin = profileDtoCreate.Linkedin,
            Resume = profileDtoCreate.Resume
        };
    }
    
    // explicit operator
    public static explicit operator ProfileDtoCreate(Profile profile)
    {
        return new ProfileDtoCreate
        {
            Skills = profile.Skills,
            Linkedin = profile.Linkedin,
            Resume = profile.Resume
        };
    }
}
