using LibraryManager.Communication;
using LibraryManager.DataAccess;
using LibraryManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManager.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(Book), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromServices] AppDbContext context, [FromBody] CreateBookRequest request)
        {
            var book = new Book
            {
                Author = request.Author,
                AvailableUnits = request.AvailableUnits,
                Gender = request.Gender,
                Price = request.Price,
                Title = request.Title,
            };

            await context.Books.AddAsync(book);
            await context.SaveChangesAsync();

            return Created(string.Empty, book);
        }


        [HttpGet]
        [ProducesResponseType(typeof(IList<Book>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll([FromServices] AppDbContext context)
        {
            var response = await context.Books.ToListAsync();

            return Ok(response);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Book), StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Update([FromServices] AppDbContext context, [FromBody] UpdateBookRequest request)
        {
            var book = await context.Books.FirstAsync(x => x.Id == request.Id);

            if (book == null) throw new Exception();

            if (request.Author != null) book.Author = request.Author; 
            if (request.Title != null) book.Title = request.Title; 
            if (request.Gender != null) book.Gender = request.Gender; 
            book.Price = request.Price; 
            book.AvailableUnits = request.AvailableUnits;

            context.Books.Update(book);
            await context.SaveChangesAsync();

            return Ok(book);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete([FromServices] AppDbContext context, string id)
        {
            var guidId = new Guid(id);

            var book = await context.Books.FindAsync(guidId);

            if (book == null) throw new Exception();

            context.Books.Remove(book);
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
