using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Volare.DAO;
using Volare.Enums;
using Volare.Models;

namespace Volare.Service
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
        private ChamadaDAO chamadaDAO;

        public SeedingService()
        {
            turmaDAO = new TurmaDAO();
            salaDAO = new SalaDAO();
            professorDAO = new ProfessorDAO();
            materiaDAO = new MateriaDAO();
            cursoDAO = new CursoDAO();
            aulaDAO = new AulaDAO();
            alunoDAO = new AlunoDAO();
            chamadaDAO = new ChamadaDAO();
        }
        public void Seed()
        {
            using(var transaction = new System.Transactions.TransactionScope())
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
                        new SalaViewModel(){ Capacidade = 40},
                        new SalaViewModel(){ Capacidade = 40},
                        new SalaViewModel(){ Capacidade = 40}
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
                    for (int i = 1; i <= 40; i++)
                    {
                        alunos.Add( new AlunoViewModel(){Nome = "Aluno" + i, TurmaId = 1});
                    }

                    foreach (var item in alunos)
                    {
                        alunoDAO.Insert(item);
                    }
                    
                    List<MateriaViewModel> materias = new List<MateriaViewModel>();
                    materias.AddRange( new List<MateriaViewModel>(){
                        new MateriaViewModel(){ Nome = "Materia1",  ProfessorId = 1, CursoId = 1},
                        new MateriaViewModel(){ Nome = "Materia2",  ProfessorId = 2, CursoId = 1},
                        new MateriaViewModel(){ Nome = "Materia3",  ProfessorId = 3, CursoId = 1},
                        new MateriaViewModel(){ Nome = "Materia4",  ProfessorId = 1, CursoId = 1},
                        new MateriaViewModel(){ Nome = "Materia5",  ProfessorId = 2, CursoId = 1},
                    });

                    foreach (var item in materias)
                    {
                        materiaDAO.Insert(item);
                    }

                    DateTime dateTime = DateTime.UtcNow;
                    TimeSpan ts = new TimeSpan(19, 15, 0);
                    dateTime = dateTime.Date + ts;
                    List<AulaViewModel> aulas = new List<AulaViewModel>();
                    for (int i = 0; i < 20; i++)
                    {
                        dateTime = dateTime.AddDays(2);
                        aulas.AddRange( new List<AulaViewModel>(){
                            new AulaViewModel(){ MateriaId = 1, TurmaId = 1, SalaId = 1, Data = dateTime },
                            new AulaViewModel(){ MateriaId = 2, TurmaId = 1, SalaId = 2, Data = dateTime.AddHours(2) },
                            new AulaViewModel(){ MateriaId = 3, TurmaId = 1, SalaId = 3, Data = dateTime.AddDays(1) },
                            new AulaViewModel(){ MateriaId = 4, TurmaId = 1, SalaId = 1, Data = dateTime.AddDays(1).AddHours(2) }
                        });
                    }

                    foreach (var item in aulas)
                    {
                        aulaDAO.Insert(item);
                    }

                    foreach (var aula in aulaDAO.SelectAll())
                    {
                        var listaAlunos = alunoDAO.SelectAll();
                        listaAlunos.RemoveAll(a => new Random().Next(1,11) == 1);

                        foreach (var item in listaAlunos)
                        {
                            chamadaDAO.Insert(new ChamadaViewModel(){ AulaId = aula.Id, AlunoId = item.Id});
                        }
                    }
                    transaction.Complete();
                }
                
            }
        }
    }
}