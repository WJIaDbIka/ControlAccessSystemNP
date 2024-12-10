using DataLayer.EF;
using DataLayer.Repository;
using DataLayer.Repository.Interfaces;
using DataLayer.UnitOfWork.Interface;

namespace DataLayer.UnitOfWork
{
	public sealed class UnitOfWork : IUnitOfWork
	{
		private readonly ControlAccessSystemContext _context;
		private readonly IAdministratorRepository _administratorRepository;
		private readonly IWorkerRepository _workerRepository;
		private readonly ICardRepository _cardRepository;

        public UnitOfWork(ControlAccessSystemContext context)
        {
            _context = context;
			_administratorRepository = new AdministratorRepository(context);
			_workerRepository = new WorkerRepository(context);
			_cardRepository = new CardRepository(context);
        }

		public IAdministratorRepository AdministratorRepository => _administratorRepository;

		public IWorkerRepository WorkerRepository => _workerRepository;

		public ICardRepository CardRepository => _cardRepository;

		public async Task SaveAsync()
		{
			await _context.SaveChangesAsync();
		}
	}
}
