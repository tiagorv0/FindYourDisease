using FindYourDisease.Users.API.Configuration;
using FindYourDisease.Users.Infra.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDatabaseContext(builder.Configuration)
                .AddInfrastructureAndServices(builder.Configuration)
                .AddPresentation()
                .AddAuthenticationAndAuthorization(builder.Configuration);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<UserDbContext>();

    dbContext.Database.EnsureCreated();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

