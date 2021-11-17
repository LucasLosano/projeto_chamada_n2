using System.ComponentModel.DataAnnotations;

namespace Volare.Models
{
    public class MateriaViewModel : SuperViewModel
    {
        public string Nome { get; set; }
        [Display(Name = "Curso")]
        public int CursoId { get; set; }
        public string CursoNome { get; set; }
        [Display(Name = "Professor")]
        public int ProfessorId { get; set; }
        public string ProfessorNome { get; set; }
    }
}