using DataLayer.Entity;

namespace DataLayer.Repository.Interfaces
{
	public interface IAdministratorRepository
	{
		Task CreateAdministratorAsync(Administrator entity);
		Task ReadAdministratorAsync(int id);
		Task ReadAllAdministratorsAsync();
		Task UpdateAdministratorAsync(Administrator entity);
		Task DeleteAdministratorAsync(Administrator entity);
	}
}
