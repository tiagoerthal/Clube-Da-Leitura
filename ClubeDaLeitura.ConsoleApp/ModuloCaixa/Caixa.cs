

using System.Runtime.ConstrainedExecution;
using ClubeDaLeitura.ConsoleApp.Compartilhados;

namespace ClubeDaLeitura.ConsoleApp.ModuloCaixa
{
     //AdicionarRevista(),RemoverRevista()
    public class Caixa : EntidadeBase
    {
        public string Cor {  get; set; }
        public string Etiqueta { get; set; }
        public int DiasEmprestimo { get; set; }

        public Caixa(string cor, string etiqueta)
        {
            Cor = cor;
            Etiqueta = etiqueta;
            DiasEmprestimo = 7;
        }
        public Caixa(string etiqueta, string cor, int diasEmprestimo)
        {
            Etiqueta = etiqueta;
            Cor = cor;
            DiasEmprestimo = diasEmprestimo;
        }

        public override void AtualizarRegistro(EntidadeBase registroAtualizado)
        {
            Caixa caixaAtualizada = (Caixa)registroAtualizado;

            this.Etiqueta = caixaAtualizada.Etiqueta;
            this.Cor = caixaAtualizada.Cor;
            this.DiasEmprestimo = caixaAtualizada.DiasEmprestimo;
        }

        //Não pode haver etiquetas duplicadas
        //Não permitir excluir uma caixa caso tenha revistas vinculadas
        //Cada caixa define o prazo máximo para empréstimo de suas revistas
        public override string Validar()
        {
            string erros = string.Empty;

            if (string.IsNullOrWhiteSpace(Etiqueta) || Etiqueta.Length > 50)
                erros += "O campo \"Etiqueta\" é obrigatório e recebe no máximo 50 caracteres.";

            if (string.IsNullOrWhiteSpace(Cor))
                erros += "O campo \"Cor\" é obrigatório.";

            if (DiasEmprestimo < 1)
                erros += "O campo \"Dias de Empréstimo\" deve conter um valor maior que 0.";

            return erros;
        }
    }
}
