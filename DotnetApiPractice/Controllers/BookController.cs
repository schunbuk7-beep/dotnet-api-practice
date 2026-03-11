using Microsoft.AspNetCore.Mvc;
using DotnetApiPractice.Models;

namespace DotnetApiPractice.Controller;

[ApiController]
[Route("[Controller]")]
public class BookController : ControllerBase
{
    public static List<Book> books = new List<Book>
    {
        new Book {Id=1, Title = "The Metamorphosis",Author = "Franz Kafka" },
        new Book {Id=2, Title = "The Palace of illusion", Author = "Chitra Banarjee"},
        new Book {Id=3, Title = "Man's Searching for Meaning", Author = "Viktor E. Frankl"}
    };

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(books);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
       var book = books.FirstOrDefault(b => b.Id == id);

       if(book == null)
       {
        return NotFound(new {Message = $"Book with ID {id} not found"});
       }
       return Ok(book);
    }
    
    //Post/Books
    [HttpPost]
    public IActionResult AddBook ([FromBody]Book book)
    {
        books.Add(book);
        return CreatedAtAction(nameof(GetById),new {id = book.Id}, book);
    }

    //Put/Books/id
    [HttpPut("{id}")]
    public IActionResult UpdateBook(int id,[FromBody] Book updatedBook)
    {
        var book = books.FirstOrDefault(b => b.Id == id);

        if(book == null)
        return NotFound(new {Message = $"Book with id {id} was not found"});

        book.Title = updatedBook.Title;
        book.Author = updatedBook.Author;

        return Ok(book);
    }

    //Delete/books/id
    [HttpDelete("{id}")]
    public IActionResult DeleteBook(int id)
    {
        var book = books.FirstOrDefault(b => b.Id == id);

        if(book == null)
        return NotFound(new{Message = $"Book with id {id} not found"});

        books.Remove(book);
        return NoContent();
    }
}