using API.DataTransferObjects.AccountRoles;
using FluentValidation;

namespace API.Utilities.Validations.AccountRoles;

public class AccountRoleValidatorUpdate : AbstractValidator<AccountRoleDtoUpdate>
{
    public AccountRoleValidatorUpdate()
    {
        RuleFor(x => x.Guid)
            .NotEmpty();

        RuleFor(x => x.AccountGuid)
            .NotEmpty();

        RuleFor(x => x.RoleGuid)
           .NotEmpty();
    }
}
