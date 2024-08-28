using System.ComponentModel.DataAnnotations;

namespace GerenciadorDeTarefasAPI.Models
{
    public class Tarefa
    {
        public int Id { get; set; }

        [Required]
        public string? Titulo { get; set; }

        public string? Descricao { get; set; }

        public bool Concluida { get; set; }
    }
}
