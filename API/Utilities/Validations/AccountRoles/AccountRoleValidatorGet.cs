using API.DataTransferObjects.AccountRoles;
using FluentValidation;

namespace API.Utilities.Validations.AccountRoles;

public class AccountRoleValidatorGet : AbstractValidator<AccountRoleDtoGet>
{
    public AccountRoleValidatorGet()
    {
        RuleFor(x => x.Guid)
            .NotEmpty();

        RuleFor(x => x.AccountGuid)
            .NotEmpty();

        RuleFor(x => x.RoleGuid)
           .NotEmpty();
    }
}
