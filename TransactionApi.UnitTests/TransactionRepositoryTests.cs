using System;
using Xunit;
using TransactionApi.Models;
using TransactionApi.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace TransactionApi.UnitTests
{
    public class TransactionRepositoryTests
    {   
        private TransactionRepository _testClass;

        public TransactionRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<TransactionContext>()
                      .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                      .Options;
            var context = new TransactionContext(options);
            this._testClass = new TransactionRepository(context);
        }

        [Fact]
        public async Task when_a_transaction_is_added__Then_it_is_returned_with_its_id()
        {
            //arrange
            var transaction = new Transaction
            {
                TransactionDate = DateTime.UtcNow,
                Description  = "some transaction",
                TransactionAmount = 1.5M,
                CurrencyCode = "GBP",
                Merchant = " amerchant",
            };
        
            //act
            var result = await this._testClass.Create(transaction);

            //assert
            Assert.True(result.Id != null);
            Assert.Equal(result.Description, transaction.Description);
            Assert.Equal(result.TransactionAmount, transaction.TransactionAmount);
            Assert.Equal(result.CurrencyCode, transaction.CurrencyCode);
            Assert.Equal(result.Merchant, transaction.Merchant);
        }

        [Fact]
        public async Task when_a_transaction_has_been_added__Then_it_can_be_accessed_via_its_id()
        {
            //arrange
            var transaction = new Transaction
            {
                TransactionDate = DateTime.UtcNow,
                Description  = "some transaction",
                TransactionAmount = 1.5M,
                CurrencyCode = "GBP",
                Merchant = " amerchant",
            };
            var insertedTransaction = await this._testClass.Create(transaction);
        
            //act
            var result = await this._testClass.GetById(insertedTransaction.Id.Value);

            //assert
            Assert.Equal(insertedTransaction.Id, result.Id);
            Assert.Equal(insertedTransaction.Description, result.Description);
            Assert.Equal(insertedTransaction.TransactionAmount, result.TransactionAmount);
            Assert.Equal(insertedTransaction.CurrencyCode, result.CurrencyCode);
            Assert.Equal(insertedTransaction.Merchant, result.Merchant);
        }

        [Fact]
        public async Task when_a_transaction_is_requested_for_an_id_not_in_the_db__Then_null_is_returned()
        {
            //arrange
            var id = 1000;
            //act
            var result = await this._testClass.GetById(id);

            //assert
            Assert.Equal(null, result);
        }

        [Fact]
        public async Task when_getting_all_transactions__Then_all_transactions_in_db_are_returned()
        {
            //arrange
            var transaction = new Transaction
            {
                TransactionDate = DateTime.UtcNow,
                Description  = "some transaction",
                TransactionAmount = 1.5M,
                CurrencyCode = "GBP",
                Merchant = " amerchant",
            };

            await this._testClass.Create(transaction);
        
            //act
            var result = await this._testClass.GetAll();
            
            //assert
            Assert.Equal(transaction.Id, result.Single().Id);
        }

        [Fact]
        public async Task when_updating_a_transaction__Then_the_new_values_are_saved()
        {
            //arrange
            var transaction = new Transaction
            {
                TransactionDate = DateTime.UtcNow,
                Description  = "some transaction",
                TransactionAmount = 1.5M,
                CurrencyCode = "GBP",
                Merchant = " amerchant",
            };

            var insertedTransaction = await this._testClass.Create(transaction);
            insertedTransaction.TransactionDate = DateTime.UtcNow.AddDays(1);
            insertedTransaction.Description = "a new description";
            insertedTransaction.TransactionAmount = 2.3M;
            insertedTransaction.CurrencyCode = "EUR";
            insertedTransaction.Merchant = "a new merchant";

            //act
            await this._testClass.Update(insertedTransaction);
            
            //assert
            var updatedTran = await this._testClass.GetById(insertedTransaction.Id.Value);
            Assert.Equal(insertedTransaction.Id, updatedTran.Id);
            Assert.Equal(insertedTransaction.Description, updatedTran.Description);
            Assert.Equal(insertedTransaction.TransactionAmount, updatedTran.TransactionAmount);
            Assert.Equal(insertedTransaction.CurrencyCode, updatedTran.CurrencyCode);
            Assert.Equal(insertedTransaction.Merchant, updatedTran.Merchant);
        }

        [Fact]
        public async Task when_deleting_a_transaction__Then_the_transaction_can_no_longer_be_returned()
        {
            //arrange
            var transaction = new Transaction
            {
                TransactionDate = DateTime.UtcNow,
                Description  = "some transaction",
                TransactionAmount = 1.5M,
                CurrencyCode = "GBP",
                Merchant = " amerchant",
            };

            var insertedTransaction = await this._testClass.Create(transaction);

            //act
            await this._testClass.Delete(insertedTransaction.Id.Value);
            
            //assert
            var deletedTransaction = await this._testClass.GetById(insertedTransaction.Id.Value);
            Assert.Equal(null, deletedTransaction);
        }

        private TransactionRepository TestClass()
        {
            var options = new DbContextOptionsBuilder<TransactionContext>()
                      .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                      .Options;
            var context = new TransactionContext(options);
            return new TransactionRepository(context);
        }


    }

    
}
