using Microsoft.EntityFrameworkCore;

namespace Thinkland.Data.Context;

public class AppDbContext : DbContext
{

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }

}