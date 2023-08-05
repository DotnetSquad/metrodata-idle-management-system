using API.Models;

namespace API.DataTransferObjects.Profiles;

public class ProfileDtoUpdate
{
    public string PhotoProfile { get; set; }
    public Guid Guid { get; set; }
    public string Skills { get; set; }
    public string Linkedin { get; set; }
    public string Resume { get; set; }
    
    public static implicit operator Profile(ProfileDtoUpdate profileDtoUpdate)
    {
        return new Profile
        {
            Guid = profileDtoUpdate.Guid,
            PhotoProfile = profileDtoUpdate.PhotoProfile,
            Skills = profileDtoUpdate.Skills,
            Linkedin = profileDtoUpdate.Linkedin,
            Resume = profileDtoUpdate.Resume,
            ModifiedDate = DateTime.UtcNow
        };
    }
    
    // explicit operator
    public static explicit operator ProfileDtoUpdate(Profile profile)
    {
        return new ProfileDtoUpdate
        {
            Guid = profile.Guid,
            PhotoProfile = profile.PhotoProfile,
            Skills = profile.Skills,
            Linkedin = profile.Linkedin,
            Resume = profile.Resume
        };
    }
}
