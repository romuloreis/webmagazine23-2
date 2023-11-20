using Microsoft.EntityFrameworkCore;
using WebMagazine.Models;

namespace WebMagazine.Data
{
    public class WebMagazineContext : DbContext
    {
        public WebMagazineContext(DbContextOptions<WebMagazineContext> options)
            : base(options)
        {
        }
        /*Tabelas do banco de dados que teremos acesso*/
        public DbSet<Department> Department { get; set; } = default!;
        public DbSet<Seller> Seller { get; set; } = default!;
        public DbSet<SalesRecord> SalesRecord { get; set; } = default!;
    }
}
