using System.ComponentModel.DataAnnotations;

namespace Volare.Models
{
    public class AlunoViewModel : SuperViewModel
    {
        public string Nome { get; set; }
        [Display(Name = "Turma")]
        public int TurmaId { get; set; }
        public string TurmaNome { get; set; }
    }
}