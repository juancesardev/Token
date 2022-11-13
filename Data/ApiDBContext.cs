using JwTokens.Models;
using Microsoft.EntityFrameworkCore;

namespace JwTokens.Data
{
    public class ApiDBContext : DbContext
    {
        public ApiDBContext(DbContextOptions<ApiDBContext> options) :
            base(options)
        {
        }

        //TODO DbSeet
        public DbSet<User> Users { get; set; }
        public DbSet<Curso> Cursos { get; set; }
    }
}
