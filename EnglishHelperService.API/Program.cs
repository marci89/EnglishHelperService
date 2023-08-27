using EnglishHelperService.Business;
using EnglishHelperService.Persistence;
using EnglishHelperService.Persistence.Repositories;
using EnglishHelperService.Persistence.UnitOfWork;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

#region Swagger

// Add Swagger generation
builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo { Title = "English Helper Service", Version = "v1" });
	// Add the security definition for bearer tokens
	c.AddSecurityDefinition(name: "Bearer", securityScheme: new OpenApiSecurityScheme
	{
		Name = "Authorization",
		Description = "Enter the Bearer Authorization string as following: `Bearer Generated-JWT-Token`",
		In = ParameterLocation.Header,
		Type = SecuritySchemeType.ApiKey,
		Scheme = "Bearer"
	});
	// Add the security Requirement for bearer tokens
	c.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{
			new OpenApiSecurityScheme
			{
				Name = "Bearer",
				In = ParameterLocation.Header,
				Reference = new OpenApiReference
				{
					Id = "Bearer",
					Type =ReferenceType.SecurityScheme
				}
			},
			new List<string>()
		}
	});
});

#endregion


#region Auth

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(options =>
				{
					options.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuerSigningKey = true,
						IssuerSigningKey = new SymmetricSecurityKey(Encoding
							.UTF8.GetBytes(builder.Configuration["TokenKey"])),
						ValidateIssuer = false,
						ValidateAudience = false
					};
				});

#endregion

builder.Services.AddDbContext<DataContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});


builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<UserFactory>();
builder.Services.AddScoped<PasswordSecurityHandler>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITokenService, TokenService>();





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


app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
