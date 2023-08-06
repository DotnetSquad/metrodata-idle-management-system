using API.DataTransferObjects.Profiles;
using FluentValidation;

namespace API.Utilities.Validations.Profiles;

public class ProfileValidatorCreate : AbstractValidator<ProfileDtoCreate>
{
    public ProfileValidatorCreate()
    {
        RuleFor(x => x.PhotoProfile)
            .NotEmpty();
        
        RuleFor(x => x.Skills)
            .NotEmpty();

        RuleFor(x => x.Linkedin)
           .NotEmpty();

        RuleFor(x => x.Resume)
           .NotEmpty();
    }
}
