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

        public bool AtualizarRegistroFinanceiro(RegistroFinanceiro registroFinanceiro)
        {
            return RegistroFinanceiroDAO.AtualizarRegistroFinanceiro(registroFinanceiro);
        }


        /// <summary>
        /// Verifica se já foi gerado um registro financeiro para um par aluno-projeto (Matricula) 
        /// </summary>
        /// <param name="p"></param>
        /// <param name="p_2"></param>
        /// <returns></returns>
        public bool ExisteRegistroFinanceiroParaMatricula(AlunoProjeto matricula)
        {
            if (ObterRegistroPorId(matricula.Id).AlunoProjeto != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
