using System;
using System.Collections.Generic;
using System.Text;

namespace Vsf.Modelo
{
    public class LogEntry
    {
        private Usuario _usuario;
        private string _texto;

        public Usuario Usuario
        {
            get { return _usuario; }
            set { _usuario = value; }
        }

        public string Texto
        {
            get { return _texto; }
            set { _texto = value; }
        }
    }
}
