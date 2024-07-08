using Cards.API.Extensions;
using Cards.Domain.Entities;
using Cards.Domain.Extensions;
using Cards.Infrastructure.Extensions;
using Cards.Infrastructure;
using FluentValidation.AspNetCore;
using Cards.Cache.Extensions;
using Cards.Cache.Healthchecks;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using Cards.Core.WebApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>c.EnableAnnotations());

builder.Services.AddControllers().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddRepositories();
builder.Services.AddServices();
builder.Services.AddCaching(builder.Configuration);
builder.Services.AddApplicationDbContext(builder.Configuration.GetSection("ConnectionStrings:DefaultConnection").Value!);

builder.Services
    .AddIdentity<ApplicationUser, IdentityRole>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.User.RequireUniqueEmail = true;
        options.Password.RequireDigit = false;
        options.Password.RequiredLength = 6;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();

builder.Services.AddTokenAuthentication(builder.Configuration);

builder.Services.AddHealthChecks()
                .AddCheck<RedisCacheHealthCheck>("cache_health_check")
                .AddSqlServer(builder.Configuration.GetSection("ConnectionStrings:DefaultConnection").Value!);

builder.Services.AddControllers();

var app = builder.Build();

app.UseRequestLocalization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<RetryPolicyMiddleware>();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
