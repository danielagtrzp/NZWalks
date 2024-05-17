using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Mappers;
using NZWalks.API.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Injecting the connectionstring of the DB to the DI Container ->here injecting the dbcontext to the container
builder.Services.AddDbContext<NZWalksDbContext>(options=>options.UseSqlServer(builder.Configuration.GetConnectionString("NZWalksConnectionString")));
//Injecting new repository with an SQL implementation to be able to used it in the controller
builder.Services.AddScoped<IRegionRepository, SQLRegionRepository>();
//Adding the automapper class to the container
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

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
