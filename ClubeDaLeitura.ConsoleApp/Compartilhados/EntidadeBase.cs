

namespace ClubeDaLeitura.ConsoleApp.Compartilhados
{
    public class EntidadeBase
    {
        public int id;

        public abstract void AtualizarRegistro(EntidadeBase registroAtualizado);
        public abstract string Validar();
    }
}
