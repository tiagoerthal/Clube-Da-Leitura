
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

        public Emprestimo(Amigo amigo, Revista revista, DateTime dataEmprestimo)
        {
            Amigo = amigo;
            Revista = revista;
            DataEmprestimo = dataEmprestimo;
            DataDevolucao = dataEmprestimo.AddDays(revista.Caixa.DiasEmprestimo);


            if (dataEmprestimo > DateTime.Now)
                revista.Status = "Reservada";
            else
                revista.Status = "Emprestada";
        }
        public override string Validar()
        {
            string erros = "";


            if (Amigo == null)
                erros += "Amigo está vazio";

            if (Revista == null)
                erros += "Revista está vazia";

            return erros;
        }

        public override void AtualizarRegistro(EntidadeBase registroAtualizado)
        {
            return;
        }


    }
}
