﻿
using System;
using ClubeDaLeitura.ConsoleApp.Compartilhados;

namespace ClubeDaLeitura.ConsoleApp.ModuloAmigos
{
    //VisualizarEmprestimos()
    public class TelaAmigo : TelaBase
    {

        public TelaAmigo(RepositorioAmigo repositorio) : base("Amigo", repositorio)
        {
        }

        public override void CadastrarRegistro()
        {
            ExibirCabecalho();

            Console.WriteLine($"Cadastro de {nomeEntidade}");

            Console.WriteLine();

            Amigo novoRegistro = (Amigo)ObterDados();

            string erros = novoRegistro.Validar();

            if (erros.Length > 0)
            {
                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(erros);
                Console.ResetColor();

                Console.Write("\nDigite ENTER para continuar...");
                Console.ReadLine();

                CadastrarRegistro();

                return;
            }

            EntidadeBase[] registros = repositorio.SelecionarRegistros();

            for (int i = 0; i < registros.Length; i++)
            {
                Amigo amigoRegistrado = (Amigo)registros[i];

                if (amigoRegistrado == null)
                    continue;

                if (amigoRegistrado.Nome == novoRegistro.Nome || amigoRegistrado.Telefone == novoRegistro.Telefone)
                {
                    Console.WriteLine();

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Um amigo com este nome ou telefone já foi cadastrado!");
                    Console.ResetColor();

                    Console.Write("\nDigite ENTER para continuar...");
                    Console.ReadLine();

                    CadastrarRegistro();
                    return;
                }
            }

            repositorio.CadastrarRegistro(novoRegistro);

            Console.WriteLine($"\n{nomeEntidade} cadastrado com sucesso!");
            Console.ReadLine();
        }
        public override void EditarRegistro()
        {
            ExibirCabecalho(); 

            Console.WriteLine($"Edição de {nomeEntidade}");

            Console.WriteLine();

            VisualizarRegistros(false);

            Console.Write("Digite o id do registro que deseja selecionar: ");
            int idSelecionado = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine();

            Amigo registroAtualizado = (Amigo)ObterDados();

            string erros = registroAtualizado.Validar();

            if (erros.Length > 0)
            {
                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(erros);
                Console.ResetColor();

                Console.Write("\nDigite ENTER para continuar...");
                Console.ReadLine();

                EditarRegistro();

                return;
            }

            EntidadeBase[] registros = repositorio.SelecionarRegistros();

            for (int i = 0; i < registros.Length; i++)
            {
                Amigo amigoRegistrado = (Amigo)registros[i];

                if (amigoRegistrado == null)
                    continue;

                if (
                    amigoRegistrado.id != idSelecionado &&
                    (amigoRegistrado.Nome == registroAtualizado.Nome ||
                    amigoRegistrado.Telefone == registroAtualizado.Telefone)
                )
                {
                    Console.WriteLine();

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Um amigo com este nome ou telefone já foi cadastrado!");
                    Console.ResetColor();

                    Console.Write("\nDigite ENTER para continuar...");
                    Console.ReadLine();

                    EditarRegistro();

                    return;
                }
            }

            repositorio.EditarRegistro(idSelecionado, registroAtualizado);

            Console.WriteLine($"\n{nomeEntidade} editado com sucesso!");
            Console.ReadLine();
        }

        public override void VisualizarRegistros(bool exibirCabecalho)
        {
            Console.Clear();

            if (exibirCabecalho == true)
                ExibirCabecalho();

            Console.WriteLine("Visualização de Amigos");

            Console.WriteLine();

            Console.WriteLine(
                "{0, -10} | {1, -20} | {2, -30} | {3, -15}",
                "Id", "Nome", "Nome do responsável", "Telefone"
            );

            EntidadeBase[] amigo = repositorio.SelecionarRegistros();

            for (int i = 0; i < amigo.Length; i++)
            {
                Amigo A = (Amigo)amigo[i];

                if (A == null)
                    continue;

                Console.WriteLine(
                   "{0, -10} | {1, -20} | {2, -30} | {3, -15}",
                    A.id, A.Nome, A.Responsavel, A.Telefone
                );
            }

            Console.ReadLine();
        }

        protected override Amigo ObterDados()
        {
            Console.Clear();

            Console.Write("Digite o nome do Amigo: ");
            string nome = Console.ReadLine();

            Console.Write("Digite o nome do responsável pelo Amigo: ");
            string nomeResponsavel = Console.ReadLine();

            Console.Write("Digite o telefone do Amigo: ");
            string telefone = Console.ReadLine();

            Amigo amigo = new Amigo(nome, nomeResponsavel, telefone);

            return amigo;
        }
    }
}
