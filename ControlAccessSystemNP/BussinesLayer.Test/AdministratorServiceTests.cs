using AutoMapper;
using BussinesLayer.DTO;
using BussinesLayer.Services;
using CCL.Security;
using CCL.Security.Identity;
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
	public class AdministratorServiceTests
	{
		private readonly Mock<IUnitOfWork> _unitOfWorkMock;
		private readonly IMapper _mapper;
		private readonly AdministratorService _service;

		public AdministratorServiceTests()
		{
			_unitOfWorkMock = new Mock<IUnitOfWork>();

			var mapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<Administrator, AdministratorDTO>().ReverseMap());
			_mapper = mapperConfig.CreateMapper();

			_service = new AdministratorService(_unitOfWorkMock.Object);
		}

		private void SetSecurityContextUser(User user)
		{
			SecurityContext.SetUser(user);
		}

		[Fact]
		public async Task CreateAdministratorAsync_ShouldCreateAdministratorWhenUserIsAdmin()
		{
			// Arrange
			var adminUser = new Admin(1, "Admin", "Administrator");
			SetSecurityContextUser(adminUser);

			var administratorDTO = new AdministratorDTO { Id = 1, Name = "Test Admin" };
			_unitOfWorkMock.Setup(u => u.AdministratorRepository.CreateAdministratorAsync(It.IsAny<Administrator>()))
				.Returns(Task.CompletedTask);

			// Act
			await _service.CreateAdministratorAsync(administratorDTO);

			// Assert
			_unitOfWorkMock.Verify(u => u.AdministratorRepository.CreateAdministratorAsync(It.IsAny<Administrator>()), Times.Once);
			_unitOfWorkMock.Verify(u => u.SaveAsync(), Times.Once);
		}

		[Fact]
		public async Task CreateAdministratorAsync_ShouldThrowMethodAccessExceptionWhenUserIsNotAdmin()
		{
			// Arrange
			var workerUser = new WorkerIdentity(2, "John", "Worker");
			SetSecurityContextUser(workerUser);

			var administratorDTO = new AdministratorDTO { Id = 1, Name = "Test Admin" };

			// Act & Assert
			await Assert.ThrowsAsync<MethodAccessException>(() => _service.CreateAdministratorAsync(administratorDTO));
		}

		[Fact]
		public async Task ReadAdministratorAsync()
		{
			// Arrange
			var adminUser = new Admin(1, "Admin", "Administrator");
			SetSecurityContextUser(adminUser);

			var administrator = new Administrator { Id = 1, Name = "Test Admin" };
			_unitOfWorkMock.Setup(u => u.AdministratorRepository.ReadAdministratorAsync(It.IsAny<int>()))
				.ReturnsAsync(administrator);

			// Act
			var result = await _service.ReadAdministratorAsync(1);

			// Assert
			Assert.NotNull(result);
			Assert.Equal(administrator.Name, result.Name);
			_unitOfWorkMock.Verify(u => u.AdministratorRepository.ReadAdministratorAsync(It.IsAny<int>()), Times.Once);
		}

		[Fact]
		public async Task DeleteAdministratorAsync()
		{
			// Arrange
			var adminUser = new Admin(1, "Admin", "Administrator");
			SetSecurityContextUser(adminUser);

			var administratorDTO = new AdministratorDTO { Id = 1, Name = "Test Admin" };
			_unitOfWorkMock.Setup(u => u.AdministratorRepository.DeleteAdministratorAsync(It.IsAny<Administrator>()))
				.Returns(Task.CompletedTask);

			// Act
			await _service.DeleteAdministratorAsync(administratorDTO);

			// Assert
			_unitOfWorkMock.Verify(u => u.AdministratorRepository.DeleteAdministratorAsync(It.IsAny<Administrator>()), Times.Once);
			_unitOfWorkMock.Verify(u => u.SaveAsync(), Times.Once);
		}

		[Fact]
		public async Task ReadAllAdministratorsAsync()
		{
			// Arrange
			var adminUser = new Admin(1, "Admin", "Administrator");
			SetSecurityContextUser(adminUser);

			var administrators = new List<Administrator>
			{
				new Administrator { Id = 1, Name = "Admin 1" },
				new Administrator { Id = 2, Name = "Admin 2" }
			};
			_unitOfWorkMock.Setup(u => u.AdministratorRepository.ReadAllAdministratorsAsync())
				.ReturnsAsync(administrators);

			// Act
			var result = await _service.ReadAllAdministratorsAsync();

			// Assert
			Assert.NotNull(result);
			Assert.Equal(2, result.Count);
			_unitOfWorkMock.Verify(u => u.AdministratorRepository.ReadAllAdministratorsAsync(), Times.Once);
		}

		[Fact]
		public async Task UpdateAdministratorAsync()
		{
			// Arrange
			var adminUser = new Admin(1, "Admin", "Administrator");
			SetSecurityContextUser(adminUser);

			var administratorDTO = new AdministratorDTO { Id = 1, Name = "Updated Admin" };
			_unitOfWorkMock.Setup(u => u.AdministratorRepository.UpdateAdministratorAsync(It.IsAny<Administrator>()))
				.Returns(Task.CompletedTask);

			// Act
			await _service.UpdateAdministratorAsync(administratorDTO);

			// Assert
			_unitOfWorkMock.Verify(u => u.AdministratorRepository.UpdateAdministratorAsync(It.IsAny<Administrator>()), Times.Once);
			_unitOfWorkMock.Verify(u => u.SaveAsync(), Times.Once);
		}
	}
}
