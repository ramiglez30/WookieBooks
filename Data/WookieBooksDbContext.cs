using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WookieBooks.Models;

namespace WookieBooks.Data
{
    public class WookieBooksDbContext: DbContext
    {
        public WookieBooksDbContext(DbContextOptions<WookieBooksDbContext> options): base(options)
        { }
        public DbSet<Book> Books { get; set; } 
    }
}
