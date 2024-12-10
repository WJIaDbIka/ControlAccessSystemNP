using DataLayer.EF;
using DataLayer.Entity;
using DataLayer.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repository
{
	public sealed class AdministratorRepository : IAdministratorRepository
	{
		private readonly ControlAccessSystemContext _context;
		private readonly DbSet<Administrator> _administrators;

        public AdministratorRepository(ControlAccessSystemContext context)
        {
            _context = context;
			_administrators = _context.Set<Administrator>();
        }

        public async Task CreateAdministratorAsync(Administrator entity)
		{
			await _administrators.AddAsync(entity);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteAdministratorAsync(Administrator entity)
		{
			_administrators.Remove(entity);
			await _context.SaveChangesAsync();
		}

		public async Task<Administrator> ReadAdministratorAsync(int id)
		{
			return await _administrators.FindAsync(id);
		}

		public async Task<ICollection<Administrator>> ReadAllAdministratorsAsync()
		{
			return await _administrators.ToListAsync();
		}

		public async Task UpdateAdministratorAsync(Administrator entity)
		{
			_administrators.Update(entity);
			await _context.SaveChangesAsync();
		}
	}
}
