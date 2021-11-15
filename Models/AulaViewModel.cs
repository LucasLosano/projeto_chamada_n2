using System;

namespace ProjetoN2.Models
{
    public class AulaViewModel : SuperViewModel
    {
        public DateTime Data { get; set; }
        public int MateriaId { get; set; }
        public string MateriaNome { get; set; }
        public int TurmaId { get; set; }
        public string TurmaNome { get; set; }
        public int SalaId { get; set; }
        public int ProfessorId { get; set; }
    }
}