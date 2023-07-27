using API.Contracts;
using API.DataTransferObjects.Employees;
using FluentValidation;

namespace API.Utilities.Validations.Employees;

public class EmployeeValidatorUpdate : AbstractValidator<EmployeeDtoUpdate>
{
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeeValidatorUpdate(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;

        RuleFor(x => x.Guid)
            .NotEmpty();

        RuleFor(x => x.Nik)
            .NotEmpty();

        RuleFor(x => x.FirstName)
            .NotEmpty();

        RuleFor(x => x.BirthDate)
            .NotEmpty();

        RuleFor(x => x.Gender)
            .IsInEnum();

        RuleFor(x => x.HiringDate)
            .NotEmpty()
            .GreaterThanOrEqualTo(x => x.BirthDate.AddYears(18));

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .Matches(@"^\+[1-9]\d{1,20}$");

        RuleFor(x => x.Status)
            .IsInEnum();
    }
}
