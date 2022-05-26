
using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.DeleteBook;
using WebApi.BookOperations.GetBookDetail;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.UpdateBook;
using WebApi.DBOperations;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;
using static WebApi.BookOperations.UpdateBook.UpdateBookCommand;

namespace WebApi.AddControllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {   
        private readonly BookStoreDbContext _context;

        public BookController(BookStoreDbContext context)
        {
            _context = context;
        }
        
        //DbContext oluşturmadan önce aşağıdaki kod parçasını kullanarak
        //initial bir veri ekledik. Bu bizim için geçici bir çözümdü.
        // private static List<Book> BookList = new List<Book>()
        // {
        //     new Book{
        //         Id =1,
        //         Title ="Lean Startup",
        //         GenreId = 1, //Personal Growth
        //         PageCount = 200,
        //         PublishDate = new DateTime(2001,06,12)
        //     },
        //     new Book{
        //         Id =2,
        //         Title ="Herland",
        //         GenreId = 2, // Science Fiction
        //         PageCount = 250,
        //         PublishDate = new DateTime(2010,05,23)
        //     },
        //     new Book{
        //         Id =3,
        //         Title ="Dune",
        //         GenreId = 2, // Science Fiction
        //         PageCount = 540,
        //         PublishDate = new DateTime(2001,12,21)
        //     }
        // };

        [HttpGet]
        public IActionResult GetBooks() // Tüm veriler döndüren bir endpoint.
        {   //var book = Booklist.OrderBy(x=> x.Id).ToList<Book>();
            //veriyi içeriden static aldığımız zaman kullanılan kod yukardaki gibidir

            // var bookList = _context.Books.OrderBy(x=> x.Id).ToList<Book>();
            // return bookList;
            // veriyi DbContext üzerinde aldığımız zaman kullanılan kodd yukardaki gibidir.

            GetBooksQuery  query = new GetBooksQuery(_context);
            var result = query.Handle();
            return Ok(result);
        }
        //id'i çağırmak istediğimizde Route'tan çağırmak , yanlışlıkla farklı endpointlerin çalıştırılmamamsı için önemlidir.
        [HttpGet("{id}")]
        public IActionResult GetById(int id) 
            // sadece id üzerinden bir veri yollayan endpoint
        {   //var book = Booklist.Where(book=> book.Id == id).SingleOrDefault();  
            //veriyi içeriden static aldığımız zaman kullanılan kod yukardaki gibidir
            BookDetailViewModel result;
            try
            {
                GetBookDetailQuery query = new GetBookDetailQuery(_context);
                query.BookId = id;
                result = query.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(result);
            
            // var book = _context.Books.Where(book=> book.Id == id).SingleOrDefault();
            // return book;
        }

        // [HttpGet]
        // public Book Get([FromQuery] string id) // Tüm veriler döndüren bir endpoint.
        // {
        //     var book = BookList.Where(book=> book.Id == Convert.ToInt32(id)).SingleOrDefault();
        //     return book;
        // }

        //Post
        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand command = new CreateBookCommand(_context);
            try
            {
                command.Model = newBook;
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
             return Ok();
            // var book = _context.Books.SingleOrDefault(x=> x.Title == newBook.Title);
            // if (book is not null)
            // {
            //     return BadRequest();
            // }else
            // {    //BookList.Add(newBok); veriyi içerden static aldığımız zaman kullanılan kod
            //     _context.Books.Add(newBook);
            //     _context.SaveChanges();
                // return Ok();
            // }  
        }


        //Put
        [HttpPut("{id}")]

        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook)
        {
            try
            {
                 UpdateBookCommand command = new UpdateBookCommand(_context);
                 command.BookId = id; 
                 command.Model = updatedBook;
                 command.Handle();
            }
            catch (Exception ex)
            {
               return BadRequest(ex.Message);
            }
            return Ok();
           
            // var book = _context.Books.SingleOrDefault(x=> x.Id == id);
            // if(book is null)
            //     return BadRequest();

            // book.GenreId = updatedBook.GenreId != default ? updatedBook.GenreId : book.GenreId;
            // book.PageCount = updatedBook.PageCount != default ? updatedBook.PageCount : book.GenreId;
            // book.PublishDate = updatedBook.PublishDate != default ? updatedBook.PublishDate : book.PublishDate;
            // book.Title = updatedBook.Title != default ? updatedBook.Title : book.Title;

            // _context.SaveChanges();
            // return Ok();
        }

        [HttpDelete("{id}")]

        public IActionResult DeleteBook(int id)
        {
            try
            {
                DeleteBookCommand command = new DeleteBookCommand(_context);
                command.BookId = id;
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
            // var book = _context.Books.SingleOrDefault(x=> x.Id == id);
            // if(book is null)
            //     return BadRequest();

            // _context.Books.Remove(book);
            // _context.SaveChanges();
            // return Ok();
        }
    }
}