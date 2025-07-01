using FluentValidation;
using TicketFree.Features.Users.Create;
using TicketFree.Features.Users.Get;

namespace TicketFree.Validations
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("'{PropertyName}' обязательное поле")
                .MaximumLength(50).WithMessage("Для поля '{PropertyName}' длина символов не более 50");

            RuleFor(x => x.UserRole)
                .NotEmpty().WithMessage("'{PropertyName}' обязательное поле")
                .IsInEnum().WithMessage("Некорректно указана роль. Ожидается ввод: \n- 0: Администратор, \n- 1: Собственник помещения, \n- 2: Организатор мероприятий, \n- 3: Посетитель");
        }
    }

    public class GetUserCommandValidator : AbstractValidator<GetUserCommand>
    {
        public GetUserCommandValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("'{PropertyName}' обязательное поле")
                .MaximumLength(50).WithMessage("Для поля '{PropertyName}' длина символов не более 50");

            RuleFor(x => x.UserRole)
                .NotEmpty().WithMessage("'{PropertyName}' обязательное поле")
                .IsInEnum().WithMessage("Некорректно указана роль. Ожидается ввод: \n- 0: Администратор, \n- 1: Собственник помещения, \n- 2: Организатор мероприятий, \n- 3: Посетитель");
        }
    }
}
