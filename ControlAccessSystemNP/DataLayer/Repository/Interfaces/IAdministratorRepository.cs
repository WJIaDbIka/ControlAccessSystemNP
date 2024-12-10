using DataLayer.Entity;

namespace DataLayer.Repository.Interfaces
{
	public interface IAdministratorRepository
	{
		Task CreateAdministratorAsync(Administrator entity);
		Task<Administrator> ReadAdministratorAsync(int id);
		Task<ICollection<Administrator>> ReadAllAdministratorsAsync();
		Task UpdateAdministratorAsync(Administrator entity);
		Task DeleteAdministratorAsync(Administrator entity);
	}
}
