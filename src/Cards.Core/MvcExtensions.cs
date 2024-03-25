using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace Cards.Core
{
    public static class MvcExtensions
    {
        public static ObjectResult ApiResponse<T>(this ControllerBase controller, IResult<T> result)
        {
            if (controller == null)
            {
                throw new ArgumentNullException(nameof(controller));
            }

            return new ObjectResult(result);
        }
    }
}
