using Data;
using Microsoft.EntityFrameworkCore;
using Service.Extensions;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDatabaseContext(builder.Configuration).AddConfig().AddMyDependencyGroup();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
