
using ClubeDaLeitura.ConsoleApp.ModuloEmprestimo;

namespace ClubeDaLeitura.ConsoleApp.Compartilhados
{
    public abstract class TelaBase
    {
        protected string nomeEntidade;
        protected RepositorioBase repositorio;

        protected TelaBase(string nomeEntidade, RepositorioBase repositorio)
        {
            this.nomeEntidade = nomeEntidade;
            this.repositorio = repositorio;
        }

        public virtual char ApresentarMenu()
        {
            Console.Clear();
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

        public virtual void CadastrarRegistro()
        {
            Console.Clear();
            ExibirCabecalho();
            Console.WriteLine("------------------------------------------");
            Console.WriteLine($"Cadastro de {nomeEntidade}");
            Console.WriteLine("------------------------------------------");

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

        public virtual void EditarRegistro()
        {
            ExibirCabecalho();
            Console.WriteLine("------------------------------------------");
            Console.WriteLine($"Edição de {nomeEntidade}");
            Console.WriteLine("------------------------------------------");

            Console.WriteLine();

            VisualizarRegistros(false);

            Console.Write("Digite o id do registro que deseja selecionar: ");
            int idSelecionado = Convert.ToInt32(Console.ReadLine());
            Console.Clear();

            Console.WriteLine();

            EntidadeBase RegistroAtualizado = ObterDados();

            repositorio.EditarRegistro(idSelecionado, RegistroAtualizado);

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

        public virtual void ExcluirRegistro()
        {
            ExibirCabecalho();

            Console.WriteLine($"Exclusão de {nomeEntidade}");

            Console.WriteLine();

            VisualizarRegistros(false);

            Console.Write("Digite o id do registro que deseja selecionar: ");
            int idSelecionado = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine();

            repositorio.ExcluirRegistro(idSelecionado);

            Console.Clear();
            Console.Write("------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n{nomeEntidade} excluído com sucesso!");
            Console.ResetColor();
            Console.Write("------------------------------------------");
            Console.WriteLine("\nDigite ENTER para continuar...");
            Console.Write("------------------------------------------");
            Console.ReadLine();
        }

        public abstract void VisualizarRegistros(bool exibirCabecalho);

        protected void ExibirCabecalho()
        {
            Console.Clear();
            Console.WriteLine("------------------------------------------");
            Console.WriteLine($"             Gestão de {nomeEntidade}s");
            Console.WriteLine("------------------------------------------");

        }

        protected abstract EntidadeBase ObterDados();
    }
}
