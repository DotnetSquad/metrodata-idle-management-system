using API.DataTransferObjects.Placements;
using FluentValidation;

namespace API.Utilities.Validations.Placements;

public class PlacementValidatorCreate : AbstractValidator<PlacementDtoCreate>
{
    public PlacementValidatorCreate()
    {
        RuleFor(x => x.Title).NotEmpty();

        RuleFor(x => x.Description).NotEmpty();

        RuleFor(x => x.EmployeeGuid).NotEmpty();

        RuleFor(x => x.CompanyGuid).NotEmpty();
    }
}
