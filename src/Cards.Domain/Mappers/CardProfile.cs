using AutoMapper;
using Cards.Domain.DTOs.Requests;
using Cards.Domain.DTOs.Requests.Admin;
using Cards.Domain.DTOs.Requests.Member;
using Cards.Domain.DTOs.Responses;
using Cards.Domain.Entities;

namespace Cards.Domain.Mappers
{
    public class CardProfile : Profile
    {
        public CardProfile() 
        {
            CreateMap<AddAdminCardRequest, Card>()
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dst => dst.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dst => dst.DateCreated, opt => opt.MapFrom(src => src.DateCreated))
                .ForMember(dst => dst.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dst => dst.Color, opt => opt.MapFrom(src => src.Color))
                .ForMember(dst => dst.Role, opt => opt.MapFrom(src => src.UserRole))
                .ReverseMap();
            CreateMap<GetCardRequest, Card>()
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
                .ReverseMap();
            CreateMap<GetCardsRequest, Card>()
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dst => dst.DateCreated, opt => opt.MapFrom(src => src.DateCreated))
                .ForMember(dst => dst.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dst => dst.Color, opt => opt.MapFrom(src => src.Color))
                .ReverseMap();
            CreateMap<DeleteCardRequest, Card>()
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
                .ReverseMap();
            CreateMap<UpdateCardRequest, Card>()
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dst => dst.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dst => dst.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dst => dst.Color, opt => opt.MapFrom(src => src.Color))
                .ReverseMap();

            CreateMap<AddMemberCardRequest, Card>()
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dst => dst.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dst => dst.DateCreated, opt => opt.MapFrom(src => src.DateCreated))
                .ForMember(dst => dst.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dst => dst.Color, opt => opt.MapFrom(src => src.Color))
                .ForMember(dst => dst.Role, opt => opt.MapFrom(src => src.Role))
                .ReverseMap();

            CreateMap<Card, CardResponse>()
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dst => dst.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dst => dst.DateCreated, opt => opt.MapFrom(src => src.DateCreated))
                .ForMember(dst => dst.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dst => dst.Color, opt => opt.MapFrom(src => src.Color))
                .ForMember(dst => dst.UserRole, opt => opt.MapFrom(src => src.Role))
                .ReverseMap();
        }
    }
}
