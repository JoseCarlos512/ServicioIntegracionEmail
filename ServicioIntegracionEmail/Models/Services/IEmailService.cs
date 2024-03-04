using MimeKit;
using ServicioIntegracionEmail.Models.Entity;

namespace ServicioIntegracionEmail.Models.Services;

public interface IEmailService
{
    Task<List<MimeMessage>> GetLastMessagesAsync(string host, int port, bool useSsl, int count);
}