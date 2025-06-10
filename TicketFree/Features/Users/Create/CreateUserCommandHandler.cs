using MediatR;
using TicketFree.Interfaces;

namespace TicketFree.Features.Users.Create
{
    public class CreateUserCommandHandler(IApplicationDbContext dbContext) : IRequestHandler<CreateUserCommand, User>
    {
        public async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
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
