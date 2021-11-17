using System.ComponentModel.DataAnnotations;

namespace Volare.Models
{
    public class TurmaViewModel : SuperViewModel
    {
        public int Semestre { get; set; }
        [Display(Name = "Curso")]
        public int CursoId { get; set; }
        public string CursoNome { get; set; }
        [Display(Name = "Quantidade de alunos")]
        public int QuantidadeAlunos { get; set; }
        public string Nome { get; set; }
    }
}