using Microsoft.Extensions.Logging;

namespace Cards.Core.Abstractions
{
    public interface IResultStatus
    {
        public ILogger Logger { get; set; }

        bool Success { get; }

        int StatusCode { get; }

        List<string> Errors { get; }
    }
}
