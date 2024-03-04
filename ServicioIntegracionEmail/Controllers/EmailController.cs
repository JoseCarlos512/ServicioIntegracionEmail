using Microsoft.AspNetCore.Mvc;
using ServicioIntegracionEmail.Models.Entity;
using ServicioIntegracionEmail.Models.Services;

namespace ServicioIntegracionEmail.Controllers;

public class EmailController : ControllerBase
{
    private readonly IEmailService _emailService;
    private readonly IConfiguration _configuration;

    public EmailController(IEmailService emailService, IConfiguration configuration)
    {
        _emailService = emailService;
        _configuration = configuration;
    }

    [HttpGet("lastMessages")]
    public async Task<IActionResult> GetLastMessages()
    {
        string host = _configuration["Email:ImapHost"];
        int port = int.Parse(_configuration["Email:ImapPort"]);
        bool useSsl = bool.Parse(_configuration["Email:UseSsl"]);
        int count = 10;  // O el número de mensajes que desees obtener

        try
        {
            var lastMessages = await _emailService.GetLastMessagesAsync(host, port, useSsl, count);

            // Mapea los MimeMessage a tu modelo EmailDetails
            var emailDetailsList = lastMessages.Select(message => new EmailMessage
            {
                Subject = message.Subject,
                Body = message.TextBody,
                Sender = message.From.ToString(),
                SentDate = message.Date
                // Agrega más mapeos según sea necesario
            }).ToList();

            return Ok(emailDetailsList);
        }
        catch (Exception ex)
        {
            // Manejo de errores, puedes devolver un error 500 Internal Server Error
            return StatusCode(500, $"Error al obtener los mensajes: {ex.Message}");
        }
    }
}