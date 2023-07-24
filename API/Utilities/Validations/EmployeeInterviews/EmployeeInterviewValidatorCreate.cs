using API.DataTransferObjects.EmployeeInterviews;
using FluentValidation;

namespace API.Utilities.Validations.EmployeeInterviews;

public class EmployeeInterviewValidatorCreate : AbstractValidator<EmployeeInterviewDtoCreate>
{
    public EmployeeInterviewValidatorCreate()
    {
        RuleFor(x => x.EmployeeGuid)
            .NotEmpty();

        RuleFor(x => x.InterviewGuid)
            .NotEmpty();
    }
}
