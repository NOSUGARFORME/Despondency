using System.Reflection;
using Despondency.Application;
using Despondency.Infrastructure.Persistence;
using Polly;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddPersistence(builder.Configuration["ConnectionStrings:Despondency"],
    typeof(Program).GetTypeInfo().Assembly.GetName().Name);

builder.Services.AddApplicationServices();

var app = builder.Build();

Policy.Handle<Exception>().WaitAndRetry(new[]
    {
        TimeSpan.FromSeconds(10),
        TimeSpan.FromSeconds(20),
        TimeSpan.FromSeconds(30),
    })
    .Execute(() =>
    {
        app.MigrateAdsDb();
    });

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();