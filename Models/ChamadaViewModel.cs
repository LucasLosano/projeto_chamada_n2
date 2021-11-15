using System;

namespace ProjetoN2.Models
{
    public class ChamadaViewModel
    {
        public int AulaId { get; set; }
        public int AlunoId { get; set; }
        public string TurmaNome { get; set; }
        public string MateriaNome { get; set; }
        public string AlunoNome { get; set; }
        public string Status { get; set; }
        public DateTime AulaData { get; set; }
    }
}