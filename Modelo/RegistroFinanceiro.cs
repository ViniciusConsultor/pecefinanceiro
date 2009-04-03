using System;
using System.Collections.Generic;
using System.Text;

namespace Vsf.Modelo
{
    public enum StatusRegistroFinanceiro
    {
        Atrasado, EmDia, Cancelado
    }

    public class RegistroFinanceiro
    {
        private int _numeroParcelas;
        private double _precoReajustado;
        private string _observacoes;
        private int _diaVencimento;
        private DateTime _dataVencimentoPrimeiraParcela;
        private StatusRegistroFinanceiro _status;

        public int NumeroParcelas
        {
            get { return _numeroParcelas; }
            set { _numeroParcelas = value; }
        }

        public double PrecoReajustado
        {
            get { return _precoReajustado; }
            set { _precoReajustado = value; }
        }

        public string Observacoes
        {
            get { return _observacoes; }
            set { _observacoes = value; }
        }
        public int DiaVencimento
        {
            get { return _diaVencimento; }
            set { _diaVencimento = value; }
        }

        public DateTime DataVencimentoPrimeiraParcela
        {
            get { return _dataVencimentoPrimeiraParcela; }
            set { _dataVencimentoPrimeiraParcela = value; }
        }

        public StatusRegistroFinanceiro Status
        {
            get { return _status; }
            set { _status = value; }
        }
    }
}
