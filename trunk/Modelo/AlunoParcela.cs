using System;
using System.Collections.Generic;
using System.Text;

namespace Vsf.Modelo
{
    public class AlunoParcela : Aluno
    {
        private int _numeroParcela;

        public int NumeroParcela
        {
            get { return _numeroParcela; }
            set { _numeroParcela = value; }
        }

        private DateTime _parcelaVencida;

        public DateTime ParcelaVencida
        {
            get { return _parcelaVencida; }
            set { _parcelaVencida = value; }
        }
        private decimal _valorParcela;

        public decimal ValorParcela
        {
            get { return _valorParcela; }
            set { _valorParcela = value; }
        }
    }

}
