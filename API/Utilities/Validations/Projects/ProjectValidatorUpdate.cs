using API.DataTransferObjects.Projects;
using FluentValidation;

namespace API.Utilities.Validations.Projects;

public class ProjectValidatorUpdate : AbstractValidator<ProjectDtoUpdate>
{
    public ProjectValidatorUpdate()
    {
        RuleFor(x => x.Guid).NotEmpty();

        RuleFor(x => x.NameProject).NotEmpty();

        RuleFor(x => x.ProjectLead).NotEmpty();

        RuleFor(x => x.Description).NotEmpty();
    }
}
