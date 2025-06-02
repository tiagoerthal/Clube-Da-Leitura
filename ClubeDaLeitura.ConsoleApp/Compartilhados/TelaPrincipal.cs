

namespace ClubeDaLeitura.ConsoleApp.Compartilhados
{
    public class TelaPrincipal
    {
        private char opcaoEscolhida;
        public static char ApresentarMenuPrincipal()
        {
            Console.Clear();

            Console.WriteLine("----------------------------------");
            Console.WriteLine("|        Clube Da Leitura        |");
            Console.WriteLine("----------------------------------");

            Console.WriteLine();

            Console.WriteLine("1 - Controle de Amigos");
            Console.WriteLine("2 - Controle de Caixas");
            Console.WriteLine("3 - Controle de Revistas");
            Console.WriteLine("4 - Controle de Empréstimos");
            Console.WriteLine("S - Sair");

            Console.WriteLine();

            Console.Write("Escolha uma das opções: ");
            char opcaoEscolhida = Console.ReadLine()[0];

            return opcaoEscolhida;
        }
        public TelaBase ObterTela()
        {
            //if (opcaoEscolhida == '1')
                //return telaAmigo;

            //else if (opcaoEscolhida == '2')
             //   return telaChamado;         trocar

           // else if (opcaoEscolhida == '3')
              //  return telaFabricante;         trocar

            return null;
        }
    }
}
