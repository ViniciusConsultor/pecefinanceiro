using System;
using System.Collections.Generic;
using System.Text;
using Vsf.Modelo;
using Vsf.DAL;

namespace Vsf.Negocio
{
    public class ProjetoNegocio
    {
        public List<Projeto> ObterTodosProjetos()
        {
            return ProjetoDAO.ObterTodosProjetos();
        }

        public Projeto ObterProjetoPorCodigo(string codigoProjeto)
        {
            return ProjetoDAO.ObterProjetoPorCodigo(codigoProjeto);
        }
    }
}
