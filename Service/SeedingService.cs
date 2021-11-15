using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ProjetoN2.DAO;
using ProjetoN2.Enums;
using ProjetoN2.Models;

namespace ProjetoN2.Service
{
    public class SeedingService : ISeedingService
    {
        private TurmaDAO turmaDAO;
        private SalaDAO salaDAO;
        private ProfessorDAO professorDAO;
        private MateriaDAO materiaDAO;
        private CursoDAO cursoDAO;
        private AulaDAO aulaDAO;
        private AlunoDAO alunoDAO;

        public SeedingService()
        {
            turmaDAO = new TurmaDAO();
            salaDAO = new SalaDAO();
            professorDAO = new ProfessorDAO();
            materiaDAO = new MateriaDAO();
            cursoDAO = new CursoDAO();
            aulaDAO = new AulaDAO();
            alunoDAO = new AlunoDAO();
        }
        public void Seed()
        {
            if(professorDAO.SelectAll().Count == 0 && salaDAO.SelectAll().Count == 0 && cursoDAO.SelectAll().Count == 0)
            {
                List<ProfessorViewModel> professores = new List<ProfessorViewModel>();
                professores.AddRange( new List<ProfessorViewModel>(){
                    new ProfessorViewModel(){ Nome = "Kakashi Hatake"},
                    new ProfessorViewModel(){ Nome = "Satoru Gojo"},
                    new ProfessorViewModel(){ Nome = "Mestre Kame"},
                    new ProfessorViewModel(){ Nome = "Silvers Rayleigh"},
                    new ProfessorViewModel(){ Nome = "Reborn"}
                });

                foreach (var item in professores)
                {
                    professorDAO.Insert(item);
                }
                
                List<SalaViewModel> salas = new List<SalaViewModel>();
                salas.AddRange( new List<SalaViewModel>(){
                    new SalaViewModel(){ Capacidade = 5},
                    new SalaViewModel(){ Capacidade = 10},
                    new SalaViewModel(){ Capacidade = 15},
                    new SalaViewModel(){ Capacidade = 20},
                    new SalaViewModel(){ Capacidade = 25}
                });

                foreach (var item in salas)
                {
                    salaDAO.Insert(item);
                }
                
                List<CursoViewModel> cursos = new List<CursoViewModel>();
                cursos.AddRange( new List<CursoViewModel>(){
                    new CursoViewModel(){ Nome = "Enhenharia de Computação", Periodo = EPeriodo.Noturno},
                    new CursoViewModel(){ Nome = "Administração", Periodo = EPeriodo.Noturno},
                    new CursoViewModel(){ Nome = "Enhenharia de Alimentos", Periodo = EPeriodo.Diurno},
                    new CursoViewModel(){ Nome = "Enhenharia de Automação", Periodo = EPeriodo.Diurno}
                });

                foreach (var item in cursos)
                {
                    cursoDAO.Insert(item);
                }
                
                List<TurmaViewModel> turmas = new List<TurmaViewModel>();
                turmas.AddRange( new List<TurmaViewModel>(){
                    new TurmaViewModel(){ Semestre = 1,  CursoId = 1},
                    new TurmaViewModel(){ Semestre = 1,  CursoId = 2},
                    new TurmaViewModel(){ Semestre = 1,  CursoId = 3},
                    new TurmaViewModel(){ Semestre = 1,  CursoId = 4},
                    new TurmaViewModel(){ Semestre = 2,  CursoId = 1},
                    new TurmaViewModel(){ Semestre = 2,  CursoId = 2},
                    new TurmaViewModel(){ Semestre = 2,  CursoId = 3},
                    new TurmaViewModel(){ Semestre = 2,  CursoId = 4},
                    new TurmaViewModel(){ Semestre = 3,  CursoId = 1},
                    new TurmaViewModel(){ Semestre = 3,  CursoId = 2},
                    new TurmaViewModel(){ Semestre = 3,  CursoId = 3},
                    new TurmaViewModel(){ Semestre = 3,  CursoId = 4}
                });

                foreach (var item in turmas)
                {
                    turmaDAO.Insert(item);
                }
                
                List<AlunoViewModel> alunos = new List<AlunoViewModel>();
                alunos.AddRange( new List<AlunoViewModel>(){
                    new AlunoViewModel(){ Nome = "Aluno1",  TurmaId = 1},
                    new AlunoViewModel(){ Nome = "Aluno2",  TurmaId = 1},
                    new AlunoViewModel(){ Nome = "Aluno3",  TurmaId = 1},
                    new AlunoViewModel(){ Nome = "Aluno4",  TurmaId = 1},
                    new AlunoViewModel(){ Nome = "Aluno15",  TurmaId = 1},
                });

                foreach (var item in alunos)
                {
                    alunoDAO.Insert(item);
                }
                
                List<MateriaViewModel> materias = new List<MateriaViewModel>();
                materias.AddRange( new List<MateriaViewModel>(){
                    new MateriaViewModel(){ Nome = "Materia1",  ProfessorId = 1, CursoId = 1},
                    new MateriaViewModel(){ Nome = "Materia2",  ProfessorId = 2, CursoId = 2},
                    new MateriaViewModel(){ Nome = "Materia3",  ProfessorId = 3, CursoId = 1},
                    new MateriaViewModel(){ Nome = "Materia4",  ProfessorId = 4, CursoId = 2}
                });

                foreach (var item in materias)
                {
                    materiaDAO.Insert(item);
                }

                List<AulaViewModel> aulas = new List<AulaViewModel>();
                aulas.AddRange( new List<AulaViewModel>(){
                    new AulaViewModel(){ MateriaId = 1, TurmaId = 1, SalaId = 1, Data = DateTime.UtcNow },
                    new AulaViewModel(){ MateriaId = 2, TurmaId = 1, SalaId = 2, Data = DateTime.UtcNow },
                    new AulaViewModel(){ MateriaId = 3, TurmaId = 1, SalaId = 3, Data = DateTime.UtcNow },
                    new AulaViewModel(){ MateriaId = 4, TurmaId = 1, SalaId = 4, Data = DateTime.UtcNow }
                });

                foreach (var item in aulas)
                {
                    aulaDAO.Insert(item);
                }
            }
        }
    }
}