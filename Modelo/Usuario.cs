using System;
using System.Collections.Generic;
using System.Text;

namespace Vsf.Modelo
{
    public class Usuario
    {
        private int _idUsuario;
        private string _login;
        private string _nome;
        private int _tipo;

        public int IdUsuario
        {
            get { return _idUsuario; }
            set { _idUsuario = value; }
        }
        public string Login
        {
            get { return _login; }
            set { _login = value; }
        }
        public string Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }
        public int Tipo
        {
            get { return _tipo; }
            set { _tipo = value; }
        }

        }

    
}
