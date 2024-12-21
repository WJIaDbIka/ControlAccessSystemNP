using AutoMapper;
using BussinesLayer.DTO;
using BussinesLayer.Services.Interfaces;
using CCL.Security;
using CCL.Security.Identity;
using DataLayer.Entity;
using DataLayer.UnitOfWork.Interface;

namespace BussinesLayer.Services
{
	public class CardService : ICardService
	{
		private readonly IUnitOfWork _db;
		private readonly IMapper _mapper;

        public CardService(IUnitOfWork unitOfWork)
        {
			if (unitOfWork == null)
			{
				throw new ArgumentNullException(nameof(unitOfWork));
			}

            _db = unitOfWork;

			_mapper = new MapperConfiguration(
				cfg => cfg.CreateMap<Card, CardDTO>()
				).CreateMapper();
		}

        public async Task CreateCardAsync(CardDTO model)
		{
			var user = SecurityContext.GetUser();
			var userType = user.GetType();
			if (userType != typeof(Admin))
			{
				throw new MethodAccessException();
			}

			await _db.CardRepository.CreateCardAsync(_mapper.Map<CardDTO, Card>(model));
			await _db.SaveAsync();
		}

		public async Task DeleteCardAsync(CardDTO model)
		{
			var user = SecurityContext.GetUser();
			var userType = user.GetType();
			if (userType != typeof(Admin))
			{
				throw new MethodAccessException();
			}

			await _db.CardRepository.DeleteCardAsync(_mapper.Map<CardDTO, Card>(model));
			await _db.SaveAsync();
		}

		public async Task<ICollection<CardDTO>> ReadAllCardsAsync()
		{
			var user = SecurityContext.GetUser();
			var userType = user.GetType();
			if (userType != typeof(Admin))
			{
				throw new MethodAccessException();
			}

			var cards = await _db.CardRepository.ReadAllCardsAsync();
			return _mapper.Map<IEnumerable<Card>, List<CardDTO>>(cards);
		}

		public async Task<CardDTO> ReadCardAsync(int id)
		{
			var user = SecurityContext.GetUser();
			var userType = user.GetType();
			if (userType != typeof(Admin) && userType != typeof(WorkerIdentity))
			{
				throw new MethodAccessException();
			}

			var card = await _db.CardRepository.ReadCardAsync(id);
			return _mapper.Map<Card, CardDTO>(card);
		}

		public async Task UpdateCardAsync(CardDTO model)
		{
			var user = SecurityContext.GetUser();
			var userType = user.GetType();
			if (userType != typeof(Admin))
			{
				throw new MethodAccessException();
			}

			await _db.CardRepository.UpdateCardAsync(_mapper.Map<CardDTO, Card>(model));
			await _db.SaveAsync();
		}
	}
}
