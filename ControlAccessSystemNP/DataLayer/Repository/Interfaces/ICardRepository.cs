using DataLayer.Entity;

namespace DataLayer.Repository.Interfaces
{
	public interface ICardRepository
	{
		Task CreateCardAsync(Card entity);
		Task ReadCardAsync(int id);
		Task ReadAllCardsAsync();
		Task UpdateCardAsync(Card entity);
		Task DeleteCardAsync(Card entity);
	}
}
