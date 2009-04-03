using System;
using System.Collections.Generic;
using System.Text;

namespace Vsf.Modelo
{
    public enum StatusAlunoProjeto
	{
	    Inativo, Ativo
	}
    public class AlunoProjeto
    {

        private int _id;
        private Aluno _aluno;
        private Projeto _projeto;
        private StatusAlunoProjeto _status;
        
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public Aluno Aluno
        {
            get { return _aluno; }
            set { _aluno = value; }
        }

        public Projeto Projeto
        {
            get { return _projeto; }
            set { _projeto = value; }
        }

        public StatusAlunoProjeto Status
        {
            get { return _status; }
            set { _status = value; }
        }

        public String NomeAluno
        {
            get { return _aluno.Nome; }
        }

        public String NomeProjeto
        {
            get { return _projeto.Nome; }
        }
    }
}
