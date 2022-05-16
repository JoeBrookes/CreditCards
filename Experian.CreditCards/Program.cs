using Experian.CreditCards.Core;
using Experian.CreditCards.Core.Interfaces;
using Experian.CreditCards.Options;
using Experian.CreditCards.Repo;
using Experian.CreditCards.Repo.Interfaces;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, cfg) => cfg.ReadFrom.Configuration(ctx.Configuration));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ICardApplicantService, CardApplicantService>();
builder.Services.AddScoped<ICardDeciderEngine, CardDeciderEngine>();
builder.Services.AddScoped<ICardApplicantRepository, CardApplicantRepository>();

builder.Services.Configure<RuleEngineOptions>(builder.Configuration.GetSection("RuleEngine"));

builder.Services.AddDbContext<CreditCardContext>(options =>
              options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), 
                                    opts => opts.MigrationsAssembly("Experian.CreditCards")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
