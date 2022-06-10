using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Entities;

namespace WebApi.DBOperations
{
    public class Datagenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                if (context.Books.Any())
                {
                    return;
                }else
                {
                    context.Authors.AddRange(
                        new Author
                        {
                            FirstName = "Eric",
                            LastName  = "Ries",
                            BirthDate =  new DateTime(1978,09,22)                           
                        },
                        new Author
                        {
                            FirstName = "Frank",
                            LastName  = "Herbert",
                            BirthDate =  new DateTime(1920,10,08)                           
                        },
                        new Author
                        {
                            FirstName = "Charlotte Perkins",
                            LastName  = "Gilman",
                            BirthDate =  new DateTime(1860,07,03)                           
                        }
                    );
                    context.Genres.AddRange(
                        new Genre
                        {
                            Name = "Personel Growth"
                        },
                        new Genre
                        {
                            Name = "Science Fiction"
                        },
                        new Genre
                        {
                            Name = "Romance"
                        }
                        
                    );
                    context.Books.AddRange(
                        new Book{
                            // Id =1,
                            Title ="Lean Startup",
                            GenreId = 1,
                            AuthorId = 1,
                            PageCount = 200,
                            PublishDate = new DateTime(2001,06,12)
                        },
                        new Book{
                            // Id =2,
                            Title ="Herland",
                            GenreId = 2,
                            AuthorId = 3,
                            PageCount = 250,
                            PublishDate = new DateTime(2010,05,23)
                        },
                        new Book{
                            // Id =3,
                            Title ="Dune",
                            GenreId = 2,
                            AuthorId = 2,
                            PageCount = 540,
                            PublishDate = new DateTime(2001,12,21)
                        });

                        context.SaveChanges();
                }
            }
        }
    }
}