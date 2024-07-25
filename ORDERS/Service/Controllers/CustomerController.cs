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


        // DELETE api/<CustomerController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var result = await _bll.DeleteAsync(id);
                if (!result)
                {
                    return NotFound("Customer not found or deletion failed."); // Informative message for unsuccessful deletion
                }
                return NoContent(); // Use NoContent for successful deletions with no content to return
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


        // GET: api/<CustomerController>
        [HttpGet]
        public async Task<ActionResult<List<Customer>>> GetAll()
        {
            try
            {
                var result = await _bll.RetrieveAllAsync();
                return Ok(result); // Use IActionResult for more flexibility (200 OK)
            }
            catch (CustomerExceptions ex) // Catch specific business logic exceptions
            {
                return BadRequest(ex.Message); // Return 400 Bad Request with error message
            }
            catch (Exception ex) // Catch unhandled exceptions for logging and generic error response
            {
                // Log the exception
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
            }
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