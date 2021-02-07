using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WookieBooks.Data;
using WookieBooks.Models;

namespace WookieBooks.Controllers
{
    [Route("api/[controller]")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ApiController]
    public class BooksController : ControllerBase
    {
        readonly WookieBooksDbContext _context;
        public BooksController(WookieBooksDbContext context)
        {
            _context = context;
        }
        // GET: api/<BooksController>
        [HttpGet]
        [ProducesResponseType(typeof(List<Book>), StatusCodes.Status200OK)]
        public IList<Book> Get()
        {
            return _context.Books.ToList();
        }

        // GET api/<BooksController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Book), StatusCodes.Status200OK)]
        public IActionResult Get(int id)
        {
            var book = _context.Books.Find(id);
            if (book == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            else return StatusCode(StatusCodes.Status200OK, book);
        }

        // POST api/<BooksController>
        [HttpPost]
        public void Post([FromBody] Book book)
        {
            _context.Books.Add(book);
        }

        // PUT api/<BooksController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BooksController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
