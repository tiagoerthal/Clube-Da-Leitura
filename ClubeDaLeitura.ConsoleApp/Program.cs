﻿using ClubeDaLeitura.ConsoleApp.Compartilhados;
using ClubeDaLeitura.ConsoleApp.ModuloEmprestimo;

namespace ClubeDaLeitura.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            TelaPrincipal telaPrincipal = new TelaPrincipal();

            while (true)
            {
                telaPrincipal.ApresentarMenuPrincipal();

                TelaBase telaEscolhida = telaPrincipal.ObterTela();

                if (telaEscolhida == null)
                    break;

                char opcaoEscolhida = telaEscolhida.ApresentarMenu();

                if (opcaoEscolhida == 'S' || opcaoEscolhida == 's')
                    break;

                if (telaEscolhida is TelaEmprestimo)
                {
                    TelaEmprestimo telaEmprestimo = (TelaEmprestimo)telaEscolhida;
                    switch (opcaoEscolhida)
                    {
                        case '1':
                            telaEmprestimo.CadastrarEmprestimo();
                            break;

                        case '2':
                            telaEmprestimo.VisualizarRegistros(true);
                            break;

                        case '3':
                            telaEmprestimo.CadastrarDevolucao();
                            break;
                    }


                }

                else switch (opcaoEscolhida)
                    {
                        case '1':
                            telaEscolhida.CadastrarRegistro();
                            break;

                        case '2':
                            telaEscolhida.VisualizarRegistros(true);
                            break;

                        case '3':
                            telaEscolhida.EditarRegistro();
                            break;

                        case '4':
                            telaEscolhida.ExcluirRegistro();
                            break;
                    }
            }
        }
    }
}
