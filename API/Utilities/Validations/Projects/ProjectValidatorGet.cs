using API.DataTransferObjects.Projects;
using FluentValidation;

namespace API.Utilities.Validations.Projects;

public class ProjectValidatorGet : AbstractValidator<ProjectDtoGet>
{
    public ProjectValidatorGet()
    {
        RuleFor(x => x.Guid).NotEmpty();

        RuleFor(x => x.NameProject).NotEmpty();

        RuleFor(x => x.ProjectLead).NotEmpty();

        RuleFor(x => x.Description).NotEmpty();
    }
}
