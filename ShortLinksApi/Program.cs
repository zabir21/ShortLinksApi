using BasicAuthentication.Authentication;
using DapperRelization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using ShortLinksApi.BLL.Services;
using ShortLinksApi.BLL.Services.Interfaces;
using ShortLinksApi.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.ConfigureDal();

builder.Services.Configure<AuthorizationBasic>(builder.Configuration.GetSection(nameof(AuthorizationBasic)));

builder.Services.AddScoped<IShortLinkService, ShortLinkService>();
builder.Services.AddScoped<ITagService, TagService>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers()
.AddNewtonsoftJson(options =>
{
    options.SerializerSettings.Converters.Add(new StringEnumConverter());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("basic",
        new OpenApiSecurityScheme()
        {
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "basic",
            In = ParameterLocation.Header,
            Description = "Basic authorization header"
        });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference= new OpenApiReference
                {
                    Type =ReferenceType.SecurityScheme,
                    Id= "basic"
                }
            },
            new string[]{}
        }
    });
});

builder.Services.AddAuthentication("BasicAuthentication")
       .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

var app = builder.Build();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

////////////////////////////
var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
await using var scope = scopeFactory.CreateAsyncScope();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAllOrigins");

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();

public partial class Program { }
