
using ClubeDaLeitura.ConsoleApp.Compartilhados;
using ClubeDaLeitura.ConsoleApp.ModuloRevista;

namespace ClubeDaLeitura.ConsoleApp.ModuloEmprestimo
{
    public class RepositorioEmprestimo : RepositorioBase
    {
        public bool ExisteEmprestimosVinculadas(int idAmigo)
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
