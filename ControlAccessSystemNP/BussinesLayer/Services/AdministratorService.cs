using AutoMapper;
using BussinesLayer.DTO;
using BussinesLayer.Services.Interfaces;
using CCL.Security.Identity;
using CCL.Security;
using DataLayer.Entity;
using DataLayer.UnitOfWork.Interface;

namespace BussinesLayer.Services
{
	public class AdministratorService : IAdministratorService
	{
		private readonly IUnitOfWork _db;
		private readonly IMapper _mapper;

        public AdministratorService(IUnitOfWork unitOfWork)
        {
			if (unitOfWork == null)
			{
				throw new ArgumentNullException(nameof(unitOfWork));
			}

			_db = unitOfWork;

			_mapper = new MapperConfiguration(
				cfg => cfg.CreateMap<Administrator, AdministratorDTO>()
				).CreateMapper();
		}

        public async Task CreateAdministratorAsync(AdministratorDTO model)
		{
			var user = SecurityContext.GetUser();
			var userType = user.GetType();
			if (userType != typeof(Admin))
			{
				throw new MethodAccessException();
			}

			await _db.AdministratorRepository.CreateAdministratorAsync(_mapper.Map<AdministratorDTO, Administrator>(model));
			await _db.SaveAsync();
		}

		public async Task DeleteAdministratorAsync(AdministratorDTO model)
		{
			var user = SecurityContext.GetUser();
			var userType = user.GetType();
			if (userType != typeof(Admin))
			{
				throw new MethodAccessException();
			}

			await _db.AdministratorRepository.DeleteAdministratorAsync(_mapper.Map<AdministratorDTO, Administrator>(model));
			await _db.SaveAsync();
		}

		public async Task<AdministratorDTO> ReadAdministratorAsync(int id)
		{
			var user = SecurityContext.GetUser();
			var userType = user.GetType();
			if (userType != typeof(Admin) && userType != typeof(WorkerIdentity))
			{
				throw new MethodAccessException();
			}

			var admin = await _db.AdministratorRepository.ReadAdministratorAsync(id);
			return _mapper.Map<Administrator, AdministratorDTO>(admin);
		}

		public async Task<ICollection<AdministratorDTO>> ReadAllAdministratorsAsync()
		{
			var user = SecurityContext.GetUser();
			var userType = user.GetType();
			if (userType != typeof(Admin))
			{
				throw new MethodAccessException();
			}

			var admins = await _db.AdministratorRepository.ReadAllAdministratorsAsync();
			return _mapper.Map<IEnumerable<Administrator>, List<AdministratorDTO>>(admins);
		}

		public async Task UpdateAdministratorAsync(AdministratorDTO model)
		{
			var user = SecurityContext.GetUser();
			var userType = user.GetType();
			if (userType != typeof(Admin))
			{
				throw new MethodAccessException();
			}

			await _db.AdministratorRepository.UpdateAdministratorAsync(_mapper.Map<AdministratorDTO, Administrator>(model));
			await _db.SaveAsync();
		}
	}
}
