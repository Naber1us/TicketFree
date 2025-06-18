using AutoMapper;
using TicketFree.Features.Events;
using TicketFree.Features.Events.Dto;
using TicketFree.Features.Places;
using TicketFree.Features.Places.Dto;
using TicketFree.Features.Users;
using TicketFree.Features.Users.Dto;

namespace TicketFree.Interfaces
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMap<Event, EventDto>()
                .ForMember(dest => dest.EventStatus, opt => opt.MapFrom(src => src.EventStatus))
                .ReverseMap();

            CreateMap<Place, PlaceDto>();
            CreateMap<User, UserDto>();
        }
    }
}
