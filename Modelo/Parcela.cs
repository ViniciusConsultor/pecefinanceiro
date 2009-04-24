using System;
using System.Collections.Generic;
using System.Text;

namespace Vsf.Modelo
{
    public class Parcela
    {
        private int _numeroParcela;
        private DateTime _dataVencimento;
        private DateTime _dataPagamento;
        private double _valorParcela;
        private double _valorPago;
        private string _observacaoPagamento;
        private bool _pago = false;

        public bool Pago
        {
            get { return _pago; }
            set { _pago = value; }
        }

        public Parcela(int numeroParcela, DateTime dataVencimento, Double valorParcela)
        {
            this._numeroParcela = numeroParcela;
            this._dataVencimento = dataVencimento;
            this._valorParcela = valorParcela;
        }

        public Parcela(int numeroParcela, DateTime dataPagamento, Double valorPagamento, bool pago)
        {
            this._numeroParcela = numeroParcela;
            this._dataPagamento = dataPagamento;
            this._valorPago = valorPagamento;
            this._pago = pago;
        }

        public Parcela()
        {

        }

        public int NumeroParcela
        {
            get { return _numeroParcela; }
            set { _numeroParcela = value; }
        }


        public DateTime DataVencimento
        {
            get { return _dataVencimento; }
            set { _dataVencimento = value; }
        }

        public DateTime DataPagamento
        {
            get { return _dataPagamento; }
            set { _dataPagamento = value; }
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
