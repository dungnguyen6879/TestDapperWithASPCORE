using WebAPI;
using WebAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ApplicationDBContext>();
builder.Services.AddScoped<IContact, ContactRepository>();

// Add services to the container.
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
