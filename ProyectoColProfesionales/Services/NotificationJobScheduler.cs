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
        try
        {
            // Configuración del timer para ejecutar el job cada 30 segundos
            _timer = new Timer(CheckNotifications, null, TimeSpan.Zero, TimeSpan.FromSeconds(30));

            // Verificar si hay un proceso de Chrome ya abierto con el perfil configurado
            var existingProcesses = System.Diagnostics.Process.GetProcessesByName("chrome")
                .Where(p => p.MainWindowTitle.Contains("WhatsApp") || p.MainWindowTitle.Contains("Google Chrome"));

            foreach (var process in existingProcesses)
            {
                try
                {
                    process.Kill(); // Cierra cualquier instancia existente
                    process.WaitForExit();
                    Console.WriteLine($"Cerrando instancia de Chrome: {process.Id}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al cerrar la instancia de Chrome: {ex.Message}");
                }
            }

            // Configurar el navegador al inicio
            StartBrowser();

            // Monitorear la ventana de Chrome para evitar que se cierre
            Task.Run(() => MonitorChromeWindow());
        }
        catch (WebDriverException ex) when (ex.Message.Contains("session not created") || ex.Message.Contains("DevToolsActivePort"))
        {
            Console.WriteLine("Error al iniciar ChromeDriver: " + ex.Message);
            Console.WriteLine("Por favor, asegúrate de que no hay una ventana de Chrome ya abierta.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error inesperado al iniciar el servicio: " + ex.Message);
        }

        return Task.CompletedTask;
    }

    private void StartBrowser()
    {
        try
        {
            var options = new ChromeOptions();
            options.AddArgument("--user-data-dir=C:\\Users\\VALDIVIA\\AppData\\Local\\Google\\Chrome\\User Data\\SeleniumProfile");
            options.AddArgument("--profile-directory=Default");
            options.BinaryLocation = @"C:\\Program Files\\Google\\Chrome\\Application\\chrome.exe";
            options.AddArgument("--no-sandbox");
            options.AddArgument("--disable-dev-shm-usage");

            _driver = new ChromeDriver(options);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al iniciar ChromeDriver: {ex.Message}");
        }
    }

    private async Task MonitorChromeWindow()
    {
        try
        {
            while (true)
            {
                if (_driver == null || _driver.WindowHandles.Count == 0)
                {
                    Console.WriteLine("La ventana de Chrome fue cerrada. Por favor, no cierres la ventana de Chrome mientras el servicio está en ejecución.");
                    break; // Opcional: Puedes intentar reiniciar Chrome aquí si lo deseas
                }
                await Task.Delay(5000); // Verificar cada 5 segundos
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al monitorear la ventana de Chrome: {ex.Message}");
        }
    }

    private async void CheckNotifications(object state)
    {
        try
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
                    try
                    {
                        int sentCount = 0; // Contador de notificaciones enviadas

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

                        if (sentCount == 3)
                        {
                            notification.Status = 0;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error al enviar notificación para el usuario {notification.Person.PhoneNumber}: {ex.Message}");
                    }
                }

                await context.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al ejecutar CheckNotifications: {ex.Message}");
        }
    }

    private async Task SendWhatsAppMessage(string phoneNumber, string message)
    {
        try
        {
            if (_driver == null)
            {
                Console.WriteLine("El navegador no está inicializado. Intentando reiniciar Chrome...");
                StartBrowser();
            }

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
        try
        {
            _timer?.Change(Timeout.Infinite, 0);

            if (_driver != null)
            {
                _driver.Quit();
                _driver.Dispose();
                _driver = null;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al detener ChromeDriver: {ex.Message}");
        }

        return Task.CompletedTask;
    }

    public void Dispose()
    {
        try
        {
            _timer?.Dispose();

            if (_driver != null)
            {
                _driver.Quit();
                _driver.Dispose();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al liberar ChromeDriver: {ex.Message}");
        }
    }
}
