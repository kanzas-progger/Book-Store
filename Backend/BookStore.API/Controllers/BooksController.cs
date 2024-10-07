using BookStore.API.Contracts;
using BookStore.Core.Abstractions;
using BookStore.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBooksRepository _BooksRepository;

        public BooksController(IBooksRepository IBooksRepository)
        {
            _BooksRepository = IBooksRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Book>>> GetAllBooks()
        {
            var books = await _BooksRepository.GetAll();
            var response = books.Select(b => new BookResponse(b.Id, b.Title, b.Description, b.Price));

            return Ok(response);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Book>> GetOneBook(Guid id)
        {
            var book = await _BooksRepository.GetById(id);

            if (book == null)
            {
                return BadRequest("Book with this Id doesn't exist");
            }

            return Ok(book);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateBook([FromBody] BookRequest request)
        {
            var (book, error) = Book.Create(Guid.NewGuid(), request.title, request.description, request.price);

            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }

            var response = await _BooksRepository.Create(book);

            return Ok(response);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Guid>> UpdateBook(Guid id, [FromBody] BookRequest request)
        {
            var response = await _BooksRepository.Update(id, request.title, request.description, request.price);

            return Ok(response);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Guid>> DeleteBook(Guid id)
        {
            return Ok(await _BooksRepository.Delete(id));
        }
    }
}
