using DbOperationsWithEfCoreApp.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DbOperationsWithEfCoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public CurrencyController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet("")]
        public async Task <IActionResult> GetAllCurrencies()
        {
            var result = await _appDbContext.Currencies.ToListAsync();
            return Ok(result);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAllCurrenciesById([FromRoute] int id)
        {
            var result = await _appDbContext.Currencies.FindAsync(id);
            return Ok(result);
        }
        [HttpGet("{name}")]
        public async Task<IActionResult> GetAllCurrenciesByName([FromRoute] string name, [FromQuery] string? description)
        {
            var result = await _appDbContext.Currencies
                .Where(x => x.Currenc == name && (string.IsNullOrEmpty(description)|| x.Description==description)).ToListAsync();
            return Ok(result);
        }
        [HttpPost("All")]
        public async Task<IActionResult> GetSelectedCurrencies([FromBody] List<int>ids)
        {
            var result = await _appDbContext.Currencies
                .Where(x =>ids.Contains(x.Id) )
                .Select(x=> new Currency()
                {
                    Id = x.Id,
                    Currenc = x.Currenc,
                })
                .ToListAsync();
            return Ok(result);
        }
    }
}
