using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using ProyectoColProfesionales.Data;
using ProyectoColProfesionales.Models.DB1;
using Microsoft.EntityFrameworkCore;

public class NotificationJobScheduler : IHostedService, IDisposable
{
    private Timer _timer;
    private readonly IServiceScopeFactory _scopeFactory;
    private ChromeDriver _driver;

    public NotificationJobScheduler(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        // Configuración del timer para ejecutar el job cada 1 minuto
        _timer = new Timer(CheckNotifications, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));

        // Configurar el navegador al inicio
        var options = new ChromeOptions();
        options.AddArgument("--user-data-dir=C:\\Users\\VALDIVIA\\AppData\\Local\\Google\\Chrome\\User Data\\SeleniumProfile");
        options.AddArgument("--profile-directory=Default");
        options.BinaryLocation = @"C:\Program Files\Google\Chrome\Application\chrome.exe";
        options.AddArgument("--no-sandbox");
        options.AddArgument("--disable-dev-shm-usage");

        _driver = new ChromeDriver(options);

        return Task.CompletedTask;
    }

    private async void CheckNotifications(object state)
    {
        using (var scope = _scopeFactory.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<DBColProfessionalContext>();
            var currentTime = DateTime.Now;

            // Buscar notificaciones con Status 1 según fecha y hora actuales
            var notifications = await context.Notifications2
                .Include(n => n.Person)
                .Where(n => n.Status == 1 && (
                           (n.Date1.HasValue && n.Date1.Value.Date == currentTime.Date && n.Date1.Value.Hour == currentTime.Hour && n.Date1.Value.Minute == currentTime.Minute) ||
                           (n.Date2.HasValue && n.Date2.Value.Date == currentTime.Date && n.Date2.Value.Hour == currentTime.Hour && n.Date2.Value.Minute == currentTime.Minute) ||
                           (n.Date3.HasValue && n.Date3.Value.Date == currentTime.Date && n.Date3.Value.Hour == currentTime.Hour && n.Date3.Value.Minute == currentTime.Minute)))
                .ToListAsync();

            foreach (var notification in notifications)
            {
                int sentCount = 0; // Contador de notificaciones enviadas

                // Verificar y enviar notificaciones en Date1, Date2, Date3
                if (notification.Date1.HasValue && notification.Date1.Value.Date == currentTime.Date &&
                    notification.Date1.Value.Hour == currentTime.Hour && notification.Date1.Value.Minute == currentTime.Minute)
                {
                    await SendWhatsAppMessage(notification.Person.PhoneNumber, notification.Message);
                    sentCount++;
                }

                if (notification.Date2.HasValue && notification.Date2.Value.Date == currentTime.Date &&
                    notification.Date2.Value.Hour == currentTime.Hour && notification.Date2.Value.Minute == currentTime.Minute)
                {
                    await SendWhatsAppMessage(notification.Person.PhoneNumber, notification.Message);
                    sentCount++;
                }

                if (notification.Date3.HasValue && notification.Date3.Value.Date == currentTime.Date &&
                    notification.Date3.Value.Hour == currentTime.Hour && notification.Date3.Value.Minute == currentTime.Minute)
                {
                    await SendWhatsAppMessage(notification.Person.PhoneNumber, notification.Message);
                    sentCount++;
                }

                // Si se han enviado las 3 notificaciones, cambiar el estado a 0
                if (sentCount == 3)
                {
                    notification.Status = 0;
                }

                await context.SaveChangesAsync();
            }
        }
    }

    private async Task SendWhatsAppMessage(string phoneNumber, string message)
    {
        try
        {
            string fullPhoneNumber = $"+591{phoneNumber}";
            _driver.Navigate().GoToUrl($"https://web.whatsapp.com/send?phone={fullPhoneNumber}&text={Uri.EscapeDataString(message)}");

            // Esperar a que se cargue la página
            await Task.Delay(8000);

            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
            var sendButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("span[data-icon='send']")));
            sendButton.Click();

            Console.WriteLine($"Mensaje enviado a {fullPhoneNumber}: {message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al enviar mensaje: {ex.Message}");
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        _driver?.Quit(); // Cerrar el navegador al detener el servicio
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
        _driver?.Dispose();
    }
}
