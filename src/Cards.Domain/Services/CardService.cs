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
                    return (IResult<CardResponse>)Result.Fail(string.Format("Bad request for card entity with id: {0}", cardEntity.Id));
                }

                await _cardRepository.UnitOfWork.SaveChangesAsync();

                return Result.Ok(card.ToResponse());
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception was thrown from {nameof(AddCardAsync)}");

                return (IResult<CardResponse>)Result.Fail(ex.Message);
            }
        }

        public async Task<IResult<bool>> DeleteCardAsync(Card cardEntity)
        {
            try
            {
                var isCardDeleted = _cardRepository.DeleteCard(cardEntity);

                if (!isCardDeleted)
                {
                    return (IResult<bool>)Result.Fail(string.Format("Bad request for card entity with id: {0}", cardEntity.Id));
                }

                await _cardRepository.UnitOfWork.SaveChangesAsync();

                return Result.Ok(isCardDeleted);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception was thrown from {nameof(DeleteCardAsync)}");

                return (IResult<bool>)Result.Fail(ex.Message);
            }
        }

        public async Task<IResult<CardResponse>> UpdateCardAsync(Card cardEntity)
        {
            try
            {
                var card = _cardRepository.UpdateCard(cardEntity);
                
                if (card is null)
                {
                    return (IResult<CardResponse>)Result.Fail(string.Format("Bad request for card entity with id: {0}", cardEntity.Id));
                }

                await _cardRepository.UnitOfWork.SaveChangesAsync();

                return Result.Ok(card.ToResponse());
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception was thrown from {nameof(UpdateCardAsync)}");

                return (IResult<CardResponse>)Result.Fail(ex.Message);
            }
        }

        public async Task<IResult<CardResponse>> GetCardAsync(Card cardEntity)
        {
            try
            {
                var card = await _cardRepository.GetCardByIdAsync(cardEntity.Id);

                if (card is null)
                {
                    return (IResult<CardResponse>)Result.Fail(string.Format("Bad request for card entity with id: {0}", cardEntity.Id));
                }

                return Result.Ok(card.ToResponse());
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception was thrown from {nameof(GetCardAsync)}");

                return (IResult<CardResponse>)Result.Fail(ex.Message);
            }
        }

        public async Task<IResult<IEnumerable<CardResponse>>> GetPaginatedCardsAsync(int pageSize, int pageIndex, IEnumerable<Card> cards)
        {
            try
            {
                var getCards = await _cardRepository.GetCardsAsync(cards);
                var cardsCount = getCards.Count();

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
                _logger.LogError($"Exception was thrown from {nameof(GetPaginatedCardsAsync)}");

                return (IResult<IEnumerable<CardResponse>>)Result.Fail(ex.Message);
            }
        }
    }
}