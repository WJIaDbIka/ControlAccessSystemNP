using BussinesLayer.DTO;


namespace BussinesLayer.Services.Interfaces
{
	public interface IAdministratorService
	{
		Task CreateAdministratorAsync(AdministratorDTO model);
		Task<AdministratorDTO> ReadAdministratorAsync(int id);
		Task<ICollection<AdministratorDTO>> ReadAllAdministratorsAsync();
		Task UpdateAdministratorAsync(AdministratorDTO model);
		Task DeleteAdministratorAsync(AdministratorDTO model);
	}
}
