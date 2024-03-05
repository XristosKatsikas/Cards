using Cards.Core.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cards.Core
{
    public class CardsObjectResult<T> : ObjectResult
    {
        private readonly IResultStatus _status;

        public CardsObjectResult(IResult<T> result) : base(null)
        {
            if (result == null)
            {
                throw new ArgumentNullException(nameof(result));
            }

            if (!result.Success && result.StatusCode < 400)
            {
                throw new ArgumentOutOfRangeException(nameof(result.StatusCode));
            }

            if (result.Success)
            {
                Value = result.Data;
                StatusCode = result.StatusCode;
            }
            else
            {
                StatusCode = StatusCodes.Status500InternalServerError;

                if (result.StatusCode >= 400 && result.StatusCode < 599)
                {
                    StatusCode = result.StatusCode;
                }

                Value = new { status = StatusCode, errors = result.Errors };
            }

            _status = result;
        }

        public override void OnFormatting(ActionContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            base.OnFormatting(context);
        }
    }
}
