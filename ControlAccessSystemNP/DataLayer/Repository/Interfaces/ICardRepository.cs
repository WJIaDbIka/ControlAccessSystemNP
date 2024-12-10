using DataLayer.Entity;

namespace DataLayer.Repository.Interfaces
{
	public interface ICardRepository
	{
		Task CreateCardAsync(Card entity);
		Task<Card> ReadCardAsync(int id);
		Task<ICollection<Card>> ReadAllCardsAsync();
		Task UpdateCardAsync(Card entity);
		Task DeleteCardAsync(Card entity);
	}
}
