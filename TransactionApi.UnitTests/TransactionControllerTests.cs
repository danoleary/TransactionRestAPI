using System;
using Xunit;
using TransactionApi.Models;
using TransactionApi.Repositories;
using TransactionApi.Controllers;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace TransactionApi.UnitTests
{
    public class TransactionControllerTests
    {   
        [Fact]
        public async Task post_returns_BadRequest_when_model_is_invalid()
        {
            // Arrange
            var mockRepo = new Mock<TransactionRepository>();
            var controller = new TransactionController(mockRepo.Object);
            var transaction = new Transaction
            {
                //TransactionDate = DateTime.UtcNow,
                Description  = "some transaction",
                TransactionAmount = 1.5M,
                CurrencyCode = "GBP",
                Merchant = " amerchant"
            };

            // Act
            var result = await controller.Create(transaction);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task post_returns_BadRequest_when_model_is_null()
        {
            // Arrange
            var mockRepo = new Mock<TransactionRepository>();
            var controller = new TransactionController(mockRepo.Object);

            // Act
            var result = await controller.Create(null);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task put_returns_BadRequest_when_model_is_invalid()
        {
            // Arrange
            var mockRepo = new Mock<TransactionRepository>();
            var controller = new TransactionController(mockRepo.Object);
            var transaction = new Transaction
            {
                TransactionDate = DateTime.UtcNow,
                //Description  = "some transaction",
                TransactionAmount = 1.5M,
                CurrencyCode = "GBP",
                Merchant = " amerchant"
            };

            // Act
            var result = await controller.Update(1, transaction);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task put_returns_BadRequest_when_model_is_null()
        {
            // Arrange
            var mockRepo = new Mock<TransactionRepository>();
            var controller = new TransactionController(mockRepo.Object);

            // Act
            var result = await controller.Update(1, null);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task GetById_returns_NotFound_if_there_is_no_corresponding_transaction()
        {
            // Arrange
            var mockRepo = new Mock<TransactionRepository>();
            mockRepo.Setup(m => m.GetById(It.IsAny<long>())).Returns(Task.FromResult((Transaction)null));
            var controller = new TransactionController(mockRepo.Object);

            // Act
            var result = await controller.GetById(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Update_returns_NotFound_if_there_is_no_corresponding_transaction()
        {
            // Arrange
            var mockRepo = new Mock<TransactionRepository>();
            mockRepo.Setup(m => m.Update(It.IsAny<Transaction>())).Returns(Task.FromResult((Transaction)null));
            var controller = new TransactionController(mockRepo.Object);

            // Act
            var result = await controller.Update(2, new Transaction{Id = 2});

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_returns_NotFound_if_there_is_no_corresponding_transaction()
        {
            // Arrange
            var mockRepo = new Mock<TransactionRepository>();
            mockRepo.Setup(m => m.Delete(It.IsAny<long>())).Returns(Task.FromResult((Transaction)null));
            var controller = new TransactionController(mockRepo.Object);

            // Act
            var result = await controller.Delete(2);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetAll_returns_an_empty_list_if_there_are_no_transactions()
        {
            // Arrange
            var mockRepo = new Mock<TransactionRepository>();
            var x  = Task.FromResult((IEnumerable<Transaction>)new List<Transaction>());
            mockRepo.Setup(m => m.GetAll()).Returns(x);
            var controller = new TransactionController(mockRepo.Object);

            // Act
            var result = await controller.GetAll();

            // Assert
            Assert.Equal(new List<Transaction>(), result.ToList());
        }

    }

    
}
