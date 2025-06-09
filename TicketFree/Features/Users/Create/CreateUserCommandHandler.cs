using MediatR;
using TicketFree.Features.Interfaces;

namespace TicketFree.Features.Users.Create
{
    public class CreateUserCommandHandler(IApplicationDbContext dbContext) : IRequestHandler<CreateUserCommand, Guid>
    {
        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Role = request.Role,
                Token = Guid.NewGuid()
            };

            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync(cancellationToken);

            return user.Id;

        }
    }

}
