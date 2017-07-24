using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TransactionApi.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;

namespace TransactionApi.Repositories
{
    public class TransactionRepository
    {
        private readonly TransactionContext _context;

        public TransactionRepository(){} // for moq

        public TransactionRepository(TransactionContext context)
        {
            _context = context;
        }

        public virtual async Task<IEnumerable<Transaction>> GetAll()
        {
            return await _context.Transactions.ToListAsync();
        } 

        public virtual async Task<Transaction> GetById(long id)
        {
            return await _context.Transactions.SingleOrDefaultAsync(t => t.Id == id);
        }  

        public virtual async Task<Transaction> Update(Transaction item)
        {
            var transaction = await _context.Transactions.SingleOrDefaultAsync(t => t.Id == item.Id);
            if(transaction != null) {
                transaction.Description = item.Description;
                transaction.CurrencyCode = item.CurrencyCode;
                transaction.Merchant = item.Merchant;
                transaction.TransactionAmount = item.TransactionAmount;
                transaction.TransactionDate = item.TransactionDate;
                _context.Transactions.Update(transaction);
                await _context.SaveChangesAsync();
            };
            
            return item;
        }

        public virtual async Task<Transaction> Delete(long id)
        {
            var transaction = await _context.Transactions.SingleOrDefaultAsync(t => t.Id == id);
            if (transaction == null)
            {
                return null;
            }
            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();
            return transaction;
        }

        public async Task<Transaction> Create(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
            return transaction;
        } 
    }
}