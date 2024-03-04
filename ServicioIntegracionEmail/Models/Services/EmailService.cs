
using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using MailKit.Security;
using MimeKit;

using ServicioIntegracionEmail.Models.Entity;

namespace ServicioIntegracionEmail.Models.Services;

public class EmailService : IEmailService
{
    public async Task<List<MimeMessage>> GetLastMessagesAsync(string host, int port, bool useSsl, int count)
    {
        using (var client = new ImapClient())
        {
            client.ServerCertificateValidationCallback = (s,c,h,e) => true;
            await client.ConnectAsync("imap.gmail.com", 993, SecureSocketOptions.SslOnConnect);

            // Ingresa las credenciales de tu cuenta de correo
            await client.AuthenticateAsync("ljosecarlos295@gmail.com", "qazx fevm xpdp oiew");

            var inbox = client.Inbox;
            await inbox.OpenAsync(FolderAccess.ReadOnly);

            // Obtén los últimos mensajes según el número especificado
            var uids = await inbox.SearchAsync(SearchQuery.All, cancellationToken: default);
            
            // Obtén los últimos mensajes ordenados por fecha de manera descendente
            // var uids = await inbox.SearchAsync(SearchQuery.Recent, cancellationToken: default);

            var messages = new List<MimeMessage>();
            for (int i = Math.Max(0, uids.Count - count); i < uids.Count; i++)
            {
                var uid = uids[i];
                var message = await inbox.GetMessageAsync(uid, cancellationToken: default);
                messages.Add(message);
            }

            await client.DisconnectAsync(true);

            return messages;
        }
    }
}