using Cards.Domain.Entities;
using Cards.Domain.Repositories.Abstractions;
using Cards.Domain.DTOs.Requests;

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

        public async Task<IEnumerable<Card>> GetCardsAsync(GetCardsRequest request)
        {
            var getAllCards =  await GetAllAsync();

            var cards = getAllCards
                .Where(x => x.DateCreated == request.DateCreated)
                .Where(x => x.Status.ToString() == request.Status)
                .Where(x => x.Color == request.Color)
                .Where(x => x.Name == request.Name);

            return cards;
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
