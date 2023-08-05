using Bookstore.Application.Logics.BookLogics;
using Bookstore.Application.Logics.BookLogics.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.API.Controllers
{
    [Authorize]
    public class BookController : BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> CreateBookAsync([FromBody] BookModel model)
        {
            return Ok(await Mediator.Send(new CreateBook { Model = model }));
        }

        [HttpGet]
        public async Task<IActionResult> GetBooksAsync()
        {
            return Ok(await Mediator.Send(new GetBooks()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookByIdAsync(int id)
        {
            return Ok(await Mediator.Send(new GetBookById() { BookId = id }));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBookAsync([FromBody] BookModel model)
        {
            return Ok(await Mediator.Send(new UpdateBook { Model = model }));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookAsync([FromRoute] int id)
        {
            return Ok(await Mediator.Send(new DeleteBook { BookId = id }));
        }
    }
}
