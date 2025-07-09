using DbOperationsWithEfCoreApp.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DbOperationsWithEfCoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguagesController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public LanguagesController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllLanguages()
        {
            var result = await _appDbContext.Languages.ToListAsync();
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAllLanguagesById([FromRoute] int id)
        {
            var result = await _appDbContext.Languages.FindAsync(id);
            return Ok(result);
        }
        [HttpGet("{name}")]
        public async Task<IActionResult> GetAllLanguagesByName([FromRoute] string name)
        {
            var result = await _appDbContext.Languages.FirstOrDefaultAsync(x => x.Title == name);
            return Ok(result);
        }
    }
}
