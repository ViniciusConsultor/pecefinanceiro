using System;
using System.Collections.Generic;
using System.Text;
using Vsf.Modelo;
using Vsf.DAL;

namespace Vsf.Negocio
{
    public class ParcelaNegocio
    {
        public List<Parcela> ObterParcelasPorRegistro(int idRegistro)
        {
            return ParcelaDAO.ObterParcelasPorRegistro(idRegistro);
        }

        public bool InserirParcela(Parcela parcela, int idRegistro)
        {
            return Vsf.DAL.ParcelaDAO.InserirParcela(parcela, idRegistro);
        }

        public bool RemoverParcela(Parcela parcela, int idRegistro)
        {
            return false;
        }

        public bool RemoverParcela(int idParcela)
        {
            return false;
        }

        public bool EditarParcela(int idParcela, Parcela parcela)
        {
            return false;
        }

        public List<Parcela> GerarParcelas(RegistroFinanceiro registroFinanceiro)
        {
            Int32 nParcelas = registroFinanceiro.NumeroParcelas;
            Double valorParcela = registroFinanceiro.PrecoReajustado / Convert.ToDouble(nParcelas);
            List<Parcela> listParcelas = new List<Parcela>();
            Parcela primeiraParcela = new Parcela(1, registroFinanceiro.DataVencimentoPrimeiraParcela, valorParcela);
            this.InserirParcela(primeiraParcela, registroFinanceiro.IdRegistro);
            listParcelas.Add(primeiraParcela);

            for (int i = 2; i <= nParcelas; i++)
            {
                DateTime vencimento = registroFinanceiro.DataVencimentoPrimeiraParcela.AddMonths(i);
                vencimento = vencimento.AddDays((vencimento.Day - registroFinanceiro.DiaPagamento) * -1);
                Parcela parcela = new Parcela(i, vencimento, valorParcela);
                this.InserirParcela(parcela, registroFinanceiro.IdRegistro);
                listParcelas.Add(parcela);
            }

            return listParcelas;
        }

        public bool EditarParcela(Parcela parcela, int idRegistro)
        {
            return Vsf.DAL.ParcelaDAO.EditarParcela(parcela, idRegistro);
        }
    }
}
