using Abdt.Loyal.UserManager.BusinessLogic;
using Abdt.Loyal.UserManager.BusinessLogic.Abstractions;
using Abdt.Loyal.UserManager.Configuration;
using Abdt.Loyal.UserManager.Domain;
using Abdt.Loyal.UserManager.Repository;
using Abdt.Loyal.UserManager.Repository.Abstractions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var dbConnection = builder.Configuration.GetConnectionString("PostgresConnection");
if (string.IsNullOrWhiteSpace(dbConnection))
    throw new ArgumentException(nameof(dbConnection));

builder.Services.AddDbContext<UserContext>(options => options.UseNpgsql(dbConnection));
builder.Services.AddAutoMapper(typeof(ControllersMappingProfile));

builder.Services.AddScoped<IAccountManager<User>, UserService>();
builder.Services.AddScoped<IRepository<User>, UserRepository>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();

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
