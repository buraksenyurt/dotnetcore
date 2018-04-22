using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartReaderApi.Models;

namespace SmartReaderApi.Controllers
{
    [Route("smarty/api/[controller]")]
    public class BooksController : Controller
    {
        static List<Book> _books=new List<Book>{
                new Book{BookID=1023,Title="Essential C# 6.0"},
                new Book{BookID=985,Title="Big Java, Early Objects"},
                new Book{BookID=124,Title="Kralın Düşüşü"},
            };

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new { Books = _books });
        }

        [HttpPost]
        public IActionResult Create([FromBody]Book book)
        {
            _books.Add(book);
            return Ok(book);
        }

        [HttpPut("{bookId}")]
        public IActionResult Update(int bookID, [FromBody]Book book)
        {
            var findResult = _books.Find(b=>b.BookID==bookID);
            if (findResult == null)
            {
                return NotFound();
            }
            findResult.Title = book.Title;
            return Ok(findResult);
        }

        [HttpDelete("{bookId}")]
        public IActionResult Delete(int bookID)
        {
            var findResult = _books.Find(b=>b.BookID==bookID);
            if (findResult == null)
            {
                return NotFound();
            }
            _books.Remove(findResult);
            return Ok(findResult);
        }
    }
}
