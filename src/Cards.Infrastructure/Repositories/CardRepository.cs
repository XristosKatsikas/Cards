using Cards.Domain.Entities;
using Cards.Domain.Enums;
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

            // Create a HashSet to store the properties of cards for faster lookups
            var cardSet = new HashSet<(DateTime, Status, string, string)>();

            foreach (var card in cards)
            {
                cardSet.Add((card.DateCreated, card.Status, card.Color, card.Name));
            }

            var finalCardList = new List<Card>();

            foreach (var item in fetchAllCards)
            {
                if (cardSet.Contains((item.DateCreated, item.Status, item.Color, item.Name)))
                {
                    finalCardList.Add(item);
                }
            }

            return finalCardList;
        }

        public Card UpdateCard(Card card)
        {
            if (card is not null)
            {
                return Update(card);
            }
            return null!;
        }

        public bool DeleteCard(Card card)
        {
            if (card is not null)
            {
                var cardToDelete = Delete(card);
                if (cardToDelete is null)
                    return true;
            }
            return false;
        }
    }
}
