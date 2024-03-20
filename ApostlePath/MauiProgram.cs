using ApostlePath.DataAccess.Data;
using ApostlePath.DataAccess.Repository;
using ApostlePath.View;
using ApostlePath.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ApostlePath
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("Rubik.ttf", "Rubik");
                    fonts.AddFont("Rubik-Italic.ttf", "RubikItalic");
                });

    		builder.Logging.AddDebug();

            builder.Services.AddDbContext<DataContext>(x => x.UseSqlite("Data Source="+Path.Combine(FileSystem.Current.AppDataDirectory, "QuestsDB.db")));

            builder.Services.AddScoped<IQuestsRepository, QuestsRepository>();

            builder.Services.AddViewModel<MainViewModel, MainPage>();
            builder.Services.AddViewModel<QuestViewModel, QuestPage>();

            //Ensure database exists
            using(var scope =  builder.Services.BuildServiceProvider())
            {
                scope.GetRequiredService<DataContext>().Database.Migrate();
            }

            return builder.Build();
        }

        private static void AddViewModel<TViewModel, TView>(this IServiceCollection services) where TView : ContentPage, new() where TViewModel : class
        {
            services.AddTransient<TViewModel>();
            services.AddTransient(s => new TView() { BindingContext = s.GetRequiredService<TViewModel>()});
        }
    }
}
