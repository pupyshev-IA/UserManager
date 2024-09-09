using Abdt.Loyal.UserManager.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var dbConnection = builder.Configuration.GetConnectionString("PostgresConnection");
if (string.IsNullOrWhiteSpace(dbConnection))
    throw new ArgumentException(nameof(dbConnection));

builder.Services.AddDbContext<UserContext>(options => options.UseNpgsql(dbConnection));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
