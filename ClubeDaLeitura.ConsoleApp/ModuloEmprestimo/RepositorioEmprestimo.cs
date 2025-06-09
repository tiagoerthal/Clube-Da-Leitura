﻿
using System;
using ClubeDaLeitura.ConsoleApp.Compartilhados;
using ClubeDaLeitura.ConsoleApp.ModuloRevista;

namespace ClubeDaLeitura.ConsoleApp.ModuloEmprestimo
{
    public class RepositorioEmprestimo : RepositorioBase
    {
        public Emprestimo[] SelecionarEmprestimosAtivos()
        {
            int contadorEmprestimosAtivos = 0;

            for (int i = 0; i < registros.Length; i++)
            {
               Emprestimo emprestimoAtual = (Emprestimo)registros[i];

                if (emprestimoAtual == null)
                    continue;

                if ( emprestimoAtual.Status == "Aberto" || emprestimoAtual.Status == "Atrasado")
                    contadorEmprestimosAtivos++;
            }

            Emprestimo[] emprestimosAtivos = new Emprestimo[contadorEmprestimosAtivos];

            int contadorAuxiliar = 0;

            for (int i = 0; i < registros.Length; i++)
            {
                Emprestimo emprestimoAtual = (Emprestimo)registros[i];

                if (emprestimoAtual == null)
                    continue;

                if (emprestimoAtual.Status == "Aberto" || emprestimoAtual.Status == "Atrasado")
                {
                    emprestimosAtivos[contadorAuxiliar++] = (Emprestimo)registros[i];
                }
            }

            return emprestimosAtivos;
        }
    }
}
