using EnglishHelperService.API.Extensions;
using EnglishHelperService.Business.Settings;
using EnglishHelperService.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//appSettings
var logSettings = new LogSettings(builder.Configuration);
var databaseSettings = new DatabaseSettings(builder.Configuration);



builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerServices();
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddDatabaseServices(databaseSettings);
builder.Services.AddDependencyInjectionServices();
builder.Services.AddLoggingService(logSettings);

builder.Services.AddCors();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

#region If database is not exists or there is a new migration to update, this will handle it.

using var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<DataContext>();
if (db.Database.GetPendingMigrations().Any())
{
	db.Database.Migrate();
}

#endregion

app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("*"));

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
