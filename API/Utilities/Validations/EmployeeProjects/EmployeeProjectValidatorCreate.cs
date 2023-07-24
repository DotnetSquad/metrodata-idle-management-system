using API.DataTransferObjects.EmployeeProjects;
using FluentValidation;

namespace API.Utilities.Validations.EmployeeProjects;

public class EmployeeProjectValidatorCreate : AbstractValidator<EmployeeProjectDtoCreate>
{
    public EmployeeProjectValidatorCreate()
    {
        RuleFor(x => x.EmployeeGuid).NotEmpty();

        RuleFor(x => x.ProjectGuid).NotEmpty();
    }
}
