using API.DataTransferObjects.EmployeeJobs;
using FluentValidation;

namespace API.Utilities.Validations.EmployeeInterviews;

public class EmployeeJobValidatorUpdate : AbstractValidator<EmployeeJobDtoUpdate>
{
    public EmployeeJobValidatorUpdate()
    {
        RuleFor(x => x.Guid)
           .NotEmpty();

        RuleFor(x => x.EmployeeGuid)
           .NotEmpty();

        RuleFor(x => x.JobGuid)
           .NotEmpty();

        RuleFor(x => x.StatusApproval)
            .NotEmpty();
    }
}
