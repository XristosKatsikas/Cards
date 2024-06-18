using Cards.Cache.Helpers;
using Cards.Cache.Services.Abstractions;
using Cards.Domain.DTOs.Requests;
using Cards.Domain.DTOs.Requests.Admin;
using Cards.Domain.DTOs.Responses;
using Cards.Domain.Services.Abstractions;
using FluentResults;

namespace Cards.Domain.Services
{
    public class AdminCardServiceDecorator : IAdminCardService
    {
        private readonly IAdminCardService _adminCardService;
        private readonly IRedisCacheService _redisCacheService;

        public AdminCardServiceDecorator(IAdminCardService adminCardService, IRedisCacheService redisCacheService)
        {
            _adminCardService = adminCardService;
            _redisCacheService = redisCacheService;
        }

        public async Task<IResult<CardResponse>> AddCardAsync(AddAdminCardRequest request)
        {
            return await _adminCardService.AddCardAsync(request);
        }

        public async Task<IResult<CardResponse>> DeleteCardAsync(DeleteCardRequest request)
        {
            return await _adminCardService.DeleteCardAsync(request);
        }

        public async Task<IResult<CardResponse>> GetCardAsync(GetCardRequest request)
        {
            return await _adminCardService.GetCardAsync(request);
        }

        public async Task<IResult<IEnumerable<CardResponse>>> GetPaginatedCardsAsync(int pageSize, int pageIndex, GetCardsRequest request)
        {
            var cacheKey = request.Name;

            var cacheData = await _redisCacheService.GetDataAsync<IResult<IEnumerable<CardResponse>>>(cacheKey);

            if (cacheData is not null)
            {
                return cacheData;
            }

            var response = await _adminCardService.GetPaginatedCardsAsync(pageSize, pageIndex, request);

            if (!response.IsSuccess)
            {
                return response;
            }

            // cache data
            await _redisCacheService.SetDataAsync(cacheKey, response, CacheDuration.OneMinute);

            return response;
        }

        public async Task<IResult<CardResponse>> UpdateCardAsync(UpdateCardRequest request)
        {
            return await _adminCardService.UpdateCardAsync(request);
        }
    }
}
