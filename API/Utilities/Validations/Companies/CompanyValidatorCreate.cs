using API.DataTransferObjects.Companies;
using FluentValidation;

namespace API.Utilities.Validations.Companies;

public class CompanyValidatorCreate : AbstractValidator<CompanyDtoCreate>
{
    public CompanyValidatorCreate()
    {
        RuleFor(company => company.CompanyName)
            .NotEmpty();

        RuleFor(company => company.Description)
            .NotEmpty();

        RuleFor(company => company.Address)
            .NotEmpty();
    }
}
