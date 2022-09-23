using CardStorageService.Core.Interfaces;
using CardStorageService.Core.Services;
using CardStorageService.Storage;
using CardStorageService.Storage.Interfaces;
using CardStorageService.Storage.Repositories;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.EntityFrameworkCore;
using NLog.Web;

namespace CardStorageService.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Configuring DbContext

            builder.Services.AddDbContext<DatabaseContext>(options =>
            {
                options.UseSqlServer(builder.Configuration["ConnectionString"]);
            });

            #endregion

            #region Logging

            builder.Services.AddHttpLogging(logging =>
            {
                logging.LoggingFields = HttpLoggingFields.All | HttpLoggingFields.RequestQuery;
                logging.RequestBodyLogLimit = 4096;
                logging.ResponseBodyLogLimit = 4096;
                logging.RequestHeaders.Add("Authorization");
                logging.RequestHeaders.Add("X-Real-IP");
                logging.RequestHeaders.Add("X-Forwarded-For");
            });

            builder.Host.ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddConsole();

            }).UseNLog(new NLogAspNetCoreOptions() { RemoveLoggerFactoryFilter = true });

            #endregion

            #region Repositories

            builder.Services.AddScoped<ICardRepository, CardRepository>();
            builder.Services.AddScoped<IClientRepository, ClientRepository>();
            builder.Services.AddScoped<IAccountRepository, AccountRepository>();
            builder.Services.AddScoped<IAccountSessionRepository, AccountSessionRepository>();

            #endregion

            #region Services

            builder.Services.AddScoped<ICardService, CardService>();
            builder.Services.AddScoped<IClientService, ClientService>();

            #endregion

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();


            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}