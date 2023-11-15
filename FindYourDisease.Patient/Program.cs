using FindYourDisease.Patient.Application.Commands;
using FindYourDisease.Patient.Endpoints;
using FindYourDisease.Patient.Options;
using FindYourDisease.Patient.Repository;
using FindYourDisease.Patient.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<IFileStorageService, FileStorageService>();
builder.Services.AddScoped<INotificationCollector, NotificationCollector>();

builder.Services.Configure<PathOptions>(builder.Configuration.GetSection(nameof(PathOptions)));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CreatePatientCommand>());

builder.Services.AddDbContext<PatientDbContext>(opt =>
{
    opt.UseNpgsql(builder.Configuration.GetConnectionString("PatientConnection"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPostCreatePatient();

app.Run();
