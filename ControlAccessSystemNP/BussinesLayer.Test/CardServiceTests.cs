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
	public class CardServiceTests
	{
		private readonly Mock<IUnitOfWork> _unitOfWorkMock;
		private readonly IMapper _mapper;
		private readonly CardService _service;

		public CardServiceTests()
		{
			_unitOfWorkMock = new Mock<IUnitOfWork>();

			var mapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<Card, CardDTO>().ReverseMap());
			_mapper = mapperConfig.CreateMapper();

			_service = new CardService(_unitOfWorkMock.Object);
		}

		private void SetSecurityContextUser(User user)
		{
			SecurityContext.SetUser(user);
		}

		[Fact]
		public async Task CreateCardAsync_UserIsAdmin()
		{
			// Arrange
			var adminUser = new Admin(1, "Admin", "Administrator");
			SetSecurityContextUser(adminUser);

			var cardDTO = new CardDTO { Id = 1, WorkerId = 1 };
			_unitOfWorkMock.Setup(u => u.CardRepository.CreateCardAsync(It.IsAny<Card>()))
				.Returns(Task.CompletedTask);

			// Act
			await _service.CreateCardAsync(cardDTO);

			// Assert
			_unitOfWorkMock.Verify(u => u.CardRepository.CreateCardAsync(It.IsAny<Card>()), Times.Once);
			_unitOfWorkMock.Verify(u => u.SaveAsync(), Times.Once);
		}

		[Fact]
		public async Task CreateCardAsync_UserIsNotAdmin()
		{
			// Arrange
			var workerUser = new WorkerIdentity(1, "Jacob", "Worker");
			SetSecurityContextUser(workerUser);

			var cardDTO = new CardDTO { Id = 1, WorkerId = 1 };

			// Act & Assert
			await Assert.ThrowsAsync<MethodAccessException>(() => _service.CreateCardAsync(cardDTO));
		}

		[Fact]
		public async Task ReadAllCardsAsync()
		{
			// Arrange
			var adminUser = new Admin(1, "Admin", "Administrator");
			SetSecurityContextUser(adminUser);

			var cards = new List<Card>
			{
				new Card { Id = 1, WorkerId = 1 },
				new Card { Id = 2, WorkerId = 2 }
			};
			_unitOfWorkMock.Setup(u => u.CardRepository.ReadAllCardsAsync())
				.ReturnsAsync(cards);

			// Act
			var result = await _service.ReadAllCardsAsync();

			// Assert
			Assert.NotNull(result);
			Assert.Equal(2, result.Count);
			_unitOfWorkMock.Verify(u => u.CardRepository.ReadAllCardsAsync(), Times.Once);
		}

		[Fact]
		public async Task ReadCardAsync()
		{
			// Arrange
			var adminUser = new Admin(1, "Admin", "Administrator");
			SetSecurityContextUser(adminUser);

			var card = new Card { Id = 1, WorkerId = 1};
			_unitOfWorkMock.Setup(u => u.CardRepository.ReadCardAsync(It.IsAny<int>()))
				.ReturnsAsync(card);

			// Act
			var result = await _service.ReadCardAsync(1);

			// Assert
			Assert.NotNull(result);
			Assert.Equal(card.WorkerId, result.WorkerId);
			_unitOfWorkMock.Verify(u => u.CardRepository.ReadCardAsync(It.IsAny<int>()), Times.Once);
		}

		[Fact]
		public async Task UpdateCardAsync()
		{
			// Arrange
			var adminUser = new Admin(1, "Admin", "Administrator");
			SetSecurityContextUser(adminUser);

			var cardDTO = new CardDTO { Id = 1, WorkerId = 1 };
			_unitOfWorkMock.Setup(u => u.CardRepository.UpdateCardAsync(It.IsAny<Card>()))
				.Returns(Task.CompletedTask);

			// Act
			await _service.UpdateCardAsync(cardDTO);

			// Assert
			_unitOfWorkMock.Verify(u => u.CardRepository.UpdateCardAsync(It.IsAny<Card>()), Times.Once);
			_unitOfWorkMock.Verify(u => u.SaveAsync(), Times.Once);
		}

		[Fact]
		public async Task DeleteCardAsync()
		{
			// Arrange
			var adminUser = new Admin(1, "Admin", "Administrator");
			SetSecurityContextUser(adminUser);

			var cardDTO = new CardDTO { Id = 1, WorkerId = 1 };
			_unitOfWorkMock.Setup(u => u.CardRepository.DeleteCardAsync(It.IsAny<Card>()))
				.Returns(Task.CompletedTask);

			// Act
			await _service.DeleteCardAsync(cardDTO);

			// Assert
			_unitOfWorkMock.Verify(u => u.CardRepository.DeleteCardAsync(It.IsAny<Card>()), Times.Once);
			_unitOfWorkMock.Verify(u => u.SaveAsync(), Times.Once);
		}
	}
}
