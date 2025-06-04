
using ClubeDaLeitura.ConsoleApp.Compartilhados;

namespace ClubeDaLeitura.ConsoleApp.ModuloAmigos
{
    //VisualizarEmprestimos()
    public class TelaAmigo : TelaBase
    {

        public TelaAmigo(RepositorioAmigo repositorio) : base("Amigo", repositorio)
        {
        }

        public override void VisualizarRegistros(bool exibirCabecalho)
        {
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
