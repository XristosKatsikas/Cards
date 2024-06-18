using Cards.Domain.DTOs.Requests;
using Cards.Domain.DTOs.Requests.Admin;
using Cards.Domain.DTOs.Requests.Admin.Validators;
using Cards.Domain.DTOs.Requests.Validators;
using Cards.Domain.DTOs.Responses;
using Cards.Domain.Mappers;
using Cards.Domain.Services.Abstractions;
using FluentResults;

namespace Cards.Domain.Services
{
    public class AdminCardService : IAdminCardService
    {
        private readonly ICardService _cardService;

        public AdminCardService(ICardService cardService)
        {
            _cardService = cardService;
        }

        public async Task<IResult<CardResponse>> AddCardAsync(AddAdminCardRequest request)
        {
            var validator = new AddAdminCardRequestValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return (IResult<CardResponse>)Result.Fail(validationResult.Errors.Select(val => val.ErrorMessage).ToList());
            }

            var cardEntity = CardMapper.ToEntity(request);

            return await _cardService.AddCardAsync(cardEntity);
        }

        public async Task<IResult<CardResponse>> DeleteCardAsync(DeleteCardRequest request)
        {
            var validator = new DeleteCardRequestValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return (IResult<CardResponse>)Result.Fail(validationResult.Errors.Select(val => val.ErrorMessage).ToList());
            }
            var cardEntity = CardMapper.ToEntity(request);

            return await _cardService.DeleteCardAsync(cardEntity);
        }

        public async Task<IResult<CardResponse>> GetCardAsync(GetCardRequest request)
        {
            var validator = new GetCardRequestValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return (IResult<CardResponse>)Result.Fail(validationResult.Errors.Select(val => val.ErrorMessage).ToList());
            }

            var cardEntity = CardMapper.ToEntity(request);

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

            var cardEntity = CardMapper.ToEntity(request);

            return await _cardService.GetPaginatedCardsAsync(pageSize, pageIndex, cardEntity);
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

            return await _cardService.UpdateCardAsync(cardEntity);
        }
    }
}