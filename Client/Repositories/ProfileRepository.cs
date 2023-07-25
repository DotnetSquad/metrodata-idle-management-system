using Client.Contracts;
using Client.DataTransferObjects.Profiles;

namespace Client.Repositories;

public class ProfileRepository : BaseRepository<ProfileDtoGet, Guid>, IProfileRepository
{
    public ProfileRepository(string request = "Profile/") : base(request)
    {
    }
}
