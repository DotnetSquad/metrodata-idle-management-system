using API.DataTransferObjects.Roles;
using FluentValidation;

namespace API.Utilities.Validations.Roles;

public class RoleValidatorGet : AbstractValidator<RoleDtoGet>
{
    public RoleValidatorGet()
    {
        RuleFor(x => x.Guid)
            .NotEmpty();

        RuleFor(x => x.Name)
            .NotEmpty();
    }
}
