using DataLayer.EF;
using DataLayer.Entity;
using DataLayer.Repository;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
	public class WorkerRepositoryTests
	{
		private readonly Mock<ControlAccessSystemContext> _mockContext;
		private readonly Mock<DbSet<Worker>> _mockDbSet;
		private readonly WorkerRepository _repository;

		public WorkerRepositoryTests()
		{
			_mockContext = new Mock<ControlAccessSystemContext>();
			_mockDbSet = new Mock<DbSet<Worker>>();
			_mockContext.Setup(m => m.Set<Worker>()).Returns(_mockDbSet.Object);

			_repository = new WorkerRepository(_mockContext.Object);
		}

		[Fact]
		public async Task CreateWorkerAsync_AddsEntityAndSavesChanges()
		{
			// Arrange
			var worker = new Worker { Id = 1, Name = "Worker1", Phone = "+381234567890", Position = "Position" };

			_mockDbSet.Setup(m => m.AddAsync(worker, default))
				.ReturnsAsync((EntityEntry<Worker>)null);

			// Act
			await _repository.CreateWorkerAsync(worker);

			// Assert
			_mockDbSet.Verify(m => m.AddAsync(worker, default), Times.Once);
			_mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once);
		}

		[Fact]
		public async Task DeleteWorkerAsync_RemovesEntityAndSavesChanges()
		{
			// Arrange
			var worker = new Worker { Id = 1, Name = "Worker1", Phone = "+381234567890", Position = "Position" };

			_mockDbSet.Setup(m => m.Remove(worker));

			// Act
			await _repository.DeleteWorkerAsync(worker);

			// Assert
			_mockDbSet.Verify(m => m.Remove(worker), Times.Once);
			_mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once);
		}

		[Fact]
		public async Task ReadWorkerAsync_FindsEntityById()
		{
			// Arrange
			var worker = new Worker { Id = 1, Name = "Worker1", Phone = "+381234567890", Position = "Position" };
			_mockDbSet.Setup(m => m.FindAsync(1)).ReturnsAsync(worker);

			// Act
			var result = await _repository.ReadWorkerAsync(1);

			// Assert
			Assert.Equal(worker, result);
			_mockDbSet.Verify(m => m.FindAsync(1), Times.Once);
		}

		[Fact]
		public async Task UpdateWorkerAsync_UpdatesEntityAndSavesChanges()
		{
			// Arrange
			var worker = new Worker { Id = 1, Name = "Worker1", Phone = "+381234567890", Position = "Position" };

			_mockDbSet.Setup(m => m.Update(worker));

			// Act
			await _repository.UpdateWorkerAsync(worker);

			// Assert
			_mockDbSet.Verify(m => m.Update(worker), Times.Once);
			_mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once);
		}
	}
}
