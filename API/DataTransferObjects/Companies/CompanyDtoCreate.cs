using API.Models;

namespace API.DataTransferObjects.Companies;

public class CompanyDtoCreate
{
    public string CompanyName { get; set; }
    public string Description { get; set; }
    public string Address { get; set; }

    // implicit operator
    public static implicit operator Company(CompanyDtoCreate companyDtoCreate)
    {
        return new Company
        {
            CompanyName = companyDtoCreate.CompanyName,
            Description = companyDtoCreate.Description,
            Address = companyDtoCreate.Address
        };
    }

    // explicit operator
    public static explicit operator CompanyDtoCreate(Company company)
    {
        return new CompanyDtoCreate
        {
            CompanyName = company.CompanyName,
            Description = company.Description,
            Address = company.Address
        };
    }
}
