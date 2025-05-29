
using ClubeDaLeitura.ConsoleApp.Compartilhados;

namespace ClubeDaLeitura.ConsoleApp.ModuloAmigos
{
    //Validar(), ObterEmprestimos()
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
            Amigo chamadoAtualizado = (Amigo)registroAtualizado;

            this.nome = chamadoAtualizado.nome;
            this.responsavel = chamadoAtualizado.responsavel;
            this.telefone = chamadoAtualizado.telefone;
        }
    }
}
