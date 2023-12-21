using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Nadin.Application;
using Nadin.Domain.Enums;
using Nadin.Infrastucture;
using Nadin.Infrastucture.Middlewares;
using Nadin.WebAPI.ABAC.Handlers;
using Nadin.WebAPI.ABAC.Requirements;
using Nadin.WebAPI.CBAC.AccessContext;
using Nadin.WebAPI.CBAC.Handlers;
using SharedKernel.Middlewares;
using System.Collections.Generic;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);

//builder.Services.AddCors(c =>
//{
//    c.AddPolicy("AllowAll", options => options
//   .AllowAnyMethod()
//   .AllowAnyHeader()
//   .AllowAnyOrigin());
//});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("GenderPolicy", policy =>
        policy.Requirements.Add(new GenderRequirement(GenderType.Female)));
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("IPAccessControlPolicy", policy =>
        policy.Requirements.Add(new IPAccessControlContext(new List<string> { "127.0.0.1", "::1" })));
});

builder.Services.AddSingleton<IAuthorizationHandler, GenderRequirementHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, IPAccessControlHandler>();


builder.Services.AddApiVersioning();
builder.Services.AddControllers();
builder.Services.AddDateOnlyTimeOnlyStringConverters();
builder.Services.AddSwaggerGen(c => c.UseDateOnlyTimeOnlyStringConverters());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.Services.DbContextInitializer();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCustomExceptionHandler();

app.UseHttpsRedirection();

//app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
