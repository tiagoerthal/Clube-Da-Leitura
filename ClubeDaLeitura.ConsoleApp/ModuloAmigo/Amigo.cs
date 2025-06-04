
using System.Text.RegularExpressions;
using ClubeDaLeitura.ConsoleApp.Compartilhados;

namespace ClubeDaLeitura.ConsoleApp.ModuloAmigos
{
    //ObterEmprestimos()
    public class Amigo : EntidadeBase
    {
        public string Nome { get; set; }
        public string Responsavel { get; set; }
        public string Telefone { get; set; }

        public Amigo(string nome,string responsavel,string telefone)
        {
            Nome = nome;
            Responsavel = responsavel;
            Telefone = telefone;
        }

        public override void AtualizarRegistro(EntidadeBase registroAtualizado)
        {
            Amigo amigoAtualizado = (Amigo)registroAtualizado;

            this.Nome = amigoAtualizado.Nome;
            this.Responsavel = amigoAtualizado.Responsavel;
            this.Telefone = amigoAtualizado.Telefone;
        }

        //Não pode haver amigos com o mesmo nome e telefone.
        // Não permitir excluir um amigo caso tenha empréstimos vinculados

        public override string Validar()
        {
            string erros = string.Empty;

            if (Nome.Length < 3 || Nome.Length > 100)
                erros += "O campo \"Nome\" deve conter entre 3 e 100 caracteres.";

            if (Responsavel.Length < 3 || Responsavel.Length > 100)
                erros += "O campo \"Nome do Responsável\" deve conter entre 3 e 100 caracteres.";

            if (!Regex.IsMatch(Telefone, @"^\(?\d{2}\)?\s?(9\d{4}|\d{4})-?\d{4}$"))
                erros += "O campo \"Telefone\" deve seguir o padrão (DDD) 90000-0000.";

            return erros;
        }
    }
}
