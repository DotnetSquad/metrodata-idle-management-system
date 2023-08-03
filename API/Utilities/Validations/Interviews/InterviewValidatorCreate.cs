using API.DataTransferObjects.Interviews;
using FluentValidation;

namespace API.Utilities.Validations.Interviews;

public class InterviewValidatorCreate : AbstractValidator<InterviewDtoCreate>
{
    public InterviewValidatorCreate()
    {
        RuleFor(x => x.Title).NotEmpty();

        RuleFor(x => x.InterviewDate).NotEmpty();

        RuleFor(x => x.Description).NotEmpty();
    }
}
