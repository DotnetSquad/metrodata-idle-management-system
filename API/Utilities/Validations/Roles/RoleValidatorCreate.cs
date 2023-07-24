using API.DataTransferObjects.Roles;
using FluentValidation;

namespace API.Utilities.Validations.Roles;

public class RoleValidatorCreate : AbstractValidator<RoleDtoCreate>
{
    public RoleValidatorCreate()
    {
        RuleFor(x => x.Name)
            .NotEmpty();
    }
}
