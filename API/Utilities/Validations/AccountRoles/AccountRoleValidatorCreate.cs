using API.DataTransferObjects.AccountRoles;
using FluentValidation;

namespace API.Utilities.Validations.AccountRoles;

public class AccountRoleValidatorCreate : AbstractValidator<AccountRoleDtoCreate>
{
    public AccountRoleValidatorCreate()
    {
        RuleFor(x => x.AccountGuid)
            .NotEmpty();

        RuleFor(x => x.RoleGuid)
           .NotEmpty();
    }
}
