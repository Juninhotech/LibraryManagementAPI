using LibraryManagementAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementAPI.Data
{
    public static class DatabaseSeeder
    {
        public static async Task SeedAsync(LibraryManagementDbContext context)
        {
            await context.Database.MigrateAsync();

            if (!await context.Books.AnyAsync())
            {
                var books = new List<Book>
                {
                    new Book
                    {
                        Title = "Clean Code",
                        Author = "Robert C. Martin",
                        ISBN = "978-0132350884",
                        PublishedDate = new DateTime(2008, 8, 1)
                    },
                    new Book
                    {
                        Title = "The Pragmatic Programmer",
                        Author = "Andrew Hunt and David Thomas",
                        ISBN = "978-0135957059",
                        PublishedDate = new DateTime(2019, 9, 13)
                    },
                    new Book
                    {
                        Title = "Design Patterns",
                        Author = "Gang of Four",
                        ISBN = "978-0201633610",
                        PublishedDate = new DateTime(1994, 10, 31)
                    },
                    new Book
                    {
                        Title = "C# in Depth",
                        Author = "Jon Skeet",
                        ISBN = "978-1617294532",
                        PublishedDate = new DateTime(2019, 3, 15)
                    },
                    new Book
                    {
                        Title = "Refactoring",
                        Author = "Martin Fowler",
                        ISBN = "978-0134757599",
                        PublishedDate = new DateTime(2018, 11, 20)
                    }
                };

                await context.Books.AddRangeAsync(books);
                await context.SaveChangesAsync();
            }
        }
    }
}