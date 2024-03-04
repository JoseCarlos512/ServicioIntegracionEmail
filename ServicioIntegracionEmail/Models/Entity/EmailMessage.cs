namespace ServicioIntegracionEmail.Models.Entity;

public class EmailMessage
{
    public string Subject { get; set; }
    public string Body { get; set; }
    public string Sender { get; set; }
    public DateTimeOffset SentDate { get; set; }
}