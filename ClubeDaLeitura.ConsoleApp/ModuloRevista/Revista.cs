

using ClubeDaLeitura.ConsoleApp.Compartilhados;
using ClubeDaLeitura.ConsoleApp.ModuloCaixa;

namespace ClubeDaLeitura.ConsoleApp.ModuloRevista
{
    public class Revista : EntidadeBase
    {
        public string Titulo { get; set; }
        public int NumeroDeEdicao { get; set; }
        public DateTime AnoDePublicacao { get; set; }
        public Caixa Caixa { get; set; }

        public string Status { get; set; }

        public Revista(string titulo, int numeroDeEdicao, DateTime anoDePublicacao, Caixa caixa)
        {
           Titulo = titulo;
           NumeroDeEdicao = numeroDeEdicao;
           AnoDePublicacao = anoDePublicacao;
           Caixa = caixa;
            Status = "Disponível";
        }

        public override void AtualizarRegistro(EntidadeBase registroAtualizado)
        {
            Revista revistaAtualizada = (Revista)registroAtualizado;

            Titulo = revistaAtualizada.Titulo;
            NumeroDeEdicao = revistaAtualizada.NumeroDeEdicao;
            AnoDePublicacao = revistaAtualizada.AnoDePublicacao;
        }

        public override string Validar()
        {
            string erros = "";

            if (string.IsNullOrWhiteSpace(Titulo))
                erros += "O titulo é obrigatório!\n";
            else if (Titulo.Length < 2 || Titulo.Length > 101)
                erros += "O titulo deve conter entre 2 e 100 caracteres!\n";

            else if (NumeroDeEdicao < 0)
                erros += "O numero da edição deve ser positivo!\n";

            else if (AnoDePublicacao > DateTime.Now)
                erros += "O ano de publicação deve ter uma data válida!\n";

            if (Caixa == null)
                erros += "Caixa está vazia";

            if (Status == null)
                erros += "Revista está emprestada";

            return erros;
        }
    }
}
