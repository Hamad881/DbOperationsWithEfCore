using DbOperationsWithEfCoreApp.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DbOperationsWithEfCoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookPriceController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public BookPriceController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        [HttpGet("")]
        public async Task<IActionResult> GetAllPrices()
        {
            var result= await _appDbContext.BooksPrice.ToListAsync();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPricesById([FromRoute] int id)
        {
            var result = await _appDbContext.BooksPrice.FindAsync(id);
            return Ok(result);
        }

    }
}
