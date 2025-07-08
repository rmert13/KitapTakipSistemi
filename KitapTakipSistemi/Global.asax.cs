using Serilog;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace KitapTakipSistemi
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // Logs klasörünü oluştur
            var logPath = Server.MapPath("~/Logs");
            if (!Directory.Exists(logPath))
            {
                Directory.CreateDirectory(logPath);
            }

            // Serilog yapılandırması
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File(
                    Path.Combine(logPath, "log-.txt"), // log-20250706.txt gibi
                    rollingInterval: RollingInterval.Day,
                    retainedFileCountLimit: 7,          // sadece son 7 günlük log tutulur
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{Exception}"
                )
                .CreateLogger();

            Log.Information("📦 Uygulama başlatıldı.");

            // MVC başlangıç ayarları
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_End()
        {
            Log.Information("⛔ Uygulama sonlandırıldı.");
            Log.CloseAndFlush();
        }
    }
}
