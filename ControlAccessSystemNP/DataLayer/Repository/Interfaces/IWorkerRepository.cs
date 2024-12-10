using DataLayer.Entity;

namespace DataLayer.Repository.Interfaces
{
	public interface IWorkerRepository
	{
		Task CreateWorkerAsync(Worker entity);
		Task ReadWorkerAsync(int id);
		Task ReadAllWorkersAsync();
		Task UpdateWorkerAsync(Worker entity);
		Task DeleteWorkerAsync(Worker entity);
	}
}
