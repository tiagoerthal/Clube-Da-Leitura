
namespace ClubeDaLeitura.ConsoleApp.Compartilhados
{
    public class TelaBase
    {
        protected string nomeEntidade;
        protected RepositorioBase repositorio;

        public char ApresentarMenu()
        {
            ExibirCabecalho();

            Console.WriteLine($"1 - Cadastro de {nomeEntidade}");
            Console.WriteLine($"2 - Visualizar {nomeEntidade}s");
            Console.WriteLine($"3 - Editar {nomeEntidade}");
            Console.WriteLine($"4 - Excluir {nomeEntidade}");
            Console.WriteLine($"S - Sair");

            Console.WriteLine();

            Console.Write("Digite uma opção válida: ");
            char opcaoEscolhida = Console.ReadLine().ToUpper()[0];

            return opcaoEscolhida;
        }
        public void CadastrarRegistro()
        {
            ExibirCabecalho();

            Console.WriteLine($"Cadastro de {nomeEntidade}");

            Console.WriteLine();

            EntidadeBase novoRegistro = ObterDados();

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

            repositorio.CadastrarRegistro(novoRegistro);

            Console.WriteLine($"\n{nomeEntidade} cadastrado com sucesso!");
            Console.ReadLine();
        }
        protected void ExibirCabecalho()
        {
            Console.Clear();
            Console.WriteLine($"Gestão de {nomeEntidade}s");
            Console.WriteLine();
        }
        protected abstract EntidadeBase ObterDados();

    }
}
