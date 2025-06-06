
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
            ExibirCabecalho();

            Console.WriteLine($"Cadastro de {nomeEntidade}");

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

            Console.WriteLine($"\n{nomeEntidade} cadastrado com sucesso!");
            Console.ReadLine();
        }

        public override void EditarRegistro()
        {
            ExibirCabecalho(); 

            Console.WriteLine($"Edição de {nomeEntidade}");

            Console.WriteLine();

            VisualizarRegistros(false);

            Console.Write("Digite o id do registro que deseja selecionar: ");
            int idSelecionado = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine();

            Caixa registroAtualizado = (Caixa)ObterDados();

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
                Caixa caixaRegistrado = (Caixa)registros[i];

                if (caixaRegistrado == null)
                    continue;

                if (
                    caixaRegistrado.id != idSelecionado &&
                    (caixaRegistrado.Etiqueta == registroAtualizado.Etiqueta)
                   )
                {
                    Console.WriteLine();

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Uma etiqueta com este nome já foi cadastrada!");
                    Console.ResetColor();

                    Console.Write("\nDigite ENTER para continuar...");
                    Console.ReadLine();

                    EditarRegistro();

                    return;
                }
            }

            repositorio.EditarRegistro(idSelecionado, registroAtualizado);

            Console.WriteLine($"\n{nomeEntidade} editado com sucesso!");
            Console.ReadLine();
        }
        public override void ExcluirRegistro()
        {
            ExibirCabecalho();

            Console.WriteLine($"Exclusão de {nomeEntidade}");
            Console.WriteLine();

            VisualizarRegistros(false);

            Console.Write("\nDigite o id da caixa que deseja excluir: ");
            int idSelecionado = Convert.ToInt32(Console.ReadLine());

            bool temVinculo = repositorioRevista.ExistemRevistasVinculadas(idSelecionado);

            if (temVinculo)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nEsta Revista possui vinculos e não pode ser excluído.");
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
            if (exibirCabecalho == true)
                ExibirCabecalho();

            Console.WriteLine("Visualização de Caixas");

            Console.WriteLine();

            Console.WriteLine(
                "{0, -10} | {1, -20} | {2, -30} | {3, -15}",
                "Id", "Eiqueta", "Cor", "Dias de empréstimo"
            );

            EntidadeBase[] caixa = repositorio.SelecionarRegistros();

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
        protected override EntidadeBase ObterDados()
        {
            Console.Write("Digite a etiqueta da caixa: ");
            string etiqueta = Console.ReadLine();

            Console.Write("Digite a cor da caixa: ");
            string cor = Console.ReadLine();

            Console.Write("Dias de Empréstimo (opcional): ");
            bool conseguiuConverter = int.TryParse(Console.ReadLine(), out int diasEmprestimo);

            Caixa caixa;

            if (conseguiuConverter)
                caixa = new Caixa(etiqueta, cor, diasEmprestimo);
            else
                caixa = new Caixa(etiqueta, cor);

            return caixa;
        }
    }
}
