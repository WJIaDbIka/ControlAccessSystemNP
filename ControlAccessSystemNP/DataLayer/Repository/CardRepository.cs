using DataLayer.EF;
using DataLayer.Entity;
using DataLayer.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repository
{
	public sealed class CardRepository : ICardRepository
	{
		private readonly ControlAccessSystemContext _context;
		private readonly DbSet<Card> _cards;

        public CardRepository(ControlAccessSystemContext context)
        {
            _context = context;
			_cards = _context.Set<Card>();
        }

        public async Task CreateCardAsync(Card entity)
		{
			await _cards.AddAsync(entity);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteCardAsync(Card entity)
		{
			_cards.Remove(entity);
			await _context.SaveChangesAsync();
		}

		public async Task<ICollection<Card>> ReadAllCardsAsync()
		{
			return await _cards.ToListAsync();
		}

		public async Task<Card> ReadCardAsync(int id)
		{
			return await _cards.FindAsync(id);
		}

		public async Task UpdateCardAsync(Card entity)
		{
			_cards.Update(entity);
			await _context.SaveChangesAsync();
		}
	}
}
