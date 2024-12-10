using DataLayer.Entity;

namespace DataLayer.Repository.Interfaces
{
	public interface IWorkerRepository
	{
		Task CreateWorkerAsync(Worker entity);
		Task<Worker> ReadWorkerAsync(int id);
		Task<ICollection<Worker>> ReadAllWorkersAsync();
		Task UpdateWorkerAsync(Worker entity);
		Task DeleteWorkerAsync(Worker entity);
	}
}
