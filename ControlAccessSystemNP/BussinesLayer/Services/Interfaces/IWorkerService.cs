using BussinesLayer.DTO;
using DataLayer.Entity;

namespace BussinesLayer.Services.Interfaces
{
	public interface IWorkerService
	{
		Task CreateWorkerAsync(WorkerDTO model);
		Task<WorkerDTO> ReadWorkerAsync(int id);
		Task<ICollection<WorkerDTO>> ReadAllWorkersAsync();
		Task UpdateWorkerAsync(WorkerDTO model);
		Task DeleteWorkerAsync(WorkerDTO model);
	}
}
