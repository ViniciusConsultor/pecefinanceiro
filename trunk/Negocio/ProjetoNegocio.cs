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

        public List<Projeto> ObterProjetosDoAluno(int codigoPece)
        {
            return ProjetoDAO.ObterProjetosDoAluno(codigoPece);
        }

        public bool DeletarProjeto(String codigoProjeto)
        {
            return DAL.ProjetoDAO.DeletarProjeto(codigoProjeto);
        }

        public bool IncluirProjeto(Projeto novoProjeto)
        {
            return DAL.ProjetoDAO.IncluirProjeto(novoProjeto);
        }

        public bool AtualizarProjeto(Projeto atualizarProjeto)
        {
            return DAL.ProjetoDAO.AtualizarProjeto(atualizarProjeto);
        }
    }
}
