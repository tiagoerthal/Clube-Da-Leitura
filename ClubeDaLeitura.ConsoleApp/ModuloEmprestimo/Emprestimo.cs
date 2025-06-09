
using ClubeDaLeitura.ConsoleApp.Compartilhados;
using ClubeDaLeitura.ConsoleApp.ModuloAmigos;
using ClubeDaLeitura.ConsoleApp.ModuloRevista;

namespace ClubeDaLeitura.ConsoleApp.ModuloEmprestimo
{
    public class Emprestimo : EntidadeBase
    {
        public Amigo Amigo {  get; set; }
        public Revista Revista { get; set; }
        public DateTime DataEmprestimo { get; set; }
        public DateTime DataDevolucao { get; set; }

        public string Status { get; set; }

        public Emprestimo(Amigo amigo, Revista revista)
        {
            Amigo = amigo;
            Revista = revista;
            DataEmprestimo = DateTime.Now;
            DataDevolucao = DataEmprestimo.AddDays(revista.Caixa.DiasEmprestimo);
            Status = "Aberto";

            //if (dataEmprestimo > DateTime.Now)
            //    revista.Status = "Reservada";
            //else
            //    revista.Status = "Emprestada";
        }  

        public override string Validar()
        {
            string erros = string.Empty;

            if (Amigo == null)
                erros += "Amigo está vazio";

            if (Revista == null)
                erros += "Revista está vazia";

            return erros;
        }

        public override void AtualizarRegistro(EntidadeBase registroAtualizado)
        {
            Status = "Concluído";
        }
    }
}
