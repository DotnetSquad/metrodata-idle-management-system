using API.DataTransferObjects.Interviews;
using FluentValidation;

namespace API.Utilities.Validations.Interviews;

public class InterviewValidatorUpdate : AbstractValidator<InterviewDtoUpdate>
{
    public InterviewValidatorUpdate()
    {
        RuleFor(x => x.Guid).NotEmpty();

        RuleFor(x => x.Title).NotEmpty();

        RuleFor(x => x.InterviewDate).NotEmpty();

        RuleFor(x => x.Description).NotEmpty();
    }
}
