
namespace ClubeDaLeitura.ConsoleApp.Compartilhados
{
    public class TelaBase
    {
        protected string nomeEntidade;
        protected RepositorioBase repositorio;

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
