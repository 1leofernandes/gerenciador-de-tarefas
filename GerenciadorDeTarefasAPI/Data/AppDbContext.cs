using Microsoft.EntityFrameworkCore;
using GerenciadorDeTarefasAPI.Models;

namespace GerenciadorDeTarefasAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Tarefa> Tarefas { get; set; }
    }
}
