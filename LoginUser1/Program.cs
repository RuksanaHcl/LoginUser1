using LoginUser;
using LoginUser.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IUser, UserRepository>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddDbContext<DbContextClass>();
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