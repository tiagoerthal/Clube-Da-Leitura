
using System;
using ClubeDaLeitura.ConsoleApp.Compartilhados;
using ClubeDaLeitura.ConsoleApp.ModuloAmigos;
using ClubeDaLeitura.ConsoleApp.ModuloRevista;

namespace ClubeDaLeitura.ConsoleApp.ModuloEmprestimo
{
    public class TelaEmprestimo : TelaBase
    {
        private RepositorioEmprestimo repositorioEmprestimo;
        private RepositorioAmigo repositorioAmigo;
        private RepositorioRevista repositorioRevista;

        public TelaEmprestimo(RepositorioEmprestimo repositorioEmprestimo, RepositorioAmigo repositorioAmigo,
            RepositorioRevista repositorioRevista) : base("Emprestimo", repositorioEmprestimo)
        {
            this.repositorioEmprestimo = repositorioEmprestimo;
            this.repositorioAmigo = repositorioAmigo;
            this.repositorioRevista = repositorioRevista;
        }
        public override char ApresentarMenu()
        {
            ExibirCabecalho();

            Console.WriteLine($"1 - Cadastro de {nomeEntidade}");
            Console.WriteLine($"2 - Visualizar {nomeEntidade}s");
            Console.WriteLine($"3 - Excluir {nomeEntidade}");
            Console.WriteLine($"4 - Devolução ");
            Console.WriteLine($"S - Sair");

            Console.WriteLine();

            Console.Write("Digite uma opção válida: ");
            char opcaoEscolhida = Console.ReadLine().ToUpper()[0];

            return opcaoEscolhida;
        }
        public void VisualizarAmigo()
        {
            Console.WriteLine();

            Console.WriteLine("Visualização de Amigos");

            Console.WriteLine();

            Console.WriteLine(
                "{0, -10} | {1, -20} | {2, -30} | {3, -15}",
                "Id", "Nome", "Nome do responsável", "Telefone"
            );

            EntidadeBase[] amigo = repositorioAmigo.SelecionarRegistros();

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
        public void VisualizarRevistas()
        {
            Console.WriteLine();

            Console.WriteLine("Visualização de Revistas");

            Console.WriteLine();

            Console.WriteLine(
                "{0, -10} | {1, -20} | {2, -30} | {3, -15}| {4, -15}",
                "Id", "titulo", "Nome do responsável", "Telefone", "status"
            );

            EntidadeBase[] revista = repositorioRevista.SelecionarRegistros();

            for (int i = 0; i < revista.Length; i++)
            {
                Revista R = (Revista)revista[i];

                if (R == null)
                    continue;

                Console.WriteLine(
                   "{0, -10} | {1, -20} | {2, -30} | {3, -15}| {4, -15}",
                    R.id, R.Titulo, R.NumeroDeEdicao, R.AnoDePublicacao, R.Status
                );
            }

            Console.ReadLine();
        }
        public override void VisualizarRegistros(bool exibirCabecalho)
        {
            if (exibirCabecalho == true)
                ExibirCabecalho();

            Console.WriteLine("Visualização de Emprestimos");

            Console.WriteLine();

            Console.WriteLine(
                "{0, -10} | {1, -20} | {2, -30} | {3, -15} | {3, -15}",
                "Id", "Amigo", "Revista", "data do empréstimo", "data da devolução"
            );

            EntidadeBase[] revista = repositorioEmprestimo.SelecionarRegistros();

            for (int i = 0; i < revista.Length; i++)
            {
                Emprestimo R = (Emprestimo)revista[i];

                if (R == null)
                    continue;

                Console.WriteLine(
                   "{0, -10} | {1, -20} | {2, -30} | {3, -15} | {3, -15}",
                    R.id, R.Amigo.Nome, R.Revista.Titulo, R.DataEmprestimo.ToShortDateString(), R.DataDevolucao.ToShortDateString()
                );
            }

            Console.ReadLine();
        }
        public void CadastrarDevolucao()
        {
            VisualizarRegistros(false);

            Console.Write("Digite o id do emprestimo: ");
            int IdEmprestimo = Convert.ToInt32(Console.ReadLine());
            Emprestimo emprestimoSelecionado = (Emprestimo)repositorioEmprestimo.SelecionarRegistroPorId(IdEmprestimo);

            emprestimoSelecionado.DataDevolucao = DateTime.Now;

            emprestimoSelecionado.Revista.Status = "Disponível";

            Console.WriteLine($"Devolução realizada com sucesso!");
            Console.ReadLine();

        }
        public override void CadastrarRegistro()
        {
            Console.Clear(); 
            ExibirCabecalho();
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine($"Cadastro de {nomeEntidade}");
            Console.ResetColor();

            Console.WriteLine();

            Emprestimo novoRegistro = ObterDados();

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

            bool temEmprestimos = repositorioEmprestimo.ExisteEmprestimosVinculadas(novoRegistro.Amigo.id);

            if (temEmprestimos)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nEste amigo possui empréstimos vinculados e não pode adquirir outro.");
                Console.ResetColor();
                Console.ReadLine();
                return;
            }

            repositorio.CadastrarRegistro(novoRegistro);
            Console.Clear();
            Console.Write("------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n{nomeEntidade} cadastrado com sucesso!");
            Console.ResetColor();
            Console.Write("------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nDigite ENTER para continuar...");
            Console.ResetColor();
            Console.Write("------------------------------------------");
            Console.ReadLine();
        }

        protected override Emprestimo ObterDados()
        {
            VisualizarAmigo();
            Console.Write("Digite o id do amigo: ");
            int IdAmigo = Convert.ToInt32(Console.ReadLine());
            Amigo amigoSelecionado = (Amigo)repositorioAmigo.SelecionarRegistroPorId(IdAmigo);

            VisualizarRevistas();
            Console.Write("Digite o id da revista: ");
            int IdRevista = Convert.ToInt32(Console.ReadLine());
            Revista revistaSelecionado = (Revista)repositorioRevista.SelecionarRegistroPorId(IdRevista);

            Console.WriteLine("Digite a data da reserva");
            DateTime dataEmprestimo = Convert.ToDateTime(Console.ReadLine());

            Emprestimo emprestimo = new Emprestimo(amigoSelecionado, revistaSelecionado, dataEmprestimo);

            return emprestimo;
        }
    }
}
