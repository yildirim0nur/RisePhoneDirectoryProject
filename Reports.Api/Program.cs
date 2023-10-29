using Contacts.Application.Service.Contact.Interface;
using Contacts.Constants;
using Contacts.Application.Service.Contact.Interface;
using Contacts.Constants;
using OfficeOpenXml;
using Reports.Api;
using Reports.Application;
using Reports.Persistence;
using Reports.Persistence.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IContactReportSvc, ContactReportSvc>();
builder.Services.AddScoped<IDbReportContext, DbReportContext>();
builder.Services.AddPersistenceService(builder.Configuration);
builder.Services.Configure<ContactSettings>(builder.Configuration.GetSection("Options"));
ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRabbitMq();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
