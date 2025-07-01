using FluentValidation;
using TicketFree.Features.Places.Create;

namespace TicketFree.Validations
{
    public class CreatePlacesCommandValidator : AbstractValidator<CreatePlaceCommand>
    {
        public CreatePlacesCommandValidator()
        {
            RuleFor(x => x.PlaceHolder)
                .NotEmpty().WithMessage("'{PropertyName}' обязательное поле")
                .WithName("PlaceHolder");

            RuleFor(x => x.PlaceCountMembers)
                .NotEmpty().WithMessage("'{PropertyName}' обязательное поле")
                .GreaterThan(0).WithMessage("Количество мест должно быть больше, чем 0")
                 .WithName("PlaceCountMembers");

            RuleFor(x => x.PlaceName)
                .NotEmpty().WithMessage("'{PropertyName}' обязательное поле")
                .MaximumLength(50).WithMessage("Длинное название. Нужно указать до 50 символов")
                .WithName("PlaceName");
        }
    }
}
