using TicketFree.Enums;

namespace TicketFree.Features.Users.Dto
{
    public record UserDto
    {
        public string Name { get; set; } = string.Empty;
        public ERoles Role { get; set; } = ERoles.Guest;
    }
}
