using back.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Neo4jClient;
using System.Threading.Tasks;
namespace back.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class Neo4JRentererController : ControllerBase
    {
        //private readonly ILogger<Neo4JRentererController> _logger;
        ////   private readonly IDriver _driver;
        //private readonly IGraphClient _client;
        //public Neo4JRentererController(ILogger<Neo4JRentererController> logger, IGraphClient client)
        //{
        //    _logger = logger;
        //    _client = client;
        //    // _driver = driver;

        //}
        //[HttpPost]
        //public async Task<IActionResult> CreateRenterer([FromBody] Renterer renterer)
        //{
        //    await _client.Cypher.Create("(renterer:Renterer {renterer})").WithParams(new { renterer }).ExecuteWithoutResultsAsync();
        //    return StatusCode(201, "Node has been created in the database");
        //}
        //[HttpGet]
        //public async Task<IActionResult> getAllNodes()
        //{
        //    return StatusCode(201, "Get radi!");
        //}

        //    //var people = new List<Renterer>();
        //    ////try
        //    ////{
        //    //var session = this._driver.AsyncSession();
        //    //var result = await session.RunAsync("MATCH(n:Renterer) RETURN (n) limit 10");
        //    //Console.WriteLine(result.ToString());
        //    //return StatusCode(201, "Procitano iz baze kako valja!");
        //    //}
        //    //catch
        //    //{

        //    //}
        //}
    }
}
// [HttpPost]
//     public async Task<ActionResult> postRenterer([FromBody] Renterer renterer) {


//         if (renterer == null)
//             return BadRequest();   
//         if (!ModelState.IsValid)
//             return BadRequest(ModelState);
//         IAsyncSession session = _driver.AsyncSession();
//         try
//         {
//             await session.WriteTransactionAsync(tx => tx.RunAsync("CREATE (a:Renterer {Name: ${renterer.Name}, Address: ${renterer.Address}, Username: ${renterer.Username}})", new {renterer}));
//             return Ok(renterer);
//         }
//         finally
//         {
//             await session.CloseAsync();
//         }

// var newRenterer = new Renterer { Id = uniqueId, Title = title, CategoryCodes = category };
// _driver.Cypher
//     .Merge("(book:Book { Id: {uniqueId} ,Title:{title},CategoryCodes:{category}})")
//     .OnCreate()
//     .Set("book = {newBook}")
//     .WithParams(new
//     {
//         uniqueId =newBook.Id,
//         title = newBook.Title,
//         category = newBook.CategoryCodes,
//         newBook
//     })
//     .ExecuteWithoutResults();

//  uniqueId++;
//  return Ok(newBook);

//  public int Id { get; set; }
//         public string Name { get; set; }
//         public string Address { get; set; }
//         public string Username { get; set; }
//         public decimal Password { get; set; }
//         public string PhoneNumber { get; set; }
//         public string CompanyName { get; set; }
//         public string Email { get; set; }
//         public string ProfilePictureUrl { get; set; }
