using System;
using System.Collections.Generic;
using System.Text;

namespace Vsf.Modelo
{
    public class Parcela
    {
        private int _numeroParcela;
        private DateTime _dtVencimento;
        private double _valorParcela;
        private double _valorPago;
        private string _observacaoPagamento;

        public int NumeroParcela
        {
            get { return _numeroParcela; }
            set { _numeroParcela = value; }
        }


        public DateTime DtVencimento
        {
            get { return _dtVencimento; }
            set { _dtVencimento = value; }
        }


        public double ValorParcela
        {
            get { return _valorParcela; }
            set { _valorParcela = value; }
        }

        public double ValorPago
        {
            get { return _valorPago; }
            set { _valorPago = value; }
        }

        public string ObservacaoPagamento
        {
            get { return _observacaoPagamento; }
            set { _observacaoPagamento = value; }
        }
    }
}
