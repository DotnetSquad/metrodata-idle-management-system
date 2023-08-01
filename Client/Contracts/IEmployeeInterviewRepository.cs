using Client.DataTransferObjects.EmployeeInterviews;

namespace Client.Contracts;

public interface IEmployeeInterviewRepository : IBaseRepository<EmployeeInterviewDtoGet, Guid>
{
}
