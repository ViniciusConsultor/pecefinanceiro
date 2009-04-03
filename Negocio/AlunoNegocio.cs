using System;
using System.Collections.Generic;
using System.Text;
using Vsf.DAL;

namespace Vsf.Negocio
{
    public class AlunoNegocio
    {
        public Vsf.Modelo.AlunoProjeto ObterRelacionamentoAlunoProjeto(int codigoAluno, string codigoProjeto)
        {
            return AlunoDAO.ObterRelacionamentoAlunoProjeto(codigoAluno, codigoProjeto);
        }
    }
}
