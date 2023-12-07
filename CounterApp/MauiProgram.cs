using Microsoft.Extensions.Logging;
using Syncfusion.Maui.Core.Hosting;
namespace CounterApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.ConfigureSyncfusionCore();
            builder.UseMauiApp<App>().ConfigureFonts(fonts =>
            {   
                 fonts.AddFont("Cheque-Regular.otf", "ChequeRegular");
                 fonts.AddFont("Cheque-Black.otf", "ChequeBlack");
            });
#if DEBUG
    		builder.Logging.AddDebug();
#endif
            return builder.Build();
        }
    }
}
