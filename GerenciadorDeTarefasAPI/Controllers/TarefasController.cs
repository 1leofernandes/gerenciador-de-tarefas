using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GerenciadorDeTarefasAPI.Data;
using GerenciadorDeTarefasAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GerenciadorDeTarefasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarefasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TarefasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/tarefas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tarefa>>> GetTarefas()
        {
            return await _context.Tarefas.ToListAsync();
        }

        // POST: api/tarefas
        [HttpPost]
        public async Task<ActionResult<Tarefa>> PostTarefa(Tarefa tarefa)
        {
            _context.Tarefas.Add(tarefa);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTarefas), new { id = tarefa.Id }, tarefa);
        }

        // PUT: api/tarefas/{id}/concluir
        [HttpPut("{id}/concluir")]
        public async Task<IActionResult> ConcluirTarefa(int id)
        {
            var tarefa = await _context.Tarefas.FindAsync(id);
            if (tarefa == null)
            {
                return NotFound();
            }

            tarefa.Concluida = true;
            _context.Entry(tarefa).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(tarefa);  // Retorne a tarefa atualizada
        }

        // DELETE: api/tarefas/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTarefa(int id)
        {
            var tarefa = await _context.Tarefas.FindAsync(id);
            if (tarefa == null)
            {
                return NotFound();
            }

            _context.Tarefas.Remove(tarefa);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

