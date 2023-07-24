using API.DataTransferObjects.EmployeeProjects;
using FluentValidation;

namespace API.Utilities.Validations.EmployeeProjects;

public class EmployeeProjectValidatorUpdate : AbstractValidator<EmployeeProjectDtoUpdate>
{
    public EmployeeProjectValidatorUpdate()
    {
        RuleFor(x => x.Guid).NotEmpty();

        RuleFor(x => x.EmployeeGuid).NotEmpty();

        RuleFor(x => x.ProjectGuid).NotEmpty();
    }
}
