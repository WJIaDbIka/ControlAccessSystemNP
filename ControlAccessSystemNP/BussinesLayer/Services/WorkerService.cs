using AutoMapper;
using BussinesLayer.DTO;
using BussinesLayer.Services.Interfaces;
using CCL.Security.Identity;
using CCL.Security;
using DataLayer.Entity;
using DataLayer.UnitOfWork.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLayer.Services
{
	public class WorkerService : IWorkerService
	{
		private readonly IUnitOfWork _db;
		private readonly IMapper _mapper;

        public WorkerService(IUnitOfWork unitOfWork)
        {
			if (unitOfWork == null)
			{
				throw new ArgumentNullException(nameof(unitOfWork));
			}

			_db = unitOfWork;

			_mapper = new MapperConfiguration(
				cfg => cfg.CreateMap<Worker, WorkerDTO>()
				).CreateMapper();
		}

        public async Task CreateWorkerAsync(WorkerDTO model)
		{
			var user = SecurityContext.GetUser();
			var userType = user.GetType();
			if (userType != typeof(Admin))
			{
				throw new MethodAccessException();
			}

			await _db.WorkerRepository.CreateWorkerAsync(_mapper.Map<WorkerDTO, Worker>(model));
			await _db.SaveAsync();
		}

		public async Task DeleteWorkerAsync(WorkerDTO model)
		{
			var user = SecurityContext.GetUser();
			var userType = user.GetType();
			if (userType != typeof(Admin))
			{
				throw new MethodAccessException();
			}

			await _db.WorkerRepository.DeleteWorkerAsync(_mapper.Map<WorkerDTO, Worker>(model));
			await _db.SaveAsync();
		}

		public async Task<ICollection<WorkerDTO>> ReadAllWorkersAsync()
		{
			var user = SecurityContext.GetUser();
			var userType = user.GetType();
			if (userType != typeof(Admin))
			{
				throw new MethodAccessException();
			}

			var workers = await _db.WorkerRepository.ReadAllWorkersAsync();
			return _mapper.Map<IEnumerable<Worker>, List<WorkerDTO>>(workers);
		}

		public async Task<WorkerDTO> ReadWorkerAsync(int id)
		{
			var user = SecurityContext.GetUser();
			var userType = user.GetType();
			if (userType != typeof(Admin) && userType != typeof(WorkerIdentity))
			{
				throw new MethodAccessException();
			}

			var worker = await _db.WorkerRepository.ReadWorkerAsync(id);
			return _mapper.Map<Worker, WorkerDTO>(worker);
		}

		public async Task UpdateWorkerAsync(WorkerDTO model)
		{
			var user = SecurityContext.GetUser();
			var userType = user.GetType();
			if (userType != typeof(Admin))
			{
				throw new MethodAccessException();
			}

			await _db.WorkerRepository.UpdateWorkerAsync(_mapper.Map<WorkerDTO, Worker>(model));
			await _db.SaveAsync();
		}
	}
}
