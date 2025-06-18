using FluentValidation;
using TicketFree.Features.Events.Create;
using TicketFree.Features.Tickets.Buy;

namespace TicketFree.Validations
{
    public class TicketsCommandValidator : AbstractValidator<BuyTicketCommand>
    {
        public TicketsCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("'{PropertyName}' обязательное поле")
                .WithName("UserId");

            RuleFor(x => x.EventId)
                .NotEmpty().WithMessage("'{PropertyName}' обязательное поле")
                .WithName("EventId");
        }
    }
}
