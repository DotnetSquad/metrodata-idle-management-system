using Client.DataTransferObjects.Dashboards;
using Client.Utilities.Handlers;

namespace Client.Contracts;

public interface IDashboardRepository : IBaseRepository<DashboardsDtoGetStatus, Guid>
{
    Task<ResponseHandler<DashboardsDtoGetStatus>> GetStatisticEmployee();
    Task<ResponseHandler<DashboardDtoGetInterviewStatus>> GetStatisticInterviewStatus();
    public Task<ResponseHandler<IEnumerable<DashboardDtoGetClient>>> GetTop5Clients();
}
