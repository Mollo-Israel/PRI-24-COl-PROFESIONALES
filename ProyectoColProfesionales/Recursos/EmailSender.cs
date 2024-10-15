using Microsoft.Extensions.Options;
using static ProyectoColProfesionales.Recursos.EmailSender;
using System.Net.Mail;
using System.Net;
using ProyectoColProfesionales.Servicios.Contrato;



namespace ProyectoColProfesionales.Recursos
{
    public class EmailSender : IEmailSender
    {


        private readonly SmtpSettings _smtpSettings;

        public EmailSender(IOptions<SmtpSettings> smtpSettings)
        {
            _smtpSettings = smtpSettings.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage, MemoryStream adjunto = null, string nombreArchivo = null)
        {
            var mailMessage = new MailMessage()
            {
                From = new MailAddress(_smtpSettings.Sender),
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true
            };
            mailMessage.To.Add(email);
            
            if (adjunto != null && !string.IsNullOrEmpty(nombreArchivo))
            {
                Attachment adjuntoFile = new Attachment(adjunto, nombreArchivo);
                mailMessage.Attachments.Add(adjuntoFile);
            }

            using (var client = new SmtpClient(_smtpSettings.Server, _smtpSettings.Port))
            {
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(_smtpSettings.Username, _smtpSettings.Password);
                client.EnableSsl = true;
                await client.SendMailAsync(mailMessage);
            }


        }
    }
}
