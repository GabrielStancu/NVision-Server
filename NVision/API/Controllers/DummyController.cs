using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DummyController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<int>> GetInts()
        {
            return new List<int>() { 1, 2, 3 };
        }
    }
}
