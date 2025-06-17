using FluentValidation;
using MediatR;
using TicketFree.Interfaces;

namespace TicketFree.Features.Users.Create
{
    public class CreateUserCommandHandler(IApplicationDbContext dbContext, IValidator<CreateUserCommand> _validator) : IRequestHandler<CreateUserCommand, User>
    {
        public async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);


            var user = new User
            {
                UserId = Guid.NewGuid(),
                UserName = request.UserName,
                UserRole = request.UserRole,
                UserToken = Guid.NewGuid()
            };

            dbContext.UsersInfo.Add(user);
            await dbContext.SaveChangesAsync(cancellationToken);

            return user;

        }
    }

}
