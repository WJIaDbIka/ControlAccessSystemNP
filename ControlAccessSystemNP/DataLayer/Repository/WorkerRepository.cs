using DataLayer.EF;
using DataLayer.Entity;
using DataLayer.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repository
{
	public sealed class WorkerRepository : IWorkerRepository
	{
		private readonly ControlAccessSystemContext _context;
		private readonly DbSet<Worker> _workers;

        public WorkerRepository(ControlAccessSystemContext context)
        {
            _context = context;
			_workers = _context.Set<Worker>();
        }

        public async Task CreateWorkerAsync(Worker entity)
		{
			await _workers.AddAsync(entity);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteWorkerAsync(Worker entity)
		{
			_workers.Remove(entity);
			await _context.SaveChangesAsync();
		}

		public async Task<ICollection<Worker>> ReadAllWorkersAsync()
		{
			return await _workers.ToListAsync();
		}

		public async Task<Worker> ReadWorkerAsync(int id)
		{
			return await _workers.FindAsync(id);
		}

		public async Task UpdateWorkerAsync(Worker entity)
		{
			_workers.Update(entity);
			await _context.SaveChangesAsync();
		}
	}
}
