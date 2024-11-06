using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ProyectoColProfesionales.Data;
using ProyectoColProfesionales.Models.DB1;
using Microsoft.EntityFrameworkCore;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

public class NotificationJobScheduler : IHostedService, IDisposable
{
    private Timer _timer;
    private readonly IServiceScopeFactory _scopeFactory;

    public NotificationJobScheduler(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _timer = new Timer(CheckNotifications, null, TimeSpan.Zero, TimeSpan.FromSeconds(15));
        return Task.CompletedTask;
    }

    private async void CheckNotifications(object state)
    {
        using (var scope = _scopeFactory.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<DBColProfessionalContext>();
            var currentTime = DateTime.Now;

            // Buscar notificaciones con Status 1 que coincidan exactamente con la hora y minuto actuales
            var notifications = await context.Notifications2
                .Include(n => n.Person)
                .Where(n => n.Status == 1 &&
                           ((n.Date1.HasValue && n.Date1.Value.Hour == currentTime.Hour && n.Date1.Value.Minute == currentTime.Minute) ||
                            (n.Date2.HasValue && n.Date2.Value.Hour == currentTime.Hour && n.Date2.Value.Minute == currentTime.Minute) ||
                            (n.Date3.HasValue && n.Date3.Value.Hour == currentTime.Hour && n.Date3.Value.Minute == currentTime.Minute)))
                .ToListAsync();

            foreach (var notification in notifications)
            {
                if (notification.Person != null && !string.IsNullOrEmpty(notification.Person.PhoneNumber))
                {
                    await SendWhatsAppMessage(notification.Person.PhoneNumber, notification.Message);

                    // Marcar como enviado
                    notification.Status = 0;
                    await context.SaveChangesAsync();
                }
            }
        }
    }

    private async Task SendWhatsAppMessage(string phoneNumber, string message)
    {
        var options = new ChromeOptions();
        options.BinaryLocation = @"C:\Program Files\Google\Chrome\Application\chrome.exe";
        options.AddArgument("--no-sandbox");
        options.AddArgument("--disable-dev-shm-usage");

        // Concatenar el código de país +591 al número de teléfono
        string fullPhoneNumber = $"+591{phoneNumber}";

        using (var driver = new ChromeDriver(options))
        {
            try
            {
                driver.Navigate().GoToUrl($"https://web.whatsapp.com/send?phone={fullPhoneNumber}&text={Uri.EscapeDataString(message)}");

                await Task.Delay(60000); // Espera para cargar WhatsApp Web y el mensaje

                var sendButton = driver.FindElement(By.CssSelector("span[data-icon='send']"));
                sendButton.Click();

                Console.WriteLine($"Mensaje enviado a {fullPhoneNumber}: {message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al enviar mensaje: {ex.Message}");
            }
            finally
            {
                await Task.Delay(5000); // Espera antes de cerrar el navegador
                driver.Quit();
            }
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}
