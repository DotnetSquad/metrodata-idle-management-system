using API.Models;

namespace API.DataTransferObjects.Companies;

public class CompanyDtoGet
{
    public Guid Guid { get; set; }
    public string CompanyName { get; set; }
    public string Description { get; set; }
    public string Address { get; set; }

    // implicit operator
    public static implicit operator Company(CompanyDtoGet companyDtoGet)
    {
        return new Company
        {
            Guid = companyDtoGet.Guid,
            CompanyName = companyDtoGet.CompanyName,
            Description = companyDtoGet.Description,
            Address = companyDtoGet.Address
        };
    }

    // explicit operator
    public static explicit operator CompanyDtoGet(Company company)
    {
        return new CompanyDtoGet
        {
            Guid = company.Guid,
            CompanyName = company.CompanyName,
            Description = company.Description,
            Address = company.Address
        };
    }
}
