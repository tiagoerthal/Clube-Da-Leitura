
using System;
using ClubeDaLeitura.ConsoleApp.Compartilhados;
using ClubeDaLeitura.ConsoleApp.ModuloAmigos;
using ClubeDaLeitura.ConsoleApp.ModuloEmprestimo;
using ClubeDaLeitura.ConsoleApp.ModuloRevista;

namespace ClubeDaLeitura.ConsoleApp.ModuloCaixa
{
    public class TelaCaixa : TelaBase
    {
        private RepositorioRevista repositorioRevista;
        
        public TelaCaixa(RepositorioCaixa repositorio,RepositorioRevista repositorioRevista) : base("Caixa", repositorio)
        {
            this.repositorioRevista = repositorioRevista;
        }

        public override void CadastrarRegistro()
        {
            Console.Clear();

            Console.WriteLine("------------------------------------------");
            Console.WriteLine($"Cadastro de {nomeEntidade}");
            Console.WriteLine("------------------------------------------");

            Console.WriteLine();

            Caixa novoRegistro = (Caixa)ObterDados();

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
                Caixa caixaRegistrado = (Caixa)registros[i];

                if (caixaRegistrado == null)
                    continue;

                if (caixaRegistrado.Etiqueta == novoRegistro.Etiqueta )
                {
                    Console.WriteLine();

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Uma etiqueta com este nome já foi cadastrada!");
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

        //public override void EditarRegistro()
        //{
        //    ExibirCabecalho();
        //    Console.WriteLine("------------------------------------------");
        //    Console.WriteLine($"Edição de {nomeEntidade}");
        //    Console.WriteLine("------------------------------------------");

        //    Console.WriteLine();

        //    VisualizarRegistros(false);

        //    Console.Write("Digite o id do registro que deseja selecionar: ");
        //    int idSelecionado = Convert.ToInt32(Console.ReadLine());

        //    Console.WriteLine();

        //    Caixa registroAtualizado = (Caixa)ObterDados();

        //    string erros = registroAtualizado.Validar();

        //    if (erros.Length > 0)
        //    {
        //        Console.WriteLine();

        //        Console.ForegroundColor = ConsoleColor.Red;
        //        Console.WriteLine(erros);
        //        Console.ResetColor();

        //        Console.Write("\nDigite ENTER para continuar...");
        //        Console.ReadLine();

        //        EditarRegistro();

        //        return;
        //    }

        //    EntidadeBase[] registros = repositorio.SelecionarRegistros();

        //    for (int i = 0; i < registros.Length; i++)
        //    {
        //        Caixa caixaRegistrado = (Caixa)registros[i];

        //        if (caixaRegistrado == null)
        //            continue;

        //        if (
        //            caixaRegistrado.Id != idSelecionado &&
        //            (caixaRegistrado.Etiqueta == registroAtualizado.Etiqueta)
        //           )
        //        {
        //            Console.WriteLine();

        //            Console.ForegroundColor = ConsoleColor.Red;
        //            Console.WriteLine("Uma etiqueta com este nome já foi cadastrada!");
        //            Console.ResetColor();

        //            Console.Write("\nDigite ENTER para continuar...");
        //            Console.ReadLine();

        //            EditarRegistro();

        //            return;
        //        }
        //    }

        //    repositorio.EditarRegistro(idSelecionado, registroAtualizado);

        //    Console.Clear();
        //    Console.Write("------------------------------------------");
        //    Console.ForegroundColor = ConsoleColor.Green;
        //    Console.WriteLine($"\n{nomeEntidade} editado com sucesso!");
        //    Console.ResetColor();
        //    Console.Write("------------------------------------------");
        //    Console.WriteLine("\nDigite ENTER para continuar...");
        //    Console.Write("------------------------------------------");
        //    Console.ReadLine();
        //}
       

        public override void VisualizarRegistros(bool exibirCabecalho)
        {
            if (exibirCabecalho == true)
                ExibirCabecalho();

            Console.WriteLine("------------------------------------------");
            Console.WriteLine("Visualização de Caixas");
            Console.WriteLine("------------------------------------------");

            Console.WriteLine();

            Console.WriteLine(
                "{0, -10} | {1, -20} | {2, -15} | {3, -15}",
                "Id", "Eiqueta", "Cor", "Dias de empréstimo"
            );

            EntidadeBase[] caixa = repositorio.SelecionarRegistros();

            for (int i = 0; i < caixa.Length; i++)
            {
                Caixa C = (Caixa)caixa[i];

                if (C == null)
                    continue;

                Console.WriteLine(
                   "{0, -10} | {1, -20} | {2, -15} | {3, -15}",
                    C.Id, C.Etiqueta, C.Cor, C.DiasEmprestimo
                );
            }

            Console.WriteLine();
            Console.Write("------------------------------------------");
            Console.WriteLine("\nDigite ENTER para continuar...");
            Console.Write("------------------------------------------");
 
            Console.ReadLine();
            Console.Clear();
        }
        protected override EntidadeBase ObterDados()
        {
            Console.Write("Digite a etiqueta da caixa: ");
            string etiqueta = Console.ReadLine();
            Console.WriteLine("------------------------------------------");

            Console.Write("Digite a cor da caixa: ");
            string cor = Console.ReadLine();
            Console.WriteLine("------------------------------------------");

            Console.Write("Dias de Empréstimo (opcional): ");
            bool conseguiuConverter = int.TryParse(Console.ReadLine(), out int diasEmprestimo);
            Console.WriteLine("------------------------------------------");

            Caixa caixa;

            if (conseguiuConverter)
                caixa = new Caixa(etiqueta, cor, diasEmprestimo);
            else
                caixa = new Caixa(etiqueta, cor);

            return caixa;
        }
    }
}
