using API.Models;

namespace API.DataTransferObjects.Profiles;

public class ProfileDtoCreate
{
    public string PhotoProfile { get; set; }
    public string Skills { get; set; }
    public string Linkedin { get; set; }
    public string Resume { get; set; }
    
    // implicit operator
    public static implicit operator Profile(ProfileDtoCreate profileDtoCreate)
    {
        return new Profile
        {
            Skills = profileDtoCreate.Skills,
            PhotoProfile = profileDtoCreate.PhotoProfile,
            Linkedin = profileDtoCreate.Linkedin,
            Resume = profileDtoCreate.Resume,
            CreatedDate = DateTime.UtcNow
        };
    }
    
    // explicit operator
    public static explicit operator ProfileDtoCreate(Profile profile)
    {
        return new ProfileDtoCreate
        {
            Skills = profile.Skills,
            PhotoProfile = profile.PhotoProfile,
            Linkedin = profile.Linkedin,
            Resume = profile.Resume
        };
    }
}
