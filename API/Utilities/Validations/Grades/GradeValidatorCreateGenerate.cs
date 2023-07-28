using API.DataTransferObjects.Grades;
using FluentValidation;

namespace API.Utilities.Validations.Grades;

public class GradeValidatorCreateGenerate : AbstractValidator<GradeDtoGenerateScore>
{
    public GradeValidatorCreateGenerate()
    {
        RuleFor(x => x.ScoreSegment1)
            .NotEmpty()
            .InclusiveBetween(0, 100)
            .WithMessage("Score Segment 1 must be between 0 and 100");

        RuleFor(x => x.ScoreSegment2)
            .NotEmpty()
            .InclusiveBetween(0, 100)
            .WithMessage("Score Segment 2 must be between 0 and 100");

        RuleFor(x => x.ScoreSegment3)
            .NotEmpty()
            .InclusiveBetween(0, 100)
            .WithMessage("Score Segment 3 must be between 0 and 100");

        RuleFor(x => x.ScoreSegment4)
            .NotEmpty()
            .InclusiveBetween(0, 100)
            .WithMessage("Score Segment 4 must be between 0 and 100");
    }
}
