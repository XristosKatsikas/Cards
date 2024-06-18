using Cards.Domain.DTOs.Requests;
using Cards.Domain.DTOs.Requests.Admin;
using Cards.Domain.DTOs.Responses;
using FluentResults;

namespace Cards.Domain.Services.Abstractions
{
    public interface IAdminCardService
    {
        Task<IResult<CardResponse>> AddCardAsync(AddAdminCardRequest request);

        Task<IResult<bool>> DeleteCardAsync(DeleteCardRequest request);

        Task<IResult<CardResponse>> UpdateCardAsync(UpdateCardRequest request);

        Task<IResult<CardResponse>> GetCardAsync(GetCardRequest request);

        Task<IResult<IEnumerable<CardResponse>>> GetPaginatedCardsAsync(int pageSize, int pageIndex, GetCardsRequest request);
    }
}
