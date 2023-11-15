using FindYourDisease.Patient.Application.Commands;
using FindYourDisease.Patient.Application.Login;
using FindYourDisease.Patient.Options;
using FindYourDisease.Patient.Repository;
using FindYourDisease.Patient.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(x =>
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
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["JwtOptions:Key"])),
                ValidateLifetime = true,
                ValidAudience = builder.Configuration["JwtOptions:Audience"],
                ValidIssuer = builder.Configuration["JwtOptions:Issuer"],
            };
        });

builder.Services.AddAuthorization();

builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<IFileStorageService, FileStorageService>();
builder.Services.AddScoped<INotificationCollector, NotificationCollector>();
builder.Services.AddScoped<ILoginService, LoginService>();

builder.Services.Configure<PathOptions>(builder.Configuration.GetSection(nameof(PathOptions)));
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(nameof(JwtOptions)));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CreatePatientCommand>());

builder.Services.AddDbContext<PatientDbContext>(opt =>
{
    opt.UseNpgsql(builder.Configuration.GetConnectionString("PatientConnection"));
});

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
