using Client.DataTransferObjects.Profiles;

namespace Client.Contracts;

public interface IProfileRepository : IBaseRepository<ProfileDtoGet, Guid>
{
}
