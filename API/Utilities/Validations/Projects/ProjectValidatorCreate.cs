using API.DataTransferObjects.Projects;
using FluentValidation;

namespace API.Utilities.Validations.Projects;

public class ProjectValidatorCreate : AbstractValidator<ProjectDtoCreate>
{
    public ProjectValidatorCreate()
    {
        RuleFor(x => x.NameProject).NotEmpty();

        RuleFor(x => x.ProjectLead).NotEmpty();

        RuleFor(x => x.Description).NotEmpty();
    }
}
