using WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Data
{
  public class Core2DbContext : DbContext
  {
    public Core2DbContext(DbContextOptions<Core2DbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
  }
}