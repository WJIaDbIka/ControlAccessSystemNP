using DataLayer.Repository.Interfaces;

namespace DataLayer.UnitOfWork.Interface
{
	public interface IUnitOfWork
	{
		IAdministratorRepository AdministratorRepository { get; }

		IWorkerRepository WorkerRepository { get; }

		ICardRepository CardRepository { get; }

		Task SaveAsync();
	}
}
