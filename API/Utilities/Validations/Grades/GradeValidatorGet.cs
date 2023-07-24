using API.DataTransferObjects.Grades;
using API.Utilities.Enums;
using FluentValidation;

namespace API.Utilities.Validations.Grades;

public class GradeValidatorGet : AbstractValidator<GradeDtoGet>
{
    public GradeValidatorGet()
    {
        RuleFor(x => x.Guid)
            .NotEmpty();

        RuleFor(x => x.GradeLevel)
            .NotEmpty()
            .Must(x => x == GradeEnum.A || x == GradeEnum.B)
            .WithMessage("GradeLevel must be either 0 is grade A or 1 is grade B");

        RuleFor(x => x.ScoreSegment1)
            .NotEmpty()
            .InclusiveBetween(0, 100)
            .WithMessage("ScoreSegment1 must be between 0 and 100");

        RuleFor(x => x.ScoreSegment2)
            .NotEmpty()
            .InclusiveBetween(0, 100)
            .WithMessage("ScoreSegment2 must be between 0 and 100");

        RuleFor(x => x.ScoreSegment3)
            .NotEmpty()
            .InclusiveBetween(0, 100)
            .WithMessage("ScoreSegment3 must be between 0 and 100");

        RuleFor(x => x.ScoreSegment4)
            .NotEmpty()
            .InclusiveBetween(0, 100)
            .WithMessage("ScoreSegment4 must be between 0 and 100");
    }
}

