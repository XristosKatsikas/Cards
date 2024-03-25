using Cards.Domain.Entities;

namespace Cards.Domain.Repositories.Abstractions
{
    public interface ICardRepository
    {
        Task<IEnumerable<Card>> GetCardsAsync(IEnumerable<Card> cards);
        Task<Card> GetCardByIdAsync(Guid id);
        Card AddCard(Card card);
        Card UpdateCard(Card card);
        Card DeleteCard(Card card);
        IUnitOfWork UnitOfWork { get; }
    }
}
