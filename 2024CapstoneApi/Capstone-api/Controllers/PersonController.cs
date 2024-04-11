using Capstone_api.Models;
using Capstone_api.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Capstone_api.Controllers
{
    [Route("2024CapstoneApi/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly GlobalDBContext _dbContext;
        public PersonController(GlobalDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("/testingInfo")]
        public IActionResult GetTestingInfo() 
        {
            return Ok("Hello");
        }
    }
}
