using Cards.Domain.DTOs.Responses;
using Cards.Domain.Entities;
using FluentResults;

namespace Cards.Domain.Services.Abstractions
{
    public interface ICardService
    {
        Task<IResult<CardResponse>> AddCardAsync(Card card);

        Task<IResult<bool>> DeleteCardAsync(Card card);

        Task<IResult<CardResponse>> UpdateCardAsync(Card card);

        Task<IResult<CardResponse>> GetCardAsync(Card card);

        Task<IResult<IEnumerable<CardResponse>>> GetPaginatedCardsAsync(int pageSize, int pageIndex, IEnumerable<Card> cards);
    }
}