using DotNetEnv;
using EMDR42.API.Extensions;
using Infrastructure.Hubs;

Env.TraversePath().Load("../.env");

var builder = WebApplication.CreateBuilder(args);

builder.AddDataBase();

builder.Services.AddSignalR();
builder.Services.AddControllers();
builder.Services.AddServices();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();
app.UseCors(o => o.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());

app.UseHttpsRedirection();


app.MapControllers();
app.MapHub<ChatHub>("/hub");

app.Run();
