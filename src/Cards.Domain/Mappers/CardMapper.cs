using AutoMapper;
using Cards.Domain.DTOs.Requests;
using Cards.Domain.DTOs.Requests.Admin;
using Cards.Domain.DTOs.Requests.Member;
using Cards.Domain.DTOs.Responses;
using Cards.Domain.Entities;

namespace Cards.Domain.Mappers
{
    public static class CardMapper
    {
        private static IMapper Mapper { get; }

        static CardMapper()
        {
            Mapper = new MapperConfiguration(config => config.AddProfile<CardProfile>()).CreateMapper();
        }

        public static Card ToEntity(this AddAdminCardRequest request)
        {
            return Mapper.Map<Card>(request);
        }

        public static Card ToEntity(this UpdateCardRequest request)
        {
            return Mapper.Map<Card>(request);
        }

        public static Card ToEntity(this GetCardRequest request)
        {
            return Mapper.Map<Card>(request);
        }

        public static IEnumerable<Card> ToEntity(this GetCardsRequest request)
        {
            return Mapper.Map<IEnumerable<Card>>(request);
        }

        public static Card ToEntity(this DeleteCardRequest request)
        {
            return Mapper.Map<Card>(request);
        }

        public static Card ToEntity(this AddMemberCardRequest request)
        {
            return Mapper.Map<Card>(request);
        }

        public static CardResponse ToResponse(this Card project)
        {
            return Mapper.Map<CardResponse>(project);
        }

        public static IEnumerable<CardResponse> ToEnumerableResponse(this IEnumerable<Card> project)
        {
            return Mapper.Map<IEnumerable<CardResponse>>(project);
        }
    }
}
