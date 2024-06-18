using Cards.Domain.DTOs.Requests;
using Cards.Domain.DTOs.Requests.Member;
using Cards.Domain.DTOs.Requests.Member.Validators;
using Cards.Domain.DTOs.Requests.Validators;
using Cards.Domain.DTOs.Responses;
using Cards.Domain.Enums;
using Cards.Domain.Mappers;
using Cards.Domain.Services.Abstractions;
using FluentResults;

namespace Cards.Domain.Services
{
    public class MemberCardService : IMemberCardService
    {
        private readonly ICardService _cardService;

        public MemberCardService(ICardService cardService)
        {
            _cardService = cardService;
        }

        public async Task<IResult<CardResponse>> AddCardAsync(AddMemberCardRequest request)
        {
            var validator = new AddMemberCardRequestValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return (IResult<CardResponse>)Result.Fail(validationResult.Errors.Select(val => val.ErrorMessage).ToList());
            }

            var cardEntity = CardMapper.ToEntity(request);

            return await _cardService.AddCardAsync(cardEntity);
        }

        public async Task<IResult<bool>> DeleteCardAsync(Guid id)
        {
            var request = new DeleteCardRequest()
            {
                Id = id
            };

            var validator = new DeleteCardRequestValidator();
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return (IResult<bool>)Result.Fail(validationResult.Errors.Select(val => val.ErrorMessage).ToList());
            }

            var cardEntity = CardMapper.ToEntity(request);

            if (!cardEntity.Role.Equals(Role.Member.ToString()))
            {
                return (IResult<bool>)Result.Fail("Role should be Member for this request");
            }

            return await _cardService.DeleteCardAsync(cardEntity);
        }

        public async Task<IResult<CardResponse>> GetCardAsync(Guid id)
        {
            var request = new GetCardRequest
            {
                Id = id
            };

            var validator = new GetCardRequestValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return (IResult<CardResponse>)Result.Fail(validationResult.Errors.Select(val => val.ErrorMessage).ToList());
            }

            var cardEntity = CardMapper.ToEntity(request);

            if (!cardEntity.Role.Equals(Role.Member.ToString()))
            {
                return (IResult<CardResponse>)Result.Fail("Role should be Member for this request");
            }

            return await _cardService.GetCardAsync(cardEntity);
        }

        public async Task<IResult<IEnumerable<CardResponse>>> GetPaginatedCardsAsync(int pageSize, int pageIndex, GetCardsRequest request)
        {
            var validator = new GetCardsRequestValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return (IResult<IEnumerable<CardResponse>>)Result.Fail(validationResult.Errors.Select(val => val.ErrorMessage).ToList());
            }

            var cardsEntity = CardMapper.ToEntity(request);

            var cards = cardsEntity.Where(x => x.Role.Equals(Role.Member.ToString()));

            return await _cardService.GetPaginatedCardsAsync(pageSize, pageIndex, cards);
        }

        public async Task<IResult<CardResponse>> UpdateCardAsync(UpdateCardRequest request)
        {
            var validator = new UpdateCardRequestValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return (IResult<CardResponse>)Result.Fail(validationResult.Errors.Select(val => val.ErrorMessage).ToList());
            }
            var cardEntity = CardMapper.ToEntity(request);

            if (!cardEntity.Role.Equals(Role.Member.ToString()))
            {
                return (IResult<CardResponse>)Result.Fail("Role should be Member for this request");
            }

            return await _cardService.UpdateCardAsync(cardEntity);
        }
    }
}
