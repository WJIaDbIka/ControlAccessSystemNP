using BussinesLayer.DTO;

namespace BussinesLayer.Services.Interfaces
{
	public interface ICardService
	{
		Task CreateCardAsync(CardDTO model);
		Task<CardDTO> ReadCardAsync(int id);
		Task<ICollection<CardDTO>> ReadAllCardsAsync();
		Task UpdateCardAsync(CardDTO model);
		Task DeleteCardAsync(CardDTO model);
	}
}
