using API.DataTransferObjects.EmployeeInterviews;
using FluentValidation;

namespace API.Utilities.Validations.EmployeeInterviews;

public class EmployeeInterviewValidatorUpdate : AbstractValidator<EmployeeInterviewDtoUpdate>
{
    public EmployeeInterviewValidatorUpdate()
    {
        RuleFor(x => x.Guid)
           .NotEmpty();

        RuleFor(x => x.EmployeeGuid)
           .NotEmpty();

        RuleFor(x => x.InterviewGuid)
           .NotEmpty();
    }
}
