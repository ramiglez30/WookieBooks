﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<IList<Book>> GetAsync()
        {
            return await _context.Books.ToListAsync();
        }

        // GET api/<BooksController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Book), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return new NotFoundResult();
            }

            else return new OkObjectResult(book);
        }

        // POST api/<BooksController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> PostAsync([FromBody] Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return new OkResult();
        }

        // PUT api/<BooksController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] Book inputBook)
        {
            if (id != inputBook.Id) return new BadRequestResult();
            
            var book = _context.Books.Find(id);
            if (book == null) return new NotFoundResult();

            book.Author = inputBook.Author;
            book.CoverImage = inputBook.CoverImage;
            book.Description = inputBook.Description;
            book.Price = inputBook.Price;
            book.Title = inputBook.Title;

            _context.Books.Update(book);
            await _context.SaveChangesAsync();

            return new OkResult();
        }

        // DELETE api/<BooksController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Book), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var book = _context.Books.Find(id);
            if (book == null) return new NotFoundResult();

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return new OkResult();
        }
    }
}
