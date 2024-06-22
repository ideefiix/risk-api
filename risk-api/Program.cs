using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using risk_api.DAL.DBContext;
using risk_api.DAL.Processes;
using risk_api.Jobs;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(corsBuilder =>
    {
        corsBuilder
            .AllowAnyOrigin() // Allow requests from any origin
            .AllowAnyMethod() // Allow any HTTP method (GET, POST, PUT, DELETE, etc.)
            .AllowAnyHeader(); // Allow any header in the request
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var securityScheme = new OpenApiSecurityScheme
    {
        Scheme = "Bearer",
        BearerFormat = "JWT",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Description = "Bearer token",
    };

    c.AddSecurityDefinition("oauth2", securityScheme);

    var securityRequirement = new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "oauth2"
                }
            },
            new List<string>()
        },
    };

    c.AddSecurityRequirement(securityRequirement);
});

string dbConnectionString;
string jwtIssuer;
string jwtKey;

if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
{
    Log.Information("Running in PROD environment");
    dbConnectionString = Environment.GetEnvironmentVariable("DB_CON_STR") ?? throw new InvalidOperationException();
    jwtIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER") ?? throw new InvalidOperationException();
    jwtKey = Environment.GetEnvironmentVariable("JWT_KEY") ?? throw new InvalidOperationException();
    Log.Information("ENV:ConnectionString " + dbConnectionString);
    Log.Information("ENV:jwtIssuer " + jwtIssuer);
    Log.Information("ENV:jwtkey " + jwtKey);
}
else
{
    Log.Information("Running in dev environment");
    dbConnectionString = builder.Configuration.GetConnectionString("DatabaseContext");
    jwtIssuer = builder.Configuration.GetSection("Jwt:Issuer").Get<string>();
    jwtKey = builder.Configuration.GetSection("Jwt:Key").Get<string>();
    Environment.SetEnvironmentVariable("DB_CON_STR", dbConnectionString);
    Environment.SetEnvironmentVariable("JWT_ISSUER", jwtIssuer);
    Environment.SetEnvironmentVariable("JWT_KEY", jwtKey);
}

builder.Services.AddDbContextFactory<DatabaseContext>(options =>
    options.UseNpgsql(dbConnectionString));

builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DatabaseContext") ??
                      throw new InvalidOperationException("Connection string 'DatabaseContext' not found.")));


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtIssuer,
        ValidAudience = jwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };
});
builder.Services.AddHostedService<IncomeService>();

builder.Services.AddScoped<PlayerProcess>();
builder.Services.AddScoped<TerritoryProcess>();
builder.Services.AddScoped<BuildingProcess>();
builder.Services.AddScoped<ServiceProcess>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<DatabaseContext>();
    //context.Database.EnsureDeleted();
    context.Database.EnsureCreated(); // TODO add migrations
    DBseeding.Initialize(context);
}

app.UseHttpsRedirection();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error");
}

app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();