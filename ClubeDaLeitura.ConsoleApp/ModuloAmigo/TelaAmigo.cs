
namespace ClubeDaLeitura.ConsoleApp.ModuloAmigos
{
    //Inserir( ), Editar( ), Excluir( ),VisualizarTodos(),VisualizarEmprestimos()
    public class TelaAmigo
    {
        protected override Amigos ObterDados()
        {
            Console.Write("Digite o título do chamado: ");
            string titulo = Console.ReadLine();

            Console.Write("Digite a descrição do chamado: ");
            string descricao = Console.ReadLine();

            DateTime dataAbertura = DateTime.Now;

            VisualizarEquipamentos();

            Console.Write("Digite o ID do equipamento que deseja selecionar: ");
            int idEquipamento = Convert.ToInt32(Console.ReadLine());

            Equipamento equipamentoSelecionado = (Equipamento)repositorioEquipamento.SelecionarRegistroPorId(idEquipamento);

            Chamado chamado = new Chamado(titulo, descricao, dataAbertura, equipamentoSelecionado);

            return chamado;
        }
    }
}
