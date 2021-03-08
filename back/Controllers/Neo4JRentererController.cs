using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Neo4j.Driver;
namespace back.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Neo4JRentererController : ControllerBase
    {
        private readonly ILogger<Neo4JRentererController> _logger;
        private readonly IDriver _driver;

        public Neo4JRentererController(ILogger<Neo4JRentererController> logger,IDriver driver )
        {
            _logger = logger;
            _driver = driver;
        }
    //  [HttpPost]
    //  public async Task<IActionResult> CreateNode(string name)
    //  {
      
    //     }
    }
}