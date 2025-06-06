
using System;
using ClubeDaLeitura.ConsoleApp.Compartilhados;

namespace ClubeDaLeitura.ConsoleApp.ModuloRevista
{
    public class RepositorioRevista : RepositorioBase
    {
        public bool ExistemRevistasVinculadas(int idRevista)
        {
            EntidadeBase[] registros = SelecionarRegistros(); 

            foreach (EntidadeBase registro in registros)
            {
                if (registro is Revista revista && revista.Caixa != null && revista.Caixa.id == idRevista)
                    return true;
            }

            return false;
        }
    }
}
