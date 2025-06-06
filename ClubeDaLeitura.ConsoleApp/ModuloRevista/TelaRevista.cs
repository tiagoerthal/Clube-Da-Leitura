

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
        public override void CadastrarRegistro()
        {
            ExibirCabecalho();
            Console.WriteLine("------------------------------------------");
            Console.WriteLine($"Cadastro de {nomeEntidade}");
            Console.WriteLine("------------------------------------------");

            Console.WriteLine();

            Revista novoRegistro = (Revista)ObterDados();

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
                Revista revistaRegistrado = (Revista)registros[i];

                if (revistaRegistrado == null)
                    continue;

                if (revistaRegistrado.Titulo == novoRegistro.Titulo || revistaRegistrado.NumeroDeEdicao == novoRegistro.NumeroDeEdicao)
                {
                    Console.WriteLine();

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Uma revista com este Título ou Edição já foram cadastrados!");
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
            ExibirCabecalho();
            Console.WriteLine("------------------------------------------");
            Console.WriteLine($"Edição de {nomeEntidade}");
            Console.WriteLine("------------------------------------------");

            Console.WriteLine();

            VisualizarRegistros(false);

            Console.Write("Digite o id do registro que deseja selecionar: ");
            int idSelecionado = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine();

            Revista registroAtualizado = (Revista)ObterDados();

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
                Revista revistaRegistrado = (Revista)registros[i];

                if (revistaRegistrado == null)
                    continue;

                if (
                    revistaRegistrado.id != idSelecionado &&
                    (revistaRegistrado.Titulo == registroAtualizado.Titulo ||
                    revistaRegistrado.NumeroDeEdicao == registroAtualizado.NumeroDeEdicao)
                )
                {
                    Console.WriteLine();

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Uma revista com este Título ou Edição já foram cadastrados!");
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
            Console.ReadLine();
        }

        public override void VisualizarRegistros(bool exibirCabecalho)
        {
            if (exibirCabecalho == true)
                ExibirCabecalho();

            Console.WriteLine("Visualização de Revistas");

            Console.WriteLine();

            Console.WriteLine(
                "{0, -10} | {1, -30} | {2, -20} | {3, -20} | {4, -20} | {5, -20}",
                "Id", "Título", "Edição", "Ano de Publicação", "Caixa", "Status"
            );

            EntidadeBase[] revistas = repositorio.SelecionarRegistros();

            for (int i = 0; i < revistas.Length; i++)
            {
                Revista r = (Revista)revistas[i];

                if (r == null)
                    continue;

                Console.WriteLine(
                 "{0, -10} | {1, -30} | {2, -20} | {3, -20} | {4, -20} | {5, -20}",
                    r.id, r.Titulo, r.NumeroDeEdicao, r.AnoDePublicacao, r.Caixa.Etiqueta, r.Status
                );
            }

            Console.ReadLine();
        }
        public void VisualizarCaixas()
        {
            Console.WriteLine();

            Console.WriteLine("Visualização de Caixas");

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
                    C.id, C.Etiqueta, C.Cor, C.DiasEmprestimo
                );
            }

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

            Console.Write("Digite o ano de publicação: ");
            DateTime anoDePublicao = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("------------------------------------------");

            VisualizarCaixas();

            Console.Write("Digite o id da Caixa: ");
            int idCaixa = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("------------------------------------------");

            Caixa CaixaSelecionado = (Caixa)repositorioCaixa.SelecionarRegistroPorId(idCaixa);

            Revista revista = new Revista(titulo, numeroDeEdicao, anoDePublicao, CaixaSelecionado);

            return revista;
        }
    }
}
