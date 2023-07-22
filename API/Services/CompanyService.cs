using API.Contracts;
using API.DataTransferObjects.Companies;

namespace API.Services;

public class CompanyService
{
    private ICompanyRepository _companyRepository;

    public CompanyService(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }

    public IEnumerable<CompanyDtoGet> Get()
    {
        var companies = _companyRepository.GetAll().ToList();
        if (!companies.Any()) return Enumerable.Empty<CompanyDtoGet>();
        List<CompanyDtoGet> companyDtoGets = new();

        foreach (var company in companies)
        {
            companyDtoGets.Add((CompanyDtoGet)company);
        }

        return companyDtoGets;
    }

    public CompanyDtoGet? Get(Guid guid)
    {
        var company = _companyRepository.GetByGuid(guid);
        if (company is null) return null!;

        return (CompanyDtoGet)company;
    }

    public CompanyDtoCreate? Create(CompanyDtoCreate companyDtoCreate)
    {
        var companyCreated = _companyRepository.Create(companyDtoCreate);
        if (companyCreated is null) return null!;

        return (CompanyDtoCreate)companyCreated;
    }

    public int Update(CompanyDtoUpdate companyDtoUpdate)
    {
        var company = _companyRepository.GetByGuid(companyDtoUpdate.Guid);
        if (company is null) return -1;

        var companyUpdated = _companyRepository.Update(companyDtoUpdate);
        return !companyUpdated ? 0 : 1;
    }

    public int Delete(Guid guid)
    {
        var company = _companyRepository.GetByGuid(guid);
        if (company is null) return -1;

        var companyDeleted = _companyRepository.Delete(company);
        return !companyDeleted ? 0 : 1;
    }
}
