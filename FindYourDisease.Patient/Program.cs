using FindYourDisease.Patient.Application.Commands;
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

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5);
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();
