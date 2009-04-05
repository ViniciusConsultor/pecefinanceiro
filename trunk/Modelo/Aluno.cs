using System;
using System.Collections.Generic;
using System.Text;

namespace Vsf.Modelo
{
    public class Aluno
    {
        private int _numeroPece;
        private string _nome;
        private string _endereco;
        private string _telefone;

        public Aluno()
        {

        }

        public Aluno(int numeroPece, string nome, string endereco, string telefone)
        {
            _numeroPece = numeroPece;
            _nome = nome;
            _endereco = endereco;
            _telefone = telefone;
        }

        public int NumeroPece
        {
            get { return _numeroPece; }
            set { _numeroPece = value; }
        }

        public string Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }

        public string Endereco
        {
            get { return _endereco; }
            set { _endereco = value; }
        }

        public string Telefone
        {
            get { return _telefone; }
            set { _telefone = value; }
        }

    }
}
