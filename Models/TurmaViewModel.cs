namespace Volare.Models
{
    public class TurmaViewModel : SuperViewModel
    {
        public int Semestre { get; set; }
        public int CursoId { get; set; }
        public string CursoNome { get; set; }
        public int QuantidadeAlunos { get; set; }
    }
}