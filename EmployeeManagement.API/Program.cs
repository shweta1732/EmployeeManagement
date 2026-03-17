using EmployeeManagement.Infrastructure.Data;
using EmployeeManagement.Infrastructure.Repositories;
using EmployeeManagement.Application.Interfaces;
using EmployeeManagement.Application.Services;
using EmployeeManagement.API.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Linq;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// configure services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", builder =>
    {
        builder.WithOrigins("http://localhost:80", "http://localhost", "http://localhost:4200", "http://localhost:3000")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});

// configure authentication/authorization
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    var jwt = builder.Configuration.GetSection("Jwt");
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = jwt.GetValue<string>("Issuer"),
        ValidateAudience = true,
        ValidAudience = jwt.GetValue<string>("Audience"),
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.GetValue<string>("Key"))),
        ValidateLifetime = true
    };
});

builder.Services.AddAuthorization();

// database (using in-memory provider for local development)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("EmployeeManagementDb"));

// repository and service DI
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IDepartmentRepository, EmployeeManagement.Infrastructure.Repositories.DepartmentRepository>();
builder.Services.AddScoped<IDepartmentService, EmployeeManagement.Application.Services.DepartmentService>();
// user repository for auth
builder.Services.AddScoped<IUserRepository, EmployeeManagement.Infrastructure.Repositories.UserRepository>();
// authentication services
builder.Services.AddScoped<IAuthService, EmployeeManagement.Application.Services.AuthService>();
builder.Services.AddScoped<ITokenService, EmployeeManagement.API.Services.JwtTokenService>();

// AutoMapper and FluentValidation
builder.Services.AddAutoMapper(typeof(MappingProfile));
// register validators from application assembly
builder.Services.AddValidatorsFromAssemblyContaining< EmployeeManagement.Application.Validators.EmployeeValidator >();

var app = builder.Build();

// configure middleware
// Enable Swagger in Development and Docker environments (but not Production)
if (app.Environment.IsDevelopment() || app.Environment.EnvironmentName == "Docker")
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAngularApp");
app.UseAuthentication();
app.UseAuthorization();

// seed sample users
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    var hasher = new Microsoft.AspNetCore.Identity.PasswordHasher<EmployeeManagement.Domain.Entities.User>();
    if (!context.Users.Any())
    {
        var admin = new EmployeeManagement.Domain.Entities.User
        {
            Username = "admin",
            Email = "admin@example.com",
            Role = "Admin"
        };
        admin.PasswordHash = hasher.HashPassword(admin, "admin123");
        context.Users.Add(admin);

        var user = new EmployeeManagement.Domain.Entities.User
        {
            Username = "user",
            Email = "user@example.com",
            Role = "User"
        };
        user.PasswordHash = hasher.HashPassword(user, "user123");
        context.Users.Add(user);

        context.SaveChanges();
    }
}

app.MapControllers();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
