using API.DataTransferObjects.EmployeeJobs;
using FluentValidation;

namespace API.Utilities.Validations.EmployeeInterviews;

public class EmployeeJobValidatorCreate : AbstractValidator<EmployeeJobDtoCreate>
{
    public EmployeeJobValidatorCreate()
    {
        RuleFor(x => x.EmployeeGuid)
            .NotEmpty();

        RuleFor(x => x.JobGuid)
            .NotEmpty();

        RuleFor(x => x.StatusApproval)
            .NotEmpty();
    }
}
