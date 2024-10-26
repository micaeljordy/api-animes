using Animes.Infra.Data.Context;
using Animes.Infrastructure.IoC;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure os serviços adicionando as dependências de infraestrutura
DependencyContainer.RegisterServices(builder.Services);

// Adicione outros serviços, controllers, etc.
builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();