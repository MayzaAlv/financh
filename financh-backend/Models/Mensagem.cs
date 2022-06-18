using MimeKit;

namespace financh_backend.Models
{
    public class Mensagem
    {
        public List<MailboxAddress> Destinatario { get; set; }
        public string Assunto { get; set; }
        public string Conteudo { get; set; }

        public Mensagem(IEnumerable<string> destinatario, string assunto,
            int usuarioId, string[] usernames, string codigo)
        {
            Destinatario = new List<MailboxAddress>();
            for (int i = 0; i < usernames.Count(); i++)
            {
                MailboxAddress mba = new MailboxAddress(usernames[i], destinatario.ToList()[i]);
                Destinatario.Add(mba);
            }
            Assunto = assunto;
            Conteudo = $"http://localhost:5146/Ativar?Id={usuarioId}&CodigoAtivacao={codigo}";
        }
    }
}
