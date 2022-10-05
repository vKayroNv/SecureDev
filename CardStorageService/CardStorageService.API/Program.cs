using AutoMapper;
using CardStorageService.API.Models.Requests;
using CardStorageService.API.Validators;
using CardStorageService.Core.Interfaces;
using CardStorageService.Core.Services;
using CardStorageService.Storage;
using CardStorageService.Storage.Interfaces;
using CardStorageService.Storage.Repositories;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog.Web;
using System.Text;

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
            builder.Services.AddScoped<IAuthService, AuthService>();

            #endregion

            #region AutoMapper

            var mapperConfiguration = new MapperConfiguration(mp =>
            {
                mp.AddProfile(new API.Mapper.MapperProfile());
                mp.AddProfile(new Core.Mapper.MapperProfile());
            });
            var mapper = mapperConfiguration.CreateMapper();
            builder.Services.AddSingleton(mapper);

            #endregion

            #region Validators

            builder.Services.AddScoped<IValidator<AuthLoginRequest>, AuthLoginValidator>();
            builder.Services.AddScoped<IValidator<AuthRegisterRequest>, AuthRegisterValidator>();
            builder.Services.AddScoped<IValidator<CardCreateRequest>, CardCreateValidator>();
            builder.Services.AddScoped<IValidator<CardDeleteRequest>, CardDeleteValidator>();
            builder.Services.AddScoped<IValidator<CardGetByClientIdRequest>, CardGetByClientIdValidator>();
            builder.Services.AddScoped<IValidator<CardGetByIdRequest>, CardGetByIdValidator>();
            builder.Services.AddScoped<IValidator<CardUpdateRequest>, CardUpdateValidator>();
            builder.Services.AddScoped<IValidator<ClientCreateRequest>, ClientCreateValidator>();
            builder.Services.AddScoped<IValidator<ClientDeleteRequest>, ClientDeleteValidator>();
            builder.Services.AddScoped<IValidator<ClientGetByIdRequest>, ClientGetByIdValidator>();
            builder.Services.AddScoped<IValidator<ClientUpdateRequest>, ClientUpdateValidator>();

            #endregion

            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme =
                JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme =
                JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(AuthService.SecretKey)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CardStorageService", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "JWT Authorization header using the Bearer scheme(Example: 'Bearer 12345abcdef')",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme()
                        {
                            Reference = new OpenApiReference()
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });

            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}