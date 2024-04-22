using Capstone_api.Data;
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

        [HttpGet("/allPersons")]
        public async Task<ActionResult<List<Person>>> GetAllPersons()
        {
            try
            {
                var dataHandler = new DataHandler();
                var data = await dataHandler.getAllPersons(_dbContext);
            
                if(data != null)
                {
                    return Ok(data);
                }
                else
                {
                    throw new Exception();
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/AllAccessLogs")]
        public async Task<ActionResult<List<AccessLog>>> GetAllAccessLogs()
        {
            try
            {
                var dataHandler = new DataHandler();
                var data = await dataHandler.getAllAccessLogs(_dbContext);

                if (data != null)
                {
                    return Ok(data);
                }
                else
                {
                    throw new Exception();
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/AllRequestLogs")]

        public async Task<ActionResult<List<RequestLog>>> GetAllRequestLogs()
        {
            try
            {
                var dataHandler = new DataHandler();
                var data = await dataHandler.getAllRequestLogs(_dbContext);

                if (data != null)
                {
                    return Ok(data);
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("/DeletePerson/{id:int}")]
        public async Task<ActionResult> DeletePerson(int id)
        {
            try
            {
                var dataHandler = new DataHandler();
                var data = await dataHandler.deletePerson(_dbContext, id);

                if(data == "Success")
                {
                    return Ok(data);
                }
                else
                {
                    throw new Exception();
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        

        [HttpPut("/UpdatePerson")]
        public async Task<ActionResult> UpdatePerson(Person person)
        {
            try
            {
                var dataHanler = new DataHandler();
                var data = await dataHanler.updatePerson(_dbContext, person);

                if (data == "Success")
                {
                    return Ok(data);
                }
                else
                {
                    throw new Exception();
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("/InsertPerson")]
        public async Task<ActionResult<string>> InsertPersonInfo(Person person)
        {
            try
            {
                var dataHandler = new DataHandler();
                var data = await dataHandler.insertNewPerson(_dbContext, person);

                if(data == "Success")
                {
                    return Ok(data);
                }
                else
                {
                    throw new Exception();
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
