using Client.DataTransferObjects.Companies;

namespace Client.Contracts;

public interface ICompanyRepository : IBaseRepository<CompanyDtoGet, Guid>
{
}