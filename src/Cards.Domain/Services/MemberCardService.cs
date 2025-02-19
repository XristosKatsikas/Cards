﻿using Cards.Core;
using Cards.Domain.DTOs.Requests;
using Cards.Domain.DTOs.Requests.Member;
using Cards.Domain.DTOs.Requests.Member.Validators;
using Cards.Domain.DTOs.Requests.Validators;
using Cards.Domain.DTOs.Responses;
using Cards.Domain.Enums;
using Cards.Domain.Mappers;
using Cards.Domain.Services.Abstractions;
using FluentResults;
using Microsoft.Extensions.Logging;

namespace Cards.Domain.Services
{
    public class MemberCardService : IMemberCardService
    {
        private readonly ILogger<MemberCardService> _logger;
        private readonly ICardService _cardService;

        public MemberCardService(ICardService cardService, ILogger<MemberCardService> logger)
        {
            _cardService = cardService;
            _logger = logger;
        }

        public async Task<IResult<CardResponse>> AddCardAsync(AddMemberCardRequest request)
        {
            var validator = new AddMemberCardRequestValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(val => val.ErrorMessage).ToList();
                _logger.LogError($"Validation errors occurred in MemberCardService.{nameof(AddCardAsync)}: " +
                    $"{string.Join(", ", errorMessages)}");

                return Result.Fail<CardResponse>(FailedResultMessage.RequestValidation);
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
                var errorMessages = validationResult.Errors.Select(val => val.ErrorMessage).ToList();
                _logger.LogError($"Validation errors occurred in MemberCardService.{nameof(DeleteCardAsync)}: " +
                    $"{string.Join(", ", errorMessages)}");

                return Result.Fail<bool>(FailedResultMessage.RequestValidation);
            }

            var cardEntity = CardMapper.ToEntity(request);

            if (!cardEntity.Role.Equals(Role.Member.ToString()))
            {
                _logger.LogError($"Delete data from MemberCardService.{nameof(DeleteCardAsync)} has failed due to role error.");
                return Result.Fail<bool>(FailedResultMessage.Unprocessable);
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
                var errorMessages = validationResult.Errors.Select(val => val.ErrorMessage).ToList();
                _logger.LogError($"Validation errors occurred in MemberCardService.{nameof(GetCardAsync)}: " +
                    $"{string.Join(", ", errorMessages)}");

                return Result.Fail<CardResponse>(FailedResultMessage.RequestValidation);
            }

            var cardEntity = CardMapper.ToEntity(request);

            if (!cardEntity.Role.Equals(Role.Member.ToString()))
            {
                _logger.LogError($"Fetch data from MemberCardService.{nameof(GetCardAsync)} has failed due to role error.");
                return Result.Fail<CardResponse>(FailedResultMessage.Unprocessable);
            }

            return await _cardService.GetCardAsync(cardEntity);
        }

        public async Task<IResult<IEnumerable<CardResponse>>> GetPaginatedCardsAsync(int pageSize, int pageIndex, GetCardsRequest request)
        {
            var validator = new GetCardsRequestValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(val => val.ErrorMessage).ToList();
                _logger.LogError($"Validation errors occurred in MemberCardService.{nameof(GetPaginatedCardsAsync)}: " +
                    $"{string.Join(", ", errorMessages)}");

                return Result.Fail<IEnumerable<CardResponse>>(FailedResultMessage.RequestValidation);
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
                var errorMessages = validationResult.Errors.Select(val => val.ErrorMessage).ToList();
                _logger.LogError($"Validation errors occurred in MemberCardService.{nameof(UpdateCardAsync)}: " +
                    $"{string.Join(", ", errorMessages)}");

                return Result.Fail<CardResponse>(FailedResultMessage.RequestValidation);
            }
            var cardEntity = CardMapper.ToEntity(request);

            if (!cardEntity.Role.Equals(Role.Member.ToString()))
            {
                _logger.LogError($"Update data from MemberCardService.{nameof(UpdateCardAsync)} has failed due to role error.");
                return Result.Fail<CardResponse>(FailedResultMessage.Unprocessable);
            }

            return await _cardService.UpdateCardAsync(cardEntity);
        }
    }
}