namespace ProyectoColProfesionales.Servicios.Contrato
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string to, string subject, string htmlMessage, MemoryStream adjunto = null, string nombreArchivo = null);

    }
}
