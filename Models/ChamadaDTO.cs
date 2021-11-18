using System;

namespace Volare.Models
{
    public class ChamadaDTO
    {
        public int AlunoId { get; set; }
        public int SalaId { get; set; }
        public int TurmaId { get; set; }
        public DateTime Chegada { get; set; }
    }
}