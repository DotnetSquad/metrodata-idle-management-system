using API.DataTransferObjects.Jobs;
using FluentValidation;

namespace API.Utilities.Validations.Jobs;

public class JobValidatorGet : AbstractValidator<JobDtoGet>
{
    public JobValidatorGet()
    {
        RuleFor(x => x.Guid)
           .NotEmpty();

        RuleFor(x => x.JobName)
            .NotEmpty();

        RuleFor(x => x.Description)
           .NotEmpty();

        RuleFor(x => x.CompanyGuid)
           .NotEmpty();
    }
}
