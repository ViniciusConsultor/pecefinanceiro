using System;
using System.Collections.Generic;
using System.Text;
using Vsf.DAL;
using Vsf.Modelo;
using Vsf.Common;


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

        
        public bool AtualizarAluno(Aluno aluno, List<Projeto> projetos)
        {
            return (alunodao.AtualizarAluno(aluno) && AtualizarMatriculasDoAluno(aluno,projetos));
        }

        private bool AtualizarMatriculasDoAluno(Aluno aluno, List<Projeto> projetosAtualizados)
        {
            List<Projeto> ProjetosAtuaisDoAluno = ProjetoDAO.ObterProjetosDoAluno(aluno.NumeroPece);
            bool exclusãoOK = true;
            bool inclusaoOK = true;

            //Exclui a matriculas que sairam
            foreach (Projeto projeto in ProjetosAtuaisDoAluno)
            {

                if (!projetoPertenceALista(projeto, projetosAtualizados))
                {
                    if (!alunodao.ExcluirMatricula(aluno, projeto))
                    {
                        exclusãoOK = false;
                    }
                }
            }
            
            //Inclui novas matriculas que sairam
            foreach (Projeto projeto in projetosAtualizados)
            {
                if (!projetoPertenceALista(projeto, ProjetosAtuaisDoAluno))
                {
                    if (!alunodao.InserirMatricula(aluno, projeto))
                    {
                        inclusaoOK = false;
                    }
                }
            }

            return (exclusãoOK && inclusaoOK);

        }

        private bool projetoPertenceALista(Projeto projeto, List<Projeto> ListaDeProjetos)
        {
            

            foreach (Projeto projetoDaLista in ListaDeProjetos)
            {
                if (projeto.Codigo.Equals(projetoDaLista.Codigo))
                {
                    return true;
                }
            }
            return false;
        }

        private bool Incluirmatriculas(Aluno aluno, List<Projeto> ProjetosAtuaisDoAluno, List<Projeto> projetosAtualizados)
        {
            throw new NotImplementedException();
        }

        private bool ExcluirMatriculas(Aluno aluno,List<Projeto> ProjetosAtuaisDoAluno, List<Projeto> projetosAtualizados)
        {
            bool exclusãoOK=true;
            
            return exclusãoOK;
        }

        public List<Aluno> BuscaAlunoPeloNumeroPeceENome(string p, string p_2)
        {
            throw new NotImplementedException();
        }

        public bool AlunoPossuiMatricula(int AlunoNumeroPece)
        {
            return alunodao.AlunoPossuiMatricula(AlunoNumeroPece);
        }

        public bool RemoverAluno(int numeropece)
        {
            return alunodao.RemoverAluno(numeropece);
        }
    }
}
