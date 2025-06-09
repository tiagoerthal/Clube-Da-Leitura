
using System;
using ClubeDaLeitura.ConsoleApp.Compartilhados;
using ClubeDaLeitura.ConsoleApp.ModuloAmigos;
using ClubeDaLeitura.ConsoleApp.ModuloCaixa;

namespace ClubeDaLeitura.ConsoleApp.ModuloRevista
{
    public class TelaRevista : TelaBase
    {
        private RepositorioCaixa repositorioCaixa;

        public TelaRevista(RepositorioRevista repositorio, RepositorioCaixa repositorioCaixa)
            : base("Revista", repositorio)
        {
            this.repositorioCaixa = repositorioCaixa;
        }

        public override char ApresentarMenu()
        {
            ExibirCabecalho();

            Console.WriteLine($"1 - Cadastro de {nomeEntidade}");
            Console.WriteLine($"2 - Visualizar {nomeEntidade}s");
            Console.WriteLine($"3 - Edição {nomeEntidade}s");
            Console.WriteLine($"4 - Excluir {nomeEntidade}");
            Console.WriteLine($"S - Sair");

            Console.WriteLine();

            Console.Write("Digite uma opção válida: ");
            char opcaoEscolhida = Console.ReadLine().ToUpper()[0];

            return opcaoEscolhida;
        }


        public override void VisualizarRegistros(bool exibirCabecalho)
        {
            if (exibirCabecalho == true)

            Console.WriteLine("------------------------------------------");
            Console.WriteLine($"Visualização de {nomeEntidade}");
            Console.WriteLine("------------------------------------------");
          
            Console.WriteLine();

            Console.WriteLine(
                "{0, -5} | {1, -20} | {2, -10} | {3, -20} | {4, -20} | {5, -20}",
                "Id", "Título", "Edição", "Ano de Publicação", "Caixa", "Status"
            );

            EntidadeBase[] revistas = repositorio.SelecionarRegistros();

            for (int i = 0; i < revistas.Length; i++)
            {
                Revista r = (Revista)revistas[i];

                if (r == null)
                    continue;

                Console.WriteLine(
                 "{0, -5} | {1, -20} | {2, -10} | {3, -20} | {4, -20} | {5, -20}",
                    r.Id, r.Titulo, r.NumeroDeEdicao, r.AnoDePublicacao, r.Caixa.Etiqueta, r.Status
                );
            }

            Console.WriteLine();
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("     Digite ENTER para continuar...");
            Console.Write("------------------------------------------");

            Console.ReadLine();
        }

        public void VisualizarCaixas()
        {
            Console.Clear();

            Console.WriteLine("------------------------------------------");
            Console.WriteLine("Visualização de Caixas");
            Console.WriteLine("------------------------------------------");

            Console.WriteLine();

            Console.WriteLine(
                "{0, -10} | {1, -20} | {2, -30} | {3, -15}",
                "Id", "Eiqueta", "Cor", "Dias de empréstimo"
            );

            EntidadeBase[] caixa = repositorioCaixa.SelecionarRegistros();

            for (int i = 0; i < caixa.Length; i++)
            {
                Caixa C = (Caixa)caixa[i];

                if (C == null)
                    continue;

                Console.WriteLine(
                   "{0, -10} | {1, -20} | {2, -30} | {3, -15}",
                    C.Id, C.Etiqueta, C.Cor, C.DiasEmprestimo
                );
            }

            Console.WriteLine();
            Console.Write("------------------------------------------");
            Console.WriteLine("\nDigite ENTER para continuar...");
            Console.Write("------------------------------------------");

            Console.ReadLine();
        }

        protected override Revista ObterDados()
        {

            Console.Write("Digite o título: ");
            string titulo = Console.ReadLine();
            Console.WriteLine("------------------------------------------");

            Console.Write("Digite o numero da Edição: ");
            int numeroDeEdicao = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("------------------------------------------");

            Console.Write("Digite a data de publicação: ");
            DateTime anoDePublicao = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("------------------------------------------");

            VisualizarCaixas();

            Console.Write("Digite o id da Caixa: ");
            int idCaixa = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("------------------------------------------"); 
            Console.Write("\nDigite ENTER para continuar...");
            Console.WriteLine("------------------------------------------");

            Caixa CaixaSelecionado = (Caixa)repositorioCaixa.SelecionarRegistroPorId(idCaixa);

            Revista revista = new Revista(titulo, numeroDeEdicao, anoDePublicao, CaixaSelecionado);

            return revista;
        }
    }
}
