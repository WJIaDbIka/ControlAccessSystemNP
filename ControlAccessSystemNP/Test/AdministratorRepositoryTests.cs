using DataLayer.EF;
using DataLayer.Entity;
using DataLayer.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;

namespace Test
{
	public class AdministratorRepositoryTests
	{
		private readonly Mock<ControlAccessSystemContext> _mockContext;
		private readonly Mock<DbSet<Administrator>> _mockDbSet;
		private readonly AdministratorRepository _repository;

		public AdministratorRepositoryTests()
		{
			_mockContext = new Mock<ControlAccessSystemContext>();
			_mockDbSet = new Mock<DbSet<Administrator>>();
			_mockContext.Setup(m => m.Set<Administrator>()).Returns(_mockDbSet.Object);

			_repository = new AdministratorRepository(_mockContext.Object);
		}

		[Fact]
		public async Task CreateAdministratorAsync()
		{
			// Arrange
			var admin = new Administrator { Id = 1, Name = "Admin1", Phone = "+381234567890", Position = "Worker" };

			_ = _mockDbSet.Setup(m => m.AddAsync(admin, default))
				.ReturnsAsync((EntityEntry<Administrator>)null);

			// Act
			await _repository.CreateAdministratorAsync(admin);

			// Assert
			_mockDbSet.Verify(m => m.AddAsync(admin, default), Times.Once);
			_mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once);
		}

		[Fact]
		public async Task DeleteAdministratorAsync()
		{
			// Arrange
			var admin = new Administrator { Id = 1, Name = "Admin1", Phone = "+381234567890", Position = "Worker" };

			_mockDbSet.Setup(m => m.Remove(admin));

			// Act
			await _repository.DeleteAdministratorAsync(admin);

			// Assert
			_mockDbSet.Verify(m => m.Remove(admin), Times.Once);
			_mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once);
		}

		[Fact]
		public async Task ReadAdministratorAsync()
		{
			// Arrange
			var admin = new Administrator { Id = 1, Name = "Admin1", Phone = "+381234567890", Position = "Worker" };
			_mockDbSet.Setup(m => m.FindAsync(1)).ReturnsAsync(admin);

			// Act
			var result = await _repository.ReadAdministratorAsync(1);

			// Assert
			Assert.Equal(admin, result);
			_mockDbSet.Verify(m => m.FindAsync(1), Times.Once);
		}

		[Fact]
		public async Task UpdateAdministratorAsync()
		{
			// Arrange
			var admin = new Administrator { Id = 1, Name = "Admin1", Phone = "+381234567890", Position = "Worker" };

			_mockDbSet.Setup(m => m.Update(admin));

			// Act
			await _repository.UpdateAdministratorAsync(admin);

			// Assert
			_mockDbSet.Verify(m => m.Update(admin), Times.Once);
			_mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once);
		}
	}
}
