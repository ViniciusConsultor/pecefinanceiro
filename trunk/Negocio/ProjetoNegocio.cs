﻿using System;
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
        /// <summary>
        /// Retorna todos os projetos que o Aluno pode se matricular (e não está já matriculado)
        /// </summary>
        /// <param name="codigoPece"></param>
        /// <returns></returns>
        public List<Projeto> ObterProjetosDisponiveisAoAluno(int codigoPece)
        {
            return ProjetoDAO.ObterProjetosDisponiveisAoAluno(codigoPece);
        }
    }
}
