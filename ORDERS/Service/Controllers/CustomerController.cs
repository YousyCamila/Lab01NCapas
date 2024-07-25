using BLL;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using SCL;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;
using BLL.Exceptions;

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

    }
}