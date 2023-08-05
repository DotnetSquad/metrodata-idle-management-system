using API.Models;

namespace API.DataTransferObjects.Profiles;

public class ProfileDtoGet
{
    public string PhotoProfile { get; set; }
    public Guid Guid { get; set; }
    public string Skills { get; set; }
    public string Linkedin { get; set; }
    public string Resume { get; set; }
    
    public static implicit operator Profile(ProfileDtoGet profileDtoGet)
    {
        return new Profile
        {
            Guid = profileDtoGet.Guid,
            PhotoProfile = profileDtoGet.PhotoProfile,
            Skills = profileDtoGet.Skills,
            Linkedin = profileDtoGet.Linkedin,
            Resume = profileDtoGet.Resume
        };
    }
    
    // explicit operator
    public static explicit operator ProfileDtoGet(Profile profile)
    {
        return new ProfileDtoGet
        {
            Guid = profile.Guid,
            PhotoProfile = profile.PhotoProfile,
            Skills = profile.Skills,
            Linkedin = profile.Linkedin,
            Resume = profile.Resume
        };
    }
}
