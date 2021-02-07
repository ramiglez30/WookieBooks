using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;
using WookieBooks.Controllers;
using WookieBooks.Data;
using WookieBooks.Models;

namespace WookieBooks.Tests
{
    [TestClass]
    public class BooksControllerTests
    {
        readonly WookieBooksDbContext _context;
        public BooksControllerTests()
        {
            var optionsBuilder = new DbContextOptionsBuilder<WookieBooksDbContext>()
           .UseInMemoryDatabase(databaseName: "Test");

            //seeding the database with same data is in project. File: WookieBooks/Data/DatabaseSeed.cs
            optionsBuilder.Seed();

            _context = new(optionsBuilder.Options);
        }

        [TestMethod]
        public async Task GetAsync_Returns_Collection()
        {
            //Arrange 
            var booksController = new BooksController(_context);
            var existingCount = _context.Books.Count();

            //Act 
            var result = await booksController.GetAsync();

            //Assert 
            Assert.AreEqual(existingCount, result.Count);
        }

        [TestMethod]
        public async Task GetAsync_With_Id_Returns_OK_With_Found_Book()
        {
            //Arrange 
            var booksController = new BooksController(_context);
            var firstItem = _context.Books.FirstOrDefault();

            //Act 
            var result = (ObjectResult)await booksController.GetAsync(firstItem.Id);

            //Assert 
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(firstItem.Title, ((Book)result.Value).Title);
            Assert.AreEqual(StatusCodes.Status200OK, result.StatusCode);
        }

        [TestMethod]
        public async Task GetAsync_With_NotExistingId_Returns_NotFound()
        {
            //Arrange 
            var booksController = new BooksController(_context);

            //Act 
            var result = (NotFoundResult)await booksController.GetAsync(99);

            //Assert 
            Assert.AreEqual(StatusCodes.Status404NotFound, result.StatusCode);
        }

        [TestMethod]
        public async Task PostAsync_With_Valid_Input_Returns_OK()
        {
            //Arrange 
            var booksController = new BooksController(_context);
            var currentCount = _context.Books.Count();

            var book = new Book { Title = "New Book", Price = 10.5, Author = "John Doe", CoverImage = "/image.jpg", Description = "New Book Description"}; 

            //Act 
            var result = (OkResult)await booksController.PostAsync(book);

            //Assert 
            Assert.AreEqual(StatusCodes.Status200OK, result.StatusCode);
            Assert.AreEqual(currentCount + 1, _context.Books.Count());
        }

        [TestMethod]
        public async Task PutAsync_With_Valid_Input_Returns_OK()
        {
            //Arrange 
            var booksController = new BooksController(_context);
            var firstItem = _context.Books.FirstOrDefault();

            var book = new Book {Id = firstItem.Id, Title = "New Updated Title", Price = 10.5, Author = "John Doe", CoverImage = "/image.jpg", Description = "New Book Description" };

            //Act 
            var result = (OkResult)await booksController.PutAsync(firstItem.Id, book);

            //Assert 
            Assert.AreEqual(StatusCodes.Status200OK, result.StatusCode);
            Assert.AreEqual("New Updated Title", _context.Books.Find(firstItem.Id).Title);
        }

        [TestMethod]
        public async Task PutAsync_With_NoMatching_Ids_Returns_BadRequest()
        {
            //Arrange 
            var booksController = new BooksController(_context);
            var book = new Book { Id = 1, Title = "New Updated Title", Price = 10.5, Author = "John Doe", CoverImage = "/image.jpg", Description = "New Book Description" };

            //Act 
            var result = (BadRequestResult)await booksController.PutAsync(99, book);

            //Assert 
            Assert.AreEqual(StatusCodes.Status400BadRequest, result.StatusCode);
        }

        [TestMethod]
        public async Task PutAsync_With_Not_Existing_Ids_Returns_NotFound()
        {
            //Arrange 
            var booksController = new BooksController(_context);
            var book = new Book { Id = 99, Title = "New Updated Title", Price = 10.5, Author = "John Doe", CoverImage = "/image.jpg", Description = "New Book Description" };

            //Act 
            var result = (NotFoundResult)await booksController.PutAsync(99, book);

            //Assert 
            Assert.AreEqual(StatusCodes.Status404NotFound, result.StatusCode);
        }

        [TestMethod]
        public async Task DeleteAsync_With_Existing_Id_Returns_OK()
        {
            //Arrange 
            var booksController = new BooksController(_context);
            var firstItem = _context.Books.FirstOrDefault();

            //Act 
            var result = (OkResult)await booksController.DeleteAsync(firstItem.Id);

            //Assert 
            Assert.AreEqual(StatusCodes.Status200OK, result.StatusCode);
            Assert.IsNull(_context.Books.Find(1));
        }

        [TestMethod]
        public async Task DeleteAsync_With_Not_Existing_Id_Returns_NotFound()
        {
            //Arrange 
            var booksController = new BooksController(_context);

            //Act 
            var result = (NotFoundResult)await booksController.DeleteAsync(99);

            //Assert 
            Assert.AreEqual(StatusCodes.Status404NotFound, result.StatusCode);
        }
    }
}
