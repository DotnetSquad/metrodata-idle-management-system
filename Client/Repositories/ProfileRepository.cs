using System.Text;
using Client.Contracts;
using Client.DataTransferObjects.Profiles;
using Client.Utilities.Handlers;
using Newtonsoft.Json;

namespace Client.Repositories;

public class ProfileRepository : BaseRepository<ProfileDtoGet, Guid>, IProfileRepository
{
    public ProfileRepository(string request = "Profile/") : base(request)
    {
    }
}
