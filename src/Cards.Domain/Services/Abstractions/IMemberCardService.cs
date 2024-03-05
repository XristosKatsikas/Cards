using Cards.Domain.DTOs.Requests;
using Cards.Domain.DTOs.Requests.Member;
using Cards.Domain.DTOs.Responses;
using FluentResults;

namespace Cards.Domain.Services.Abstractions
{
    public interface IMemberCardService
    {
        Task<IResult<CardResponse>> AddCardAsync(AddMemberCardRequest request);

        Task<IResult<CardResponse>> DeleteCardAsync(Guid id);

        Task<IResult<CardResponse>> UpdateCardAsync(UpdateCardRequest request);

        Task<IResult<CardResponse>> GetCardAsync(Guid id);

        Task<IResult<IEnumerable<CardResponse>>> GetPaginatedCardsAsync(int pageSize, int pageIndex, GetCardsRequest request);
    }
}
