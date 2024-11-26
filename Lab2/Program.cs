using StackExchange.Redis;
using Lab2.Data;

var builder = WebApplication.CreateBuilder(args);

string? connStr = builder.Configuration.GetConnectionString("ItemsDataConnection");
if(connStr!=null)
{
    builder.Services.AddSingleton<IConnectionMultiplexer>(
        ConnectionMultiplexer.Connect(connStr)
        );
}

builder.Services.AddScoped<IItemsData, ItemsData>();

// Add services to the container.

builder.Services.AddControllers();


builder.Services.AddSwaggerGen();



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
