using Cards.Core;
using Cards.Core.Abstractions;
using Cards.Domain.DTOs.Responses;

namespace Cards.Domain.Helpers
{
    public static class FailedResponse
    {
        public static async Task<IResult<CardResponse>> GetUnprocessableValidationResultAsync(FluentValidation.Results.ValidationResult validationResult)
        {
            return Result<CardResponse>.CreateFailed(
                    ResultCode.UnprocessableEntity,
                    validationResult.Errors.Select(val => val.ErrorMessage).ToList());
        }

        public static async Task<IResult<CardResponse>> GetForbiddenResultAsync()
        {
            return Result<CardResponse>.CreateFailed(
                    ResultCode.Forbidden,
                    "Requested entity must not be fetched");
        }

        public static async Task<IResult<IEnumerable<CardResponse>>> GetEnumerableUnprocessableValidationResultAsync(FluentValidation.Results.ValidationResult validationResult)
        {
            return Result<IEnumerable<CardResponse>>.CreateFailed(
                    ResultCode.UnprocessableEntity,
                    validationResult.Errors.Select(val => val.ErrorMessage).ToList());
        }

        public static async Task<IResult<CardResponse>> GetBadRequestResultAsync(Guid id)
        {
            return Result<CardResponse>.CreateFailed(
                        ResultCode.BadRequest,
                        string.Format("Bad create request for card entity with Id {0}", id));
        }
    }
}
