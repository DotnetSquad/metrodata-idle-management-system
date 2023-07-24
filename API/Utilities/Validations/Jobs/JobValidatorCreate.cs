using API.DataTransferObjects.Jobs;
using FluentValidation;

namespace API.Utilities.Validations.Jobs;

public class JobValidatorCreate : AbstractValidator<JobDtoCreate>
{
    public JobValidatorCreate()
    {
        RuleFor(x => x.JobName)
            .NotEmpty();

        RuleFor(x => x.Description)
           .NotEmpty();

        RuleFor(x => x.CompanyGuid)
           .NotEmpty();
    }
}
