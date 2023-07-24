using API.DataTransferObjects.Profiles;
using FluentValidation;

namespace API.Utilities.Validations.Profiles;

public class ProfileValidatorUpdate : AbstractValidator<ProfileDtoUpdate>
{
    public ProfileValidatorUpdate()
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
