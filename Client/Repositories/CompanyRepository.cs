using Client.Contracts;
using Client.DataTransferObjects.Companies;

namespace Client.Repositories;

public class CompanyRepository : BaseRepository<CompanyDtoGet, Guid>, ICompanyRepository
{
    public CompanyRepository(string request = "Company/") : base(request)
    {
    }
}