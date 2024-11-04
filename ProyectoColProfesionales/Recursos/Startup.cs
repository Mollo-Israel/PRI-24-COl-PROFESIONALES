using ProyectoColProfesionales.Servicios.Contrato;

namespace ProyectoColProfesionales.Recursos
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Configurar los settings de SMTP desde appsettings.json
            services.Configure<SmtpSettings>(Configuration.GetSection("SmtpSettings"));

            // Registrar el servicio de envío de emails
            services.AddSingleton<IEmailSender, EmailSender>();

            // Añadir otros servicios necesarios para la aplicación
            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
