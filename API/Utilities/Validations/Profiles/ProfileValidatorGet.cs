using API.DataTransferObjects.Profiles;
using FluentValidation;

namespace API.Utilities.Validations.Profiles;

public class ProfileValidatorGet : AbstractValidator<ProfileDtoGet>
{
    public ProfileValidatorGet()
    {
        RuleFor(x => x.Guid)
          .NotEmpty();

        RuleFor(x => x.Skills)
            .NotEmpty();

        RuleFor(x => x.Linkedin)
           .NotEmpty();

        RuleFor(x => x.Resume)
           .NotEmpty();
    }
}
