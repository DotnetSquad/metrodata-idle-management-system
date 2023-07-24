using API.DataTransferObjects.Companies;
using FluentValidation;

namespace API.Utilities.Validations.Companies;

public class CompanyValidatorUpdate : AbstractValidator<CompanyDtoUpdate>
{
    public CompanyValidatorUpdate()
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
