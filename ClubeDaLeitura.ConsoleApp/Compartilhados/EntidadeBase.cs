

namespace ClubeDaLeitura.ConsoleApp.Compartilhados
{
    public abstract class EntidadeBase
    {
        public int Id;

        public abstract void AtualizarRegistro(EntidadeBase registroAtualizado);
        public abstract string Validar();
    }
}
