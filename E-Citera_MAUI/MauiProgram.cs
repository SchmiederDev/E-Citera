using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

namespace E_Citera_MAUI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Poppins-Regular.ttf", "PoppinsRegular");
                });

#if DEBUG
		builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<TitleView>();
            
            builder.Services.AddTransient<TitleEditView>();
            builder.Services.AddTransient<TitleEditPage>();

            builder.Services.AddSingleton<CitationStylesPage>();
            builder.Services.AddSingleton<ReferenceListPage>();
            
            return builder.Build();
        }
    }
}