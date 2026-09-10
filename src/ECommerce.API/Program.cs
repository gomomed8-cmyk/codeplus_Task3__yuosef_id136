using ECommerce.API.Hubs;
using ECommerce.API.Middleware;
using ECommerce.Application.DependencyInjection;
using ECommerce.Infrastructure.DependencyInjection;
using ECommerce.Infrastructure.Persistence;
using QuestPDF.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationServices();
builder.Services.AddSignalR();
builder.Services.AddInfrastructureServices(builder.Configuration);
QuestPDF.Settings.License = LicenseType.Evaluation;

var app = builder.Build();
app.UseMiddleware<ExceptionHandlingMiddleware>();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.MapHub<ChatHub>("/hubs/chat");
app.MapHub<OrderTrackingHub>("/hubs/order-tracking");

app.Run();

public partial class Program { }
