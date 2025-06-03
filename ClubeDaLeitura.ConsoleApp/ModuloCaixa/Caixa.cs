

using ClubeDaLeitura.ConsoleApp.Compartilhados;

namespace ClubeDaLeitura.ConsoleApp.ModuloCaixa
{
     //AdicionarRevista(),RemoverRevista()
    public class Caixa : EntidadeBase
    {
        public string cor;
        public string etiqueta;
        public int diasEmprestimo;

        public Caixa(string cor, string etiqueta, int diasEmprestimo)
        {
            this.cor = cor;
            this.etiqueta = etiqueta;
            this.diasEmprestimo = diasEmprestimo;
        }

        public override void AtualizarRegistro(EntidadeBase registroAtualizado)
        {
            Caixa caixaAtualizada = (Caixa)registroAtualizado;

            this.etiqueta = caixaAtualizada.etiqueta;
            this.cor = caixaAtualizada.cor;
            this.diasEmprestimo = caixaAtualizada.diasEmprestimo;
        }

        //Não pode haver etiquetas duplicadas
        //Não permitir excluir uma caixa caso tenha revistas vinculadas
        //Cada caixa define o prazo máximo para empréstimo de suas revistas
        public override string Validar()
        {
            string erros = "";

            if (string.IsNullOrWhiteSpace(etiqueta))
                erros += "O campo \"Etiqueta\" é obrigatória!\n";

            else if (etiqueta.Length < 3 || etiqueta.Length > 51)
                erros += "O campo \"Etiqueta\" deve conter entre 3 e 50 caracteres!\n";

            if (string.IsNullOrWhiteSpace(cor))
                erros += "O campo \"Cor\" é obrigatória!\n";

            else if (cor.Length < 4 || cor.Length > 99)
                erros += "O campo \"Cor\" deve conter entre 4 e 99 caracteres!\n";

            if (diasEmprestimo != 7)
                erros += "O campo \"Empréstimo\" deve conter 7 dias!\n";

            return erros;
        }
    }
}
