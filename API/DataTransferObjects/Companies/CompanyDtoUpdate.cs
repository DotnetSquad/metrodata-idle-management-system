using API.Models;

namespace API.DataTransferObjects.Companies;

public class CompanyDtoUpdate
{
    public Guid Guid { get; set; }
    public string CompanyName { get; set; }
    public string Description { get; set; }
    public string Address { get; set; }

    // implicit operator
    public static implicit operator Company(CompanyDtoUpdate companyDtoUpdate)
    {
        return new Company
        {
            Guid = companyDtoUpdate.Guid,
            CompanyName = companyDtoUpdate.CompanyName,
            Description = companyDtoUpdate.Description,
            Address = companyDtoUpdate.Address,
            ModifiedDate = DateTime.UtcNow
        };
    }

    // explicit operator
    public static explicit operator CompanyDtoUpdate(Company company)
    {
        return new CompanyDtoUpdate
        {
            Guid = company.Guid,
            CompanyName = company.CompanyName,
            Description = company.Description,
            Address = company.Address
        };
    }
}
