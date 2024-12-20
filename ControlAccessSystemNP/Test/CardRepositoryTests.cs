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
	public class CardRepositoryTests
	{
		private readonly Mock<ControlAccessSystemContext> _mockContext;
		private readonly Mock<DbSet<Card>> _mockDbSet;
		private readonly CardRepository _repository;

		public CardRepositoryTests()
		{
			_mockContext = new Mock<ControlAccessSystemContext>();
			_mockDbSet = new Mock<DbSet<Card>>();
			_mockContext.Setup(m => m.Set<Card>()).Returns(_mockDbSet.Object);

			_repository = new CardRepository(_mockContext.Object);
		}

		[Fact]
		public async Task CreateCardAsync()
		{
			// Arrange
			var card = new Card { Id = 1, WorkerId = 1};

			_mockDbSet.Setup(m => m.AddAsync(card, default))
				.ReturnsAsync((EntityEntry<Card>)null);

			// Act
			await _repository.CreateCardAsync(card);

			// Assert
			_mockDbSet.Verify(m => m.AddAsync(card, default), Times.Once);
			_mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once);
		}

		[Fact]
		public async Task DeleteCardAsync()
		{
			// Arrange
			var card = new Card { Id = 1, WorkerId = 1 };

			_mockDbSet.Setup(m => m.Remove(card));

			// Act
			await _repository.DeleteCardAsync(card);

			// Assert
			_mockDbSet.Verify(m => m.Remove(card), Times.Once);
			_mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once);
		}

		[Fact]
		public async Task ReadCardAsync()
		{
			// Arrange
			var card = new Card { Id = 1, WorkerId = 1 };
			_mockDbSet.Setup(m => m.FindAsync(1)).ReturnsAsync(card);

			// Act
			var result = await _repository.ReadCardAsync(1);

			// Assert
			Assert.Equal(card, result);
			_mockDbSet.Verify(m => m.FindAsync(1), Times.Once);
		}

		[Fact]
		public async Task UpdateCardAsync()
		{
			// Arrange
			var card = new Card { Id = 1, WorkerId = 1 };

			_mockDbSet.Setup(m => m.Update(card));

			// Act
			await _repository.UpdateCardAsync(card);

			// Assert
			_mockDbSet.Verify(m => m.Update(card), Times.Once);
			_mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once);
		}
	}
}
