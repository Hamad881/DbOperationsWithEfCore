using DbOperationsWithEfCoreApp.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DbOperationsWithEfCoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public BookController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        [HttpGet("")]
        public async Task<IActionResult> GetAllBooks()
        {
            var result = await _appDbContext.Books.ToListAsync();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllBooksById([FromRoute] int id)
        {
            var result = await _appDbContext.Books.FindAsync(id);
            return Ok(result);
        }

        [HttpPost("")]
        public async Task<IActionResult> AddBook([FromBody] Book model)
        {
            
            _appDbContext.Books.Add(model);
             await _appDbContext.SaveChangesAsync();
            return Ok(model);
        }
        [HttpPost("bulk")]
        public async Task<IActionResult> AddBooks([FromBody] List<Book> model)
        {
            _appDbContext.Books.AddRange(model);
            await _appDbContext.SaveChangesAsync();
            return Ok(model);
        }
        [HttpPut("{bookId}")]
        public async Task<IActionResult> UpdateBook([FromRoute] int bookId,[FromBody] Book model)
        {
            var book = _appDbContext.Books.FirstOrDefault(x => x.Id == bookId);
            if (book == null)
            {
                return NotFound();
            }
            book.Title = model.Title;
            book.Description = model.Description;

            
            await _appDbContext.SaveChangesAsync();
            return Ok(model);
        }
        [HttpPut("{bulk}")]
        public async Task<IActionResult> UpdateBookInBulk()
        {
            await _appDbContext.Books
                  .Where(x => x.NoOfPages == 0)  //update data only where this condition meets
                  .ExecuteUpdateAsync(x => x
                  .SetProperty(p => p.Description, p => p.Title + "This is a book Description")
                  .SetProperty(p => p.Title, p => p.Title + "Updated")
                  );
            return Ok();
        }

        [HttpDelete("{bookId}")]
        public async Task<IActionResult> DeleteBooks([FromRoute] int bookId)
        {

            //var book = new Book(Id = bookId);
            //_appDbContext.Entry(book).State = EntityState.Deleted;
            //await _appDbContext.SaveChangesAsync();
            var book = _appDbContext.Books.FirstOrDefault(x => x.Id == bookId);
            if (book == null)
            {
                return NotFound();
            }
            _appDbContext.Books.Remove(book);
            await _appDbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
