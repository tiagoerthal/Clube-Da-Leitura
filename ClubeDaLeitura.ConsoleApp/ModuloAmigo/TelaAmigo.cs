
using System;
using ClubeDaLeitura.ConsoleApp.Compartilhados;
using ClubeDaLeitura.ConsoleApp.ModuloEmprestimo;

namespace ClubeDaLeitura.ConsoleApp.ModuloAmigos
{
    //VisualizarEmprestimos()
    public class TelaAmigo : TelaBase
    {
        private RepositorioEmprestimo repositorioEmprestimo;
        public TelaAmigo(RepositorioAmigo repositorio, RepositorioEmprestimo repositorioEmprestimo)
             :   base("Amigo", repositorio)
        {
            this.repositorioEmprestimo = repositorioEmprestimo;
        }
        public override void CadastrarRegistro()
        {
            Console.Clear();

            Console.WriteLine("------------------------------------------");
            Console.WriteLine($"Cadastro de {nomeEntidade}");
            Console.WriteLine("------------------------------------------");

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

            Console.Clear();
            Console.Write("------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n{nomeEntidade} cadastrado com sucesso!");
            Console.ResetColor();
            Console.Write("------------------------------------------");
            Console.WriteLine("\nDigite ENTER para continuar...");
            Console.Write("------------------------------------------");
            Console.ReadLine();
        }
        public override void EditarRegistro()
        {
            Console.Clear();

            ExibirCabecalho();

            Console.WriteLine("------------------------------------------");
            Console.WriteLine($"Edição de {nomeEntidade}");
            Console.WriteLine("------------------------------------------");

            Console.WriteLine();

            VisualizarRegistros(false);

            Console.Clear();
            Console.WriteLine();

            Console.Write("Digite o id do registro que deseja selecionar: ");
            int idSelecionado = Convert.ToInt32(Console.ReadLine());

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
                    amigoRegistrado.Id != idSelecionado &&
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

            Console.Clear();
            Console.Write("------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n{nomeEntidade} editado com sucesso!");
            Console.ResetColor();
            Console.Write("------------------------------------------");
            Console.WriteLine("\nDigite ENTER para continuar...");
            Console.Write("------------------------------------------");
            Console.WriteLine();

            Console.ReadLine();
        }
        public override void ExcluirRegistro()
        {
            ExibirCabecalho();
            Console.WriteLine("------------------------------------------");
            Console.WriteLine($"Exclusão de {nomeEntidade}");
            Console.WriteLine("------------------------------------------");

            Console.WriteLine();

             VisualizarRegistros(false);

            Console.Write("\nDigite o id do amigo que deseja excluir: ");
            int idSelecionado = Convert.ToInt32(Console.ReadLine());

            bool temEmprestimos = repositorioEmprestimo.ExisteEmprestimosVinculadas(idSelecionado);

            if (temEmprestimos)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nEste amigo possui empréstimos vinculados e não pode ser excluído.");
                Console.ResetColor();
                Console.ReadLine();
                return;
            }

            bool conseguiuExcluir = repositorio.ExcluirRegistro(idSelecionado);

            if (conseguiuExcluir)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n{nomeEntidade} excluído com sucesso!");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nErro ao tentar excluir o {nomeEntidade}.");
            }

            Console.ResetColor();
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
                    A.Id, A.Nome, A.Responsavel, A.Telefone
                );
            }

            Console.WriteLine();
            Console.Write("------------------------------------------");
            Console.WriteLine("\nDigite ENTER para continuar...");
            Console.Write("------------------------------------------");
            Console.ReadLine();
        }      

        protected override Amigo ObterDados()
        {
            Console.Clear();

            Console.Write("Digite o nome do Amigo: ");
            string nome = Console.ReadLine();
            Console.WriteLine("------------------------------------------");

            Console.Write("Digite o nome do responsável pelo Amigo: ");
            string nomeResponsavel = Console.ReadLine();
            Console.WriteLine("------------------------------------------");

            Console.Write("Digite o telefone do Amigo: ");
            string telefone = Console.ReadLine();
            Console.WriteLine("------------------------------------------");

            Amigo amigo = new Amigo(nome, nomeResponsavel, telefone);

            return amigo;
        }
    }
}
