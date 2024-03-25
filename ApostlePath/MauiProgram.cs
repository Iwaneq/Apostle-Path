using ApostlePath.DataAccess.Data;
using ApostlePath.DataAccess.Repository;
using ApostlePath.Factory;
using ApostlePath.Services;
using ApostlePath.View;
using ApostlePath.ViewModel;
using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Views;
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
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("Rubik.ttf", "Rubik");
                    fonts.AddFont("Rubik-Italic.ttf", "RubikItalic");
                });

    		builder.Logging.AddDebug();

            builder.Services.AddDbContext<DataContext>(x =>
            {
                x.UseSqlite("Data Source=" + Path.Combine(FileSystem.Current.AppDataDirectory, "QuestsDB.db"));
                x.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                x.EnableSensitiveDataLogging(true);
            });

            builder.Services.AddScoped<IQuestViewModelFactory, QuestViewModelFactory>();

            builder.Services.AddScoped<IQuestsRepository, QuestsRepository>();
            builder.Services.AddScoped<IQuestProgressChecker, QuestProgressChecker>();

            builder.Services.AddViewModelWithView<CompactQuestViewModel, CompactQuestView>();
            builder.Services.AddViewModelWithPage<MainViewModel, MainPage>();
            builder.Services.AddViewModelWithPage<QuestViewModel, QuestPage>();
            builder.Services.AddViewModelWithPage<CreateQuestViewModel, CreateQuestPage>();
            builder.Services.AddViewModelWithPage<AskForNameViewModel, AskForNameView>();

            //Ensure database exists
            using(var scope =  builder.Services.BuildServiceProvider())
            {
                var dataContext = scope.GetRequiredService<DataContext>();
                dataContext.Database.Migrate();

#if DEBUG
                Task.Run(dataContext.SeedData).Wait();
#endif
                var questChecker = scope.GetRequiredService<IQuestProgressChecker>();
                Task.Run(questChecker.CheckProgress).Wait();
            }

            return builder.Build();
        }

        private static void AddViewModelWithPage<TViewModel, TView>(this IServiceCollection services) where TView : ContentPage, new() where TViewModel : class
        {
            services.AddTransient<TViewModel>();
            services.AddTransient(s => new TView() { BindingContext = s.GetRequiredService<TViewModel>()});
        }

        private static void AddViewModelWithView<TViewModel, TView>(this IServiceCollection services) where TView : ContentView, new() where TViewModel : class
        {
            services.AddTransient<TViewModel>();
            services.AddTransient(s => new TView() { BindingContext = s.GetRequiredService<TViewModel>() });
        }
    }
}
