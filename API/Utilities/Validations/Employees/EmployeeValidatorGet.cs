using API.DataTransferObjects.Employees;
using FluentValidation;

namespace API.Utilities.Validations.Employees;

public class EmployeeValidatorGet : AbstractValidator<EmployeeDtoGet>
{
    public EmployeeValidatorGet()
    {
        RuleFor(x => x.Guid)
            .NotEmpty();

        RuleFor(x => x.Nik)
            .NotEmpty();

        RuleFor(x => x.FirstName)
            .NotEmpty();

        RuleFor(x => x.BirthDate).NotEmpty()
            .LessThan(x => x.HiringDate)
            .GreaterThanOrEqualTo(x => x.BirthDate.AddYears(18));

        RuleFor(x => x.Gender)
            .NotEmpty();

        RuleFor(x => x.HiringDate)
            .NotEmpty()
            .GreaterThanOrEqualTo(x => x.BirthDate.AddYears(0));

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .Matches(@"^\d+$");

        RuleFor(x => x.Status)
            .NotEmpty()
            .IsInEnum();
    }
}
