using System;
using System.Collections.Generic;
using System.Text;

namespace Vsf.Modelo
{
    public class Projeto
    {
        private string _codigo;
        private string _nome;
        private string _descricao;
        private double _valor;
        private List<Aluno> _alunos;


        public string Codigo
        {
            get { return _codigo; }
            set { _codigo = value; }
        }

        public string Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }

        public string Descricao
        {
            get { return _descricao; }
            set { _descricao = value; }
        }

        public double Valor
        {
            get { return _valor; }
            set { _valor = value; }
        }

        public List<Aluno> Alunos
        {
            get { return _alunos; }
            set { _alunos = value; }
        }
    }
}
