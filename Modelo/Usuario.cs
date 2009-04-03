using System;
using System.Collections.Generic;
using System.Text;

namespace Vsf.Modelo
{
    public class Usuario
    {
        private int _idUsuario;
        private string _nomeUsuario;

        public int IdUsuario
        {
            get { return _idUsuario; }
            set { _idUsuario = value; }
        }

        public string NomeUsuario
        {
            get { return _nomeUsuario; }
            set { _nomeUsuario = value; }
        }
    }
}
