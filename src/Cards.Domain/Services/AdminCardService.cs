using Cards.Core;
using Cards.Domain.DTOs.Requests;
using Cards.Domain.DTOs.Requests.Admin;
using Cards.Domain.DTOs.Requests.Admin.Validators;
using Cards.Domain.DTOs.Requests.Validators;
using Cards.Domain.DTOs.Responses;
using Cards.Domain.Mappers;
using Cards.Domain.Services.Abstractions;
using FluentResults;
using Microsoft.Extensions.Logging;

namespace Cards.Domain.Services
{
    public class AdminCardService : IAdminCardService
    {
        private readonly ILogger<AdminCardService> _logger;
        private readonly ICardService _cardService;

        public AdminCardService(ICardService cardService, ILogger<AdminCardService> logger)
        {
            _cardService = cardService;
            _logger = logger;
        }

        public async Task<IResult<CardResponse>> AddCardAsync(AddAdminCardRequest request)
        {
            var validator = new AddAdminCardRequestValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(val => val.ErrorMessage).ToList();
                _logger.LogError($"Validation errors occurred in AdminCardService.{nameof(AddCardAsync)}: " +
                    $"{string.Join(", ", errorMessages)}");

                return Result.Fail<CardResponse>(FailedResultMessage.RequestValidation);
            }

            var cardEntity = CardMapper.ToEntity(request);

            try
            {
                return await _cardService.AddCardAsync(cardEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError($"AdminCardService.{nameof(AddCardAsync)} has failed with exception message: {ex.Message}");
                throw;
            }
        }

        public async Task<IResult<bool>> DeleteCardAsync(DeleteCardRequest request)
        {
            var validator = new DeleteCardRequestValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(val => val.ErrorMessage).ToList();
                _logger.LogError($"Validation errors occurred in AdminCardService.{nameof(DeleteCardAsync)}: " +
                    $"{string.Join(", ", errorMessages)}");

                return Result.Fail<bool>(FailedResultMessage.RequestValidation);
            }

            var cardEntity = CardMapper.ToEntity(request);
            try
            {
                return await _cardService.DeleteCardAsync(cardEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError($"AdminCardService.{nameof(DeleteCardAsync)} has failed with exception message: {ex.Message}");
                throw;
            }
        }

        public async Task<IResult<CardResponse>> GetCardAsync(GetCardRequest request)
        {
            var validator = new GetCardRequestValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(val => val.ErrorMessage).ToList();
                _logger.LogError($"Validation errors occurred in AdminCardService.{nameof(GetCardAsync)}: " +
                    $"{string.Join(", ", errorMessages)}");

                return Result.Fail<CardResponse>(FailedResultMessage.RequestValidation);
            }

            var cardEntity = CardMapper.ToEntity(request);

            try
            {
                return await _cardService.GetCardAsync(cardEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError($"AdminCardService.{nameof(GetCardAsync)} has failed with exception message: {ex.Message}");
                throw;
            }
        }

        public async Task<IResult<IEnumerable<CardResponse>>> GetPaginatedCardsAsync(int pageSize, int pageIndex, GetCardsRequest request)
        {
            var validator = new GetCardsRequestValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(val => val.ErrorMessage).ToList();
                _logger.LogError($"Validation errors occurred in AdminCardService.{nameof(GetPaginatedCardsAsync)}: " +
                    $"{string.Join(", ", errorMessages)}");

                return Result.Fail<IEnumerable<CardResponse>>(FailedResultMessage.RequestValidation);
            }

            var cardEntity = CardMapper.ToEntity(request);

            try
            {
                return await _cardService.GetPaginatedCardsAsync(pageSize, pageIndex, cardEntity);

            }
            catch (Exception ex)
            {
                _logger.LogError($"AdminCardService.{nameof(GetPaginatedCardsAsync)} has failed with exception message: {ex.Message}");
                throw;
            }
        }

        public async Task<IResult<CardResponse>> UpdateCardAsync(UpdateCardRequest request)
        {
            var validator = new UpdateCardRequestValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(val => val.ErrorMessage).ToList();
                _logger.LogError($"Validation errors occurred in AdminCardService.{nameof(UpdateCardAsync)}: " +
                    $"{string.Join(", ", errorMessages)}");

                return Result.Fail<CardResponse>(FailedResultMessage.RequestValidation);
            }
            var cardEntity = CardMapper.ToEntity(request);

            try
            {
                return await _cardService.UpdateCardAsync(cardEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError($"AdminCardService.{nameof(UpdateCardAsync)} has failed with exception message: {ex.Message}");
                throw;
            }
        }
    }
}