using Cards.Core;
using Cards.Core.Abstractions;
using Cards.Domain.DTOs.Responses;
using Cards.Domain.Entities;
using Cards.Domain.Helpers;
using Cards.Domain.Mappers;
using Cards.Domain.Repositories.Abstractions;
using Cards.Domain.Services.Abstractions;
using Microsoft.Extensions.Logging;

namespace Cards.Domain.Services
{
    public class CardService : ICardService
    {
        private readonly ICardRepository _cardRepository;
        private readonly ILogger<CardService> _logger;

        public CardService(
            ICardRepository cardRepository,
            ILogger<CardService> logger)
        {
            _cardRepository = cardRepository;
            _logger = logger;
        }

        public async Task<IResult<CardResponse>> AddCardAsync(Card cardEntity)
        {
            try
            {
                var card = _cardRepository.AddCard(cardEntity);

                if (card is null)
                {
                    return await FailedResponse.GetBadRequestResultAsync(cardEntity.Id);
                }

                await _cardRepository.UnitOfWork.SaveChangesAsync();

                return Result<CardResponse>.CreateSuccessful(card.ToResponse());
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception was thrown from {nameof(AddCardAsync)}");

                return Result<CardResponse>.CreateFailed(_logger, ResultCode.BadGateway, ex.Message);
            }
        }

        public async Task<IResult<CardResponse>> DeleteCardAsync(Card cardEntity)
        {
            try
            {
                var card = _cardRepository.DeleteCard(cardEntity);

                if (card is null)
                {
                    return await FailedResponse.GetBadRequestResultAsync(cardEntity.Id);
                }

                await _cardRepository.UnitOfWork.SaveChangesAsync();

                return Result<CardResponse>.CreateSuccessful(card.ToResponse());
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception was thrown from {nameof(DeleteCardAsync)}");

                return Result<CardResponse>.CreateFailed(_logger, ResultCode.BadGateway, ex.Message);
            }
        }

        public async Task<IResult<CardResponse>> UpdateCardAsync(Card cardEntity)
        {
            try
            {
                var card = _cardRepository.UpdateCard(cardEntity);
                
                if (card is null)
                {
                    return await FailedResponse.GetBadRequestResultAsync(cardEntity.Id);
                }

                await _cardRepository.UnitOfWork.SaveChangesAsync();

                return Result<CardResponse>.CreateSuccessful(card.ToResponse());
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception was thrown from {nameof(UpdateCardAsync)}");

                return Result<CardResponse>.CreateFailed(_logger, ResultCode.BadGateway, ex.Message);
            }
        }

        public async Task<IResult<CardResponse>> GetCardAsync(Card cardEntity)
        {
            try
            {
                var card = await _cardRepository.GetCardByIdAsync(cardEntity.Id);

                if (card is null)
                {
                    return await FailedResponse.GetBadRequestResultAsync(cardEntity.Id);
                }

                return Result<CardResponse>.CreateSuccessful(card.ToResponse());
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception was thrown from {nameof(GetCardAsync)}");

                return Result<CardResponse>.CreateFailed(_logger, ResultCode.BadGateway, ex.Message);
            }
        }

        public async Task<IResult<IEnumerable<CardResponse>>> GetPaginatedCardsAsync(int pageSize, int pageIndex, IEnumerable<Card> cards)
        {
            try
            {
                var cardsCount = cards.Count();

                // ordered by Name
                var orderedCardsOnPage = cards.ToEnumerableResponse()
                    .OrderBy(c => c.Name)
                    .Skip(pageSize * pageIndex)
                    .Take(pageSize);

                var model = new PaginatedEntityResponseModel<CardResponse>(
                    pageIndex, pageSize, cardsCount, orderedCardsOnPage);

                return Result<IEnumerable<CardResponse>>.CreateSuccessful(model.Data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception was thrown from {nameof(GetPaginatedCardsAsync)}");

                return Result<IEnumerable<CardResponse>>.CreateFailed(_logger, ResultCode.BadGateway, ex.Message);
            }
        }
    }
}