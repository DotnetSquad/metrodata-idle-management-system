using API.DataTransferObjects.Interviews;
using FluentValidation;

namespace API.Utilities.Validations.Interviews;

public class InterviewValidatorGet : AbstractValidator<InterviewDtoGet>
{
    public InterviewValidatorGet()
    {
        RuleFor(x => x.Guid).NotEmpty();

        RuleFor(x => x.Title).NotEmpty();

        RuleFor(x => x.InterviewDate).NotEmpty();

        RuleFor(x => x.Description).NotEmpty();

        RuleFor(x => x.StatusInterview).NotNull().IsInEnum().WithMessage("Status Interview must be either 0 is Pending status, 1 is Accepted status, and 2 is Rejected");

        RuleFor(x => x.JobGuid).NotEmpty();
    }
}