using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace MariaDbSample
{
    [Table("Book")]
    public class Book
    {
        public long BookID { get; set; }
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }
        public bool InStock { get; set; }
        public double Price { get; set; }
        public virtual BookCategory Category { get; set; }
    }

    [Table("Category")]
    public class BookCategory
    {
        [Key]
        public long CategoryID { get; set; }
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
        public virtual ICollection<Book> Books { get; set; }
    }

    public partial class BeautyBooksContext : DbContext
    {
        public static readonly LoggerFactory EFLoggerFactory
            = new LoggerFactory(new[] {new ConsoleLoggerProvider((cat, level) =>
            cat== DbLoggerCategory.Database.Command.Name && level==LogLevel.Information,true)});

        public DbSet<Book> Books { get; set; }
        public DbSet<BookCategory> BookCategories { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(EFLoggerFactory);
            optionsBuilder.UseMySql(@"uid=root;pwd=rootşifre;Host=localhost;Database=BeautyBooks;");
        }
    }

    public class Program
    {
        public static void Main()
        {
            using (var context = new BeautyBooksContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var scienceCategory = new BookCategory()
                {
                    Name = "Science"
                };
                var programmingCategory = new BookCategory()
                {
                    Name = "Programming"
                };
                var mathCategory = new BookCategory()
                {
                    Name = "Math"
                };
                context.BookCategories.Add(scienceCategory);
                context.BookCategories.Add(programmingCategory);

                context.Books.Add(new Book()
                {
                    Title = "2025 : Go to the MARS",
                    Price = 8.99,
                    InStock = true,
                    Category = scienceCategory
                });
                context.Books.Add(new Book()
                {
                    Title = "The 9nth Element",
                    Price = 6.99,
                    InStock = false,
                    Category = scienceCategory
                });
                context.Books.Add(new Book()
                {
                    Title = "Calculus - I",
                    Price = 19.99,
                    InStock = false,
                    Category = mathCategory
                });
                context.Books.Add(new Book()
                {
                    Title = "Advanced Asp.Net Core 2.0",
                    Price = 38.10,
                    InStock = true,
                    Category = programmingCategory
                });
                context.Books.Add(new Book()
                {
                    Title = "C# 7.0 Introduction",
                    Price = 15.33,
                    InStock = false,
                    Category = programmingCategory
                });
                context.Books.Add(new Book()
                {
                    Title = "Vue.js for Dummies",
                    Price = 28.49,
                    InStock = false,
                    Category = programmingCategory
                });
                context.Books.Add(new Book()
                {
                    Title = "GoLang - The New Era",
                    Price = 55.55,
                    InStock = false,
                    Category = programmingCategory
                });

                context.SaveChanges();

                Console.WriteLine("Book List\n");

                var query = context.Books.Include(p => p.Category)
                    .Where(p => p.Price < 30.0)
                    .ToList();

                Console.WriteLine("{0,-8} | {1,-50} | {2,-8} | {3}\n\n", "BookID", "Title", "Price", "Category");
                foreach (var book in query)
                    Console.WriteLine("{0,-8} | {1,-50} | {2,-8} | {3}", book.BookID, book.Title, book.Price, book.Category.Name);

                Console.WriteLine("Press any key to exit");
                Console.ReadKey();
            }
        }
    }
}