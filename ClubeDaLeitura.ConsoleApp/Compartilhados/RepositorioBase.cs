
using ClubeDaLeitura.ConsoleApp.ModuloAmigos;
using ClubeDaLeitura.ConsoleApp.ModuloEmprestimo;

namespace ClubeDaLeitura.ConsoleApp.Compartilhados
{
    public abstract class RepositorioBase
    {
        private EntidadeBase[] registros = new EntidadeBase[100];
        private int contadorRegistros = 0;

        public void CadastrarRegistro(EntidadeBase novoRegistro)
        {
            registros[contadorRegistros] = novoRegistro;

            contadorRegistros++;
        }

        public bool EditarRegistro(int idSelecionado, EntidadeBase registroAtualizado)
        {
            EntidadeBase registroSelecionado = SelecionarRegistroPorId(idSelecionado);

            if (registroSelecionado == null)
                return false;

            registroSelecionado.AtualizarRegistro(registroAtualizado);

            return true;
        }

        public bool ExcluirRegistro(int idSelecionado)
        {
            for (int i = 0; i < registros.Length; i++)
            {
                if (registros[i] == null)
                    continue;

                else if (registros[i].id == idSelecionado)
                {
                    registros[i] = null;

                    return true;
                }
            }

            return false;
        }

        public EntidadeBase[] SelecionarRegistros()
        {
            return registros;
        }

        public EntidadeBase SelecionarRegistroPorId(int idSelecionado)
        {
            for (int i = 0; i < registros.Length; i++)
            {
                EntidadeBase registro = registros[i];

                if (registro == null)
                    continue;

                if (registro.id == idSelecionado)
                    return registro;
            }

            return null;
        }
        public bool ExistemEmprestimosDoAmigo(int idAmigo)
        {
            EntidadeBase[] registros = SelecionarRegistros();

            foreach (EntidadeBase registro in registros)
            {
                if (registro is Emprestimo emprestimo && emprestimo.Amigo != null && emprestimo.Amigo.id == idAmigo)
                    return true;
            }

            return false;
        }

    }
}
