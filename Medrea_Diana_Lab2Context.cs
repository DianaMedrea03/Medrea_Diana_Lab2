using Microsoft.EntityFrameworkCore;

public class Medrea_Diana_Lab2Context(DbContextOptions<Medrea_Diana_Lab2Context> options) : DbContext(options)
{
    public DbSet<Medrea_Diana_Lab2.Models.Book> Book { get; set; } = default!;
    public DbSet<Medrea_Diana_Lab2.Models.Publisher> Publisher { get; set; } = default!;
    public DbSet<Medrea_Diana_Lab2.Models.Authors> Authors { get; set; } = default!;
}
