using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Student_Result_Management_System.Models;

namespace Student_Result_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultController : ControllerBase
    {
        [HttpGet]
        // IActionResult return any value type
        //public async Task<IActionResult> Get()
        // ActionResult return specific value type, the type will displayed in Schemas section
        public async Task<ActionResult<List<Result>>> Get() // async go with Task<> to make function asynchronous
        {
            var results = new List<Result>
            {
                new Result
                {
                    Id = 1,
                    StudentId = 1,
                    QuestionId = 1,
                    Marks = 10
                }
            };
            return Ok(results);
        }
    }
}
