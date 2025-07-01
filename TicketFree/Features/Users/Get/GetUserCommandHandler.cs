using MediatR;
using Microsoft.EntityFrameworkCore;
using TicketFree.Features.Users.Dto;
using TicketFree.Interfaces;

namespace TicketFree.Features.Users.Get
{
    public class GetUserByIdQueryHandler(IApplicationDbContext context) : IRequestHandler<GetUserByIdQuery, UserDto?>
    {
        private readonly IApplicationDbContext _context = context;

        public async Task<UserDto?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.UsersInfo
                .Where(u => u.UserId == request.Id)
                .Select(u => new UserDto
                {
                    UserId = u.UserId,
                    UserName = u.UserName,
                    UserRole = u.UserRole
                })
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);
        }
    }

    public class GetAllUsersQueryHandler(IApplicationDbContext context) : IRequestHandler<GetAllUsersQuery, List<UserDto>>
    {
        private readonly IApplicationDbContext _context = context;

        public async Task<List<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            return await _context.UsersInfo
                .Select(u => new UserDto
                {
                    UserId = u.UserId,
                    UserName = u.UserName,
                    UserRole = u.UserRole
                })
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
    }
}
