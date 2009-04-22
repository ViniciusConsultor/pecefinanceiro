using System;
using System.Collections.Generic;
using System.Text;
using Vsf.DAL;
using Vsf.Modelo;

namespace Vsf.Negocio
{
    public class RelatorioNegocio
    {
        public RelatorioNegocio()
        {
        }

        public Relatorio ObterRelatorioInadimplentes()
        {
            return RelatorioDAO.ObterRelatorioInadimplentes();
        }
        public Relatorio ObterRelatorioArrecadacaoPrevistaMes(int mes, int ano)
        {
            return RelatorioDAO.ObterRelatorioArrecadacaoMes(mes, ano, enumTipoRelatorio.Previsto);
        }
        public Relatorio ObterRelatorioArrecadacaoMes(int mes, int ano)
        {
            return RelatorioDAO.ObterRelatorioArrecadacaoMes(mes, ano, enumTipoRelatorio.Efetivo);
        }
        public Relatorio ObterRelatorioArrecadacaoDevidaMes(int mes, int ano)
        {
            return RelatorioDAO.ObterRelatorioArrecadacaoMes(mes, ano, enumTipoRelatorio.Devido);
        }

        public List<AlunoParcela> ObterAlunosInadimplentes()
        {
            return RelatorioDAO.ObterAlunosInadimplentes();
        }
        public List<Projeto> ObterArrecadacaoMes(int mes,int ano)
        {
            return RelatorioDAO.ObterArrecadacaoMes(mes, ano, enumTipoRelatorio.Efetivo);
        }
        public List<Projeto> ObterArrecadacaoPrevistaMes(int mes,int ano)
        {
            return RelatorioDAO.ObterArrecadacaoMes(mes, ano, enumTipoRelatorio.Previsto);
        }
        public List<Projeto> ObterArrecadacaoDevidaMes(int mes, int ano)
        {
            return RelatorioDAO.ObterArrecadacaoMes(mes, ano, enumTipoRelatorio.Devido);
        }

    }
}
