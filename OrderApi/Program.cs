using Microsoft.EntityFrameworkCore;
using OrderApi.DataAccess;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
});

// PostgreSql
var sqlConnectionString = builder.Configuration.GetValue<string>("PostgreSql:ConnectionString");
builder.Services.AddDbContext<PostgreSqlContext>(
    options => options.UseNpgsql(sqlConnectionString));
builder.Services.AddScoped<IDataAccessProvider, DataAccessProvider>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
