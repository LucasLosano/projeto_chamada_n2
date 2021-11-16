namespace Volare.Models
{
    public class MateriaViewModel : SuperViewModel
    {
        public string Nome { get; set; }
        public int CursoId { get; set; }
        public string CursoNome { get; set; }
        public int ProfessorId { get; set; }
        public string ProfessorNome { get; set; }
    }
}