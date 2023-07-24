using API.DataTransferObjects.Companies;
using FluentValidation;

namespace API.Utilities.Validations.Companies;

public class CompanyValidatorGet : AbstractValidator<CompanyDtoGet>
{
    public CompanyValidatorGet()
    {
        RuleFor(company => company.Guid)
            .NotEmpty();

        RuleFor(company => company.CompanyName)
            .NotEmpty();

        RuleFor(company => company.Description)
            .NotEmpty();

        RuleFor(company => company.Address)
            .NotEmpty();
    }
}
