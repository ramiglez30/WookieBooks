using Microsoft.EntityFrameworkCore;
using System.Linq;
using WookieBooks.Models;

namespace WookieBooks.Data
{
    public static class DatabaseSeed
    {
        public static void Seed(this DbContextOptionsBuilder options)
        {
            using (var context = new WookieBooksDbContext((DbContextOptions<WookieBooksDbContext>)options.Options))
            {
                if (context.Books.Any())
                {
                    return;
                }

                Book theforce = new() { Title = "The Force Awakens", Author = "Steven Spielberg", Description = "Episode VII of the Star War. saga.", Price = 10.00, CoverImage = "/image.jpg" };
                Book thelast = new() { Title = "The Last Jedi", Author = "Steven Spielberg", Description = "Episode VIII of the Star Wars saga.", Price = 15.00, CoverImage = "/image.jpg" };

                context.Books.Add(theforce);
                context.Books.Add(thelast);
                context.SaveChanges();
            }
        }
    }
}
