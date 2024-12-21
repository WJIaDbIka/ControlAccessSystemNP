using AutoMapper;
using BussinesLayer.DTO;
using BussinesLayer.Services;
using CCL.Security.Identity;
using CCL.Security;
using DataLayer.Entity;
using DataLayer.UnitOfWork.Interface;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLayer.Test
{
	public class WorkerServiceTests
	{
		private readonly Mock<IUnitOfWork> _unitOfWorkMock;
		private readonly IMapper _mapper;
		private readonly WorkerService _service;

		public WorkerServiceTests()
		{
			_unitOfWorkMock = new Mock<IUnitOfWork>();

			var mapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<Worker, WorkerDTO>().ReverseMap());
			_mapper = mapperConfig.CreateMapper();

			_service = new WorkerService(_unitOfWorkMock.Object);
		}

		private void SetSecurityContextUser(User user)
		{
			SecurityContext.SetUser(user);
		}

		[Fact]
		public async Task CreateWorkerAsync_UserIsAdmin()
		{
			// Arrange
			var adminUser = new Admin(1, "Admin", "Administrator");
			SetSecurityContextUser(adminUser);

			var workerDTO = new WorkerDTO { Id = 1, Name = "John Doe" };

			_unitOfWorkMock.Setup(u => u.WorkerRepository.CreateWorkerAsync(It.IsAny<Worker>())).Returns(Task.CompletedTask);

			// Act
			await _service.CreateWorkerAsync(workerDTO);

			// Assert
			_unitOfWorkMock.Verify(u => u.WorkerRepository.CreateWorkerAsync(It.IsAny<Worker>()), Times.Once);
			_unitOfWorkMock.Verify(u => u.SaveAsync(), Times.Once);
		}

		[Fact]
		public async Task CreateWorkerAsync_UserIsNotAdmin()
		{
			// Arrange
			var workerUser = new WorkerIdentity(1, "WorkerUser", "Worker");
			SetSecurityContextUser(workerUser);

			var workerDTO = new WorkerDTO { Id = 1, Name = "John Doe" };

			// Act & Assert
			await Assert.ThrowsAsync<MethodAccessException>(() => _service.CreateWorkerAsync(workerDTO));
		}

		[Fact]
		public async Task ReadAllWorkersAsync()
		{
			// Arrange
			var adminUser = new Admin(1, "Admin", "Administrator");
			SetSecurityContextUser(adminUser);

			var workers = new List<Worker>
		{
			new Worker { Id = 1, Name = "John Doe" },
			new Worker { Id = 2, Name = "Jane Smith" }
		};

			_unitOfWorkMock.Setup(u => u.WorkerRepository.ReadAllWorkersAsync()).ReturnsAsync(workers);

			// Act
			var result = await _service.ReadAllWorkersAsync();

			// Assert
			Assert.Equal(2, result.Count);
			_unitOfWorkMock.Verify(u => u.WorkerRepository.ReadAllWorkersAsync(), Times.Once);
		}

		[Fact]
		public async Task ReadWorkerAsync()
		{
			// Arrange
			var adminUser = new Admin(1, "Admin", "Administrator");
			SetSecurityContextUser(adminUser);

			var worker = new Worker { Id = 1, Name = "John Doe" };

			_unitOfWorkMock.Setup(u => u.WorkerRepository.ReadWorkerAsync(1)).ReturnsAsync(worker);

			// Act
			var result = await _service.ReadWorkerAsync(1);

			// Assert
			Assert.NotNull(result);
			Assert.Equal("John Doe", result.Name);
			_unitOfWorkMock.Verify(u => u.WorkerRepository.ReadWorkerAsync(1), Times.Once);
		}

		[Fact]
		public async Task UpdateWorkerAsync()
		{
			// Arrange
			var adminUser = new Admin(1, "Admin", "Administrator");
			SetSecurityContextUser(adminUser);

			var workerDTO = new WorkerDTO { Id = 1, Name = "Updated Name" };
			_unitOfWorkMock.Setup(u => u.WorkerRepository.UpdateWorkerAsync(It.IsAny<Worker>()))
				.Returns(Task.CompletedTask);

			// Act
			await _service.UpdateWorkerAsync(workerDTO);

			//Assert
			_unitOfWorkMock.Verify(u => u.WorkerRepository.UpdateWorkerAsync(It.IsAny<Worker>()), Times.Once);
			_unitOfWorkMock.Verify(u => u.SaveAsync(), Times.Once);
		}

		[Fact]
		public async Task DeleteWorkerAsync()
		{
			// Arrange
			var adminUser = new Admin(1, "Admin", "Administrator");
			SetSecurityContextUser(adminUser);

			var workerDTO = new WorkerDTO { Id = 1, Name = "John Doe" };

			_unitOfWorkMock.Setup(u => u.WorkerRepository.DeleteWorkerAsync(It.IsAny<Worker>())).Returns(Task.CompletedTask);

			// Act
			await _service.DeleteWorkerAsync(workerDTO);

			// Assert
			_unitOfWorkMock.Verify(u => u.WorkerRepository.DeleteWorkerAsync(It.IsAny<Worker>()), Times.Once);
			_unitOfWorkMock.Verify(u => u.SaveAsync(), Times.Once);
		}
	}
}
