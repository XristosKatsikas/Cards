using Cards.Core;
using Cards.Domain.DTOs.Responses;
using Cards.Domain.Entities;
using Cards.Domain.Mappers;
using Cards.Domain.Repositories.Abstractions;
using Cards.Domain.Services.Abstractions;
using FluentResults;
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
                    _logger.LogError($"Post data from CardService.{nameof(AddCardAsync)} has failed.");
                    return Result.Fail<CardResponse>(FailedResultMessage.Unprocessable);
                }

                await _cardRepository.UnitOfWork.SaveChangesAsync();

                return Result.Ok(card.ToResponse());
            }
            catch (Exception ex)
            {
                _logger.LogError($"CardService.{nameof(AddCardAsync)} has failed with exception message: {ex.Message}");
                return Result.Fail<CardResponse>(FailedResultMessage.Exception);
            }
        }

        public async Task<IResult<bool>> DeleteCardAsync(Card cardEntity)
        {
            try
            {
                var isCardDeleted = _cardRepository.DeleteCard(cardEntity);

                if (!isCardDeleted)
                {
                    _logger.LogError($"Delete data from CardService.{nameof(DeleteCardAsync)} has failed.");
                    return Result.Fail<bool>(FailedResultMessage.NotFound);
                }

                await _cardRepository.UnitOfWork.SaveChangesAsync();

                return Result.Ok(isCardDeleted);
            }
            catch (Exception ex)
            {
                _logger.LogError($"CardService.{nameof(DeleteCardAsync)} has failed with exception message: {ex.Message}");
                return Result.Fail<bool>(FailedResultMessage.Exception);
            }
        }

        public async Task<IResult<CardResponse>> UpdateCardAsync(Card cardEntity)
        {
            try
            {
                var card = _cardRepository.UpdateCard(cardEntity);
                
                if (card is null)
                {
                    _logger.LogError($"Update data from CardService.{nameof(UpdateCardAsync)} has failed.");
                    return Result.Fail<CardResponse>(FailedResultMessage.Unprocessable);
                }

                await _cardRepository.UnitOfWork.SaveChangesAsync();

                return Result.Ok(card.ToResponse());
            }
            catch (Exception ex)
            {
                _logger.LogError($"CardService.{nameof(UpdateCardAsync)} has failed with exception message: {ex.Message}");
                return Result.Fail<CardResponse>(FailedResultMessage.Exception);
            }
        }

        public async Task<IResult<CardResponse>> GetCardAsync(Card cardEntity)
        {
            try
            {
                var card = await _cardRepository.GetCardByIdAsync(cardEntity.Id);

                if (card is null)
                {
                    _logger.LogError($"Fetch data from CardService.{nameof(GetCardAsync)} has failed for id:{0}.", cardEntity.Id);
                    return Result.Fail<CardResponse>(FailedResultMessage.NotFound);
                }

                return Result.Ok(card.ToResponse());
            }
            catch (Exception ex)
            {
                _logger.LogError($"CardService.{nameof(GetCardAsync)} has failed with exception message: {ex.Message}");
                return Result.Fail<CardResponse>(FailedResultMessage.Exception);
            }
        }

        public async Task<IResult<IEnumerable<CardResponse>>> GetPaginatedCardsAsync(int pageSize, int pageIndex, IEnumerable<Card> cards)
        {
            try
            {
                var getCards = await _cardRepository.GetCardsAsync(cards);
                var cardsCount = getCards.Count();
                if (cardsCount == 0)
                {
                    _logger.LogError($"Fetch data from CardService.{nameof(GetPaginatedCardsAsync)} has failed.");
                    return Result.Fail<IEnumerable<CardResponse>>(FailedResultMessage.NotFound);
                }

                // ordered by Name
                var orderedCardsOnPage = getCards.ToEnumerableResponse()
                    .OrderBy(c => c.Name)
                    .Skip(pageSize * pageIndex)
                    .Take(pageSize);

                var model = new PaginatedEntityResponseModel<CardResponse>(
                    pageIndex, pageSize, cardsCount, orderedCardsOnPage);

                return Result.Ok(model.Data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"CardService.{nameof(GetPaginatedCardsAsync)} has failed with exception message: {ex.Message}");
                return Result.Fail<IEnumerable<CardResponse>>(FailedResultMessage.Exception);
            }
        }
    }
}