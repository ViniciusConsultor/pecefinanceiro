using System;
using System.Collections.Generic;
using System.Text;
using Vsf.DAL;
using Vsf.Modelo;

namespace Vsf.Negocio
{
    public class RegistroFinanceiroNegocio
    {

        public bool IncluirRegistroFinanceiro(RegistroFinanceiro registroFinanceiro, AlunoProjeto alunoProjeto)
        {
            return RegistroFinanceiroDAO.IncluirRegistroFinanceiro(registroFinanceiro, alunoProjeto);
        }

        public List<RegistroFinanceiro> ObterTodosRegistros()
        {
            return RegistroFinanceiroDAO.ObterTodosRegistros();
        }

        public void RemoverRegistroFinanceiro(int IdAlunoProjeto)
        {
            throw new NotImplementedException();
        }

        public RegistroFinanceiro ObterRegistroPorId(int idRegistroFinanceiro)
        {
            return RegistroFinanceiroDAO.ObterRegistroPorId(idRegistroFinanceiro);
        }
    }
}
