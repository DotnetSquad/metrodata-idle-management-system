using API.DataTransferObjects.Jobs;
using FluentValidation;

namespace API.Utilities.Validations.Jobs;

public class JobValidatorUpdate : AbstractValidator<JobDtoUpdate>
{
    public JobValidatorUpdate()
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
