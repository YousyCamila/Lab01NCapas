using BLL;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using SCL;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;

namespace Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase, IService
    {
        private readonly Customers _bll; // Dependency injection for better testability

        public CustomerController(Customers bll)
        {
            _bll = bll;
        }


        // POST: api/<CustomerController>
        [HttpPost]
        public async Task<ActionResult<Customer>> CreateAsync([FromBody] Customer toCreate)
        {
            try
            {
                var customer = await _bll.CreateAsync(toCreate);
                return CreatedAtRoute("RetrieveAsync", new { id = customer.Id }, customer); // Use CreatedAtRoute for 201 Created
            }
            catch (CustomerExceptions ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
            }
        }


        public Task<IActionResult> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<List<Customer>>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<Customer>> RetrieveAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> UpdateAsync(int id, [FromBody] Customer toUpdate)
        {
            throw new NotImplementedException();
        }
    }
}