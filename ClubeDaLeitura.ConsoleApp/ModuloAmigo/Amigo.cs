
using ClubeDaLeitura.ConsoleApp.Compartilhados;

namespace ClubeDaLeitura.ConsoleApp.ModuloAmigos
{
    //ObterEmprestimos()
    public class Amigo : EntidadeBase
    {
        public string nome;
        public string responsavel;
        public string telefone;

        public Amigo(string nome,string responsavel,string telefone)
        {
            this.nome = nome;
            this.responsavel = responsavel;
            this.telefone = telefone;
        }

        public override void AtualizarRegistro(EntidadeBase registroAtualizado)
        {
            Amigo amigoAtualizado = (Amigo)registroAtualizado;

            this.nome = amigoAtualizado.nome;
            this.responsavel = amigoAtualizado.responsavel;
            this.telefone = amigoAtualizado.telefone;
        }

        //Não pode haver amigos com o mesmo nome e telefone.
        // Não permitir excluir um amigo caso tenha empréstimos vinculados
        public override string Validar()
        {
            string erros = "";

            if (string.IsNullOrWhiteSpace(nome))
                erros += "O campo \"Nome\" é obrigatório.\n";

            else if (nome.Length < 3)
                erros += "O campo \"Nome\" precisa conter ao menos 3 caracteres.\n";

            if (string.IsNullOrWhiteSpace(responsavel))
                erros += "O campo \"Responsável\" é obrigatório.\n";

            else if (responsavel.Length < 3)
                erros += "O campo \"Responsável\" precisa conter ao menos 3 caracteres.\n";

            if (string.IsNullOrWhiteSpace(telefone))
                erros += "O campo \"Telefone\" é obrigatório!\n";

            else if (telefone.Length < 9)
                erros += "O campo \"Telefone\" deve conter no mínimo 9 caracteres!\n";

            return erros;
        }
    }
}
