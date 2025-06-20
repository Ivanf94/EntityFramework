using Microsoft.EntityFrameworkCore;
using EF_code_first.Models;

namespace EF_code_first.Data
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options) : base(options)
        {

        }

        public DbSet<TodoModel> TodoModel { get; set; }
    }
}
