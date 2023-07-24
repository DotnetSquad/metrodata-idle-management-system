using API.Contracts;
using API.DataTransferObjects.Accounts;
using FluentValidation;

namespace API.Utilities.Validations.Accounts;

public class AccountValidatorRegister : AbstractValidator<AccountDtoRegister>
{
    private readonly IEmployeeRepository _employeeRepository;
    public AccountValidatorRegister(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;

        RuleFor(x => x.FirstName).NotEmpty();

        RuleFor(x => x.BirthDate)
            .NotEmpty();

        RuleFor(x => x.Gender)
           .NotNull()
           .IsInEnum();

        RuleFor(x => x.HiringDate)
            .NotEmpty()
            .GreaterThanOrEqualTo(x => x.BirthDate.AddYears(18));

        RuleFor(x => x.Email)
            .NotEmpty()
            .Must(BeUniqueProperty).WithMessage("'Email' already registered")
            .EmailAddress();

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .Must(BeUniqueProperty).WithMessage("'Phone Number' already registered")
            .Matches(@"^\+[1-9]\d{1,20}$");

        RuleFor(x => x.Password)
           .NotEmpty().WithMessage("Password is required.")
           .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
           .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
           .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
           .Matches("[0-9]").WithMessage("Password must contain at least one digit.")
           .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.");

        RuleFor(x => x.ConfirmPassword)
          .NotEmpty()
          .Equal(x => x.Password).WithMessage("Password and Confirm Password do not match.");
        _employeeRepository = employeeRepository;
    }
    private bool BeUniqueProperty(string property)
    {
        return _employeeRepository.IsDuplicateValue(property);
    }
}
