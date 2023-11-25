using FindYourDisease.Users.Application.Commands.UserCommand;
using FindYourDisease.Users.Application.Enum;
using FindYourDisease.Users.Domain.Options;
using FindYourDisease.Users.Infra.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Scrutor;
using System.Data;
using System.Text;
using System.Text.Json.Serialization;

namespace FindYourDisease.Users.API.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddDatabaseContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<UserDbContext>(opt =>
        {
            opt.UseNpgsql(configuration.GetConnectionString("UserConnection"));
        });

        return services;
    }

    public static IServiceCollection AddInfrastructureAndServices(this IServiceCollection services, IConfiguration configuration)
    {
        services
                .Scan(selector => selector
                                    .FromAssemblies(
                                            Infra.AssemblyReference.Assembly,
                                            Application.AssemblyReference.Assembly)
                                    .AddClasses(false)
                                    .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                                    .AsMatchingInterface()
                .WithScopedLifetime());

        services.Configure<PathOptions>(configuration.GetSection(nameof(PathOptions)));
        services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));

        return services;
    }

    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CreateUserCommand>());

        //services.AddFluentValidationAutoValidation();
        //services.AddValidatorsFromAssemblyContaining<CategoryRequestValidation>();
        //services.Configure<ApiBehaviorOptions>(options =>
        //{
        //    options.SuppressModelStateInvalidFilter = true;
        //});

        services
            .AddControllers(options =>
            {
                //options.Filters.Add(typeof(ValidationAttribute));
            })
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

        services.AddHttpContextAccessor();

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "FindYourDisease.Users", Version = "v1" });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                    },
                    new List<string>()
                }
                });
        });

        //services.AddStackExchangeRedisCache(o => {
        //    o.InstanceName = "instance";
        //    o.Configuration = "localhost:6379";
        //});

        return services;
    }

    public static IServiceCollection AddAuthenticationAndAuthorization(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["JwtOptions:Key"])),
                ValidateLifetime = true,
                ValidAudience = configuration["JwtOptions:Audience"],
                ValidIssuer = configuration["JwtOptions:Issuer"],
            };
        });

        services.AddAuthorization(opt =>
        {
            opt.AddPolicy(RolePolicy.Patient, policy => policy.RequireRole(RolePolicy.Patient));
            opt.AddPolicy(RolePolicy.User, policy => policy.RequireRole(RolePolicy.User));
        });

        return services;
    }
}
