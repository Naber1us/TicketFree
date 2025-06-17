using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TicketFree.Features.Events.Create;
using TicketFree.Interfaces;

namespace TicketFree.Validations
{
    public class CreateEventCommandValidator : AbstractValidator<CreateEventCommand>
    {
        public CreateEventCommandValidator()
        {
            RuleFor(x => x.OrganizatorId)
                .NotEmpty().WithMessage("'{PropertyName}' обязательное поле")
                .WithName("OrganizatorId");

            RuleFor(x => x.PlaceId)
                .NotEmpty().WithMessage("'{PropertyName}' обязательное поле")
                .WithName("PlaceId");
            RuleFor(x => x.EventCountTickets)
                .NotEmpty().WithMessage("'{PropertyName}' обязательное поле")
                .GreaterThan(0).WithMessage("Количество мест должно быть больше 0")
                .WithName("PlaceName");

        }
    }
}
