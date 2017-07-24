using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TransactionApi.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;
using TransactionApi.Repositories;

namespace TransactionApi.Controllers
{
    [Route("api/[controller]")]
    public class TransactionController : Controller
    {
        private readonly TransactionRepository _repository;

        public TransactionController(TransactionRepository repository)
        {
            _repository = repository;
        }  

        [HttpGet]
        public async Task<IEnumerable<Transaction>> GetAll()
        {
            return await _repository.GetAll();
        }

        [HttpGet("{id}", Name = "GetTransaction")]
        public async Task<IActionResult> GetById(long id)
        {
            var transaction = await _repository.GetById(id);
            if (transaction == null)
            {
                return NotFound();
            }
            return new ObjectResult(transaction);
        }    

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Transaction model)
        {
            if (model == null || !model.IsValid())
            {
                return BadRequest();
            }

            var insertedTransaction = await _repository.Create(model);

            return CreatedAtRoute("GetTransaction", new { id = insertedTransaction.Id.Value }, model);
        } 

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] Transaction item)
        {
            if (item == null || item.Id != id || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var transaction = await _repository.Update(item);
            if (transaction == null)
            {
                return NotFound();
            }

            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var todo = await _repository.Delete(id);
            if (todo == null)
            {
                return NotFound();
            }

            return new NoContentResult();
        }
    }
}