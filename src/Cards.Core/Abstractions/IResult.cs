namespace Cards.Core.Abstractions
{
    public interface IResult<T> : IResultStatus
    {
        T Data { get; }
    }
}
