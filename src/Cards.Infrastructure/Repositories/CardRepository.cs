using Cards.Domain.Entities;
using Cards.Domain.Repositories.Abstractions;

namespace Cards.Infrastructure.Repositories
{
    public class CardRepository : GenericRepository<Card>, ICardRepository
    {
        private readonly ApplicationDbContext _context;

        public IUnitOfWork UnitOfWork => _context;

        public CardRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public Card AddCard(Card card)
        {
            if (card is not null)
            {
                return AddCard(card);
            }
            return null!;
        }

        public async Task<Card> GetCardByIdAsync(Guid id)
        {
            return await GetAsyncById(id) ?? throw new InvalidDataException();
        }

        public async Task<IEnumerable<Card>> GetCardsAsync(IEnumerable<Card> cards)
        {
            var fetchAllCards =  await GetAllAsync();

            var finalCards = fetchAllCards
                .Join(cards,
                    fetchedCard => new { fetchedCard.Name, fetchedCard.DateCreated, fetchedCard.Color, fetchedCard.Status },
                    card => new { card.Name, card.DateCreated, card.Color, card.Status },
                    (fetchedCard, card) => fetchedCard)
                .ToList();

            return finalCards;
        }

        public Card UpdateCard(Card card)
        {
            if (card is not null)
            {
                return Update(card);
            }
            return null!;
        }

        public Card DeleteCard(Card card)
        {
            if (card is not null)
            {
                return Delete(card);
            }
            return null!;
        }
    }
}
