using API.DataTransferObjects.Roles;
using FluentValidation;

namespace API.Utilities.Validations.Roles;

public class RoleValidatorUpdate : AbstractValidator<RoleDtoUpdate>
{
    public RoleValidatorUpdate()
    {
        RuleFor(x => x.Guid)
            .NotEmpty();

        RuleFor(x => x.Name)
            .NotEmpty();
    }
}
