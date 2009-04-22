using System;
using System.Collections.Generic;
using System.Text;

namespace Vsf.Modelo
{
    public enum enumTipoRelatorio
    {
        Efetivo =0,
        Previsto,
        Devido
    }

    public class Relatorio
    {
        public Relatorio()
        { 
        }

        private string _mesReferencia;
        private int _numAlunos;
        private int _numProjetos;
        private decimal _mediaDiasAtrasados;
        private int _maiorAtraso;
        private decimal _valorTotal;
        private decimal _valorJuros;
        private decimal _valorPrevisto;

        public decimal ValorPrevisto
        {
            get { return _valorPrevisto; }
            set { _valorPrevisto = value; }
        }

        public decimal MediaDiasAtrasados
        {
            get { return _mediaDiasAtrasados; }
            set { _mediaDiasAtrasados = value; }
        }
        public int MaiorAtraso
        {
            get { return _maiorAtraso; }
            set { _maiorAtraso = value; }
        }
        public string MesReferencia
        {
            get { return _mesReferencia; }
            set { _mesReferencia = value; }
        }
        public int NumAlunos
        {
            get { return _numAlunos; }
            set { _numAlunos = value; }
        }
        public int NumProjetos
        {
            get { return _numProjetos; }
            set { _numProjetos = value; }
        }
        public decimal ValorTotal
        {
            get { return _valorTotal; }
            set { _valorTotal = value; }
        }
        public decimal ValorJuros
        {
            get { return _valorJuros; }
            set { _valorJuros = value; }
        }

    }
}
