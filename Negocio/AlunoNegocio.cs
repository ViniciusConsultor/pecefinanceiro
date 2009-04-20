using System;
using System.Collections.Generic;
using System.Text;
using Vsf.DAL;
using Vsf.Modelo;

namespace Vsf.Negocio
{
    public class AlunoNegocio
    {
        AlunoDAO alunodao = new AlunoDAO();

        public Vsf.Modelo.AlunoProjeto ObterRelacionamentoAlunoProjeto(int codigoAluno, string codigoProjeto)
        {
            return AlunoDAO.ObterRelacionamentoAlunoProjeto(codigoAluno, codigoProjeto);
        }

        public bool InserirAluno(Vsf.Modelo.Aluno aluno)
        {
            return alunodao.InserirAluno(aluno);
            
        }

        public bool InserirMatriculas(Aluno aluno, List<Projeto> projetosDoAluno)
        {
           return alunodao.InserirVariasMatriculas(aluno, projetosDoAluno);
            
        }

        public List<Aluno> ObterTodosAlunos()
        {
            return alunodao.ObterTodosAlunos();

        }


        public Aluno ObterAlunoPeloNumeroPece(int numeroPece)
        {
            return alunodao.ObterAlunoPeloNumeroPece(numeroPece);
        }
    }
}
