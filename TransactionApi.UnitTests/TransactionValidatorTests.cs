using System;
using Xunit;
using TransactionApi.Models;

namespace TransactionApi.UnitTests
{
    public class TransactionValidatorTests
    {   
        [Fact]
        public void when_transaction_does_not_have_a_transaction_date__Then_it_is_invalid()
        {
            //arrange
            var transaction = new Transaction
            {
                Description  = "some transaction",
                TransactionAmount = 1.5M,
                CurrencyCode = "GBP",
                Merchant = " amerchant",
            };
        
            //act
            var result = transaction.IsValid();

            //assert
            Assert.False(result);
        }

        [Fact]
        public void when_transaction_does_not_have_a_transaction_amount__Then_it_is_invalid()
        {
            //arrange
            var transaction = new Transaction
            {
                Description  = "some transaction",
                TransactionDate = new DateTime(),
                CurrencyCode = "GBP",
                Merchant = " amerchant",
            };

            //act
            var result = transaction.IsValid();

            //assert
            Assert.False(result);
        }

        [Fact]
        public void when_transaction_does_not_have_a_description__Then_it_is_invalid()
        {
            //arrange
            var transaction = new Transaction
            {
                TransactionDate  = new DateTime(),
                TransactionAmount = 1.5M,
                CurrencyCode = "GBP",
                Merchant = " amerchant",
            };

            //act
            var result = transaction.IsValid();

            //assert
            Assert.False(result);
        }

        [Theory]
        [InlineData("GBP")]
        [InlineData(null)]
        public void a_transaction_is_valid_whether_or_not_it_has_a_currency_code(string currencyCode)
        {
            //arrange
            var transaction = new Transaction
            {
                Description  = "some transaction",
                TransactionDate  = new DateTime(),
                TransactionAmount = 1.5M,
                CurrencyCode =  currencyCode,
                Merchant = " amerchant",
            };

            //act
            var result = transaction.IsValid();

            //assert
            Assert.True(result);
        }

        [Theory]
        [InlineData("some shop")]
        [InlineData(null)]
        public void a_transaction_is_valid_whether_or_not_it_has_a_merchant(string merchant)
        {
            //arrange
            var transaction = new Transaction
            {
                Description  = "some transaction",
                TransactionDate  = new DateTime(),
                TransactionAmount = 1.5M,
                CurrencyCode = "GBP",
                Merchant = merchant,
            };

            //act
            var result = transaction.IsValid();

            //assert
            Assert.True(result);
        }

    }

    
}
