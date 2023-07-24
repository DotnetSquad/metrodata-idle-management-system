using API.DataTransferObjects.EmployeeProjects;
using FluentValidation;

namespace API.Utilities.Validations.EmployeeProjects;

public class EmployeeProjectValidatorGet : AbstractValidator<EmployeeProjectDtoGet>
{
    public EmployeeProjectValidatorGet()
    {
        RuleFor(x => x.Guid).NotEmpty();

        RuleFor(x => x.EmployeeGuid).NotEmpty();

        RuleFor(x => x.ProjectGuid).NotEmpty();
    }
}
