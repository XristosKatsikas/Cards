using Cards.Core.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Cards.Core
{
    public static class MvcExtensions
    {
        public static CardsObjectResult<T> ApiResponse<T>(this ControllerBase controller, IResult<T> result)
        {
            if (controller == null)
            {
                throw new ArgumentNullException(nameof(controller));
            }

            return new CardsObjectResult<T>(result);
        }
    }
}
