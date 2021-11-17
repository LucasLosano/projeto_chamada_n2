using System;
using System.ComponentModel.DataAnnotations;

namespace Volare.Models
{
    public class AulaViewModel : SuperViewModel
    {
        public DateTime Data { get; set; }
        [Display(Name = "Materia")]
        public int MateriaId { get; set; }
        public string MateriaNome { get; set; }
        [Display(Name = "Turma")]
        public int TurmaId { get; set; }
        public string TurmaNome { get; set; }
        [Display(Name = "Sala")]
        public int SalaId { get; set; }
        public int ProfessorId { get; set; }
    }
}