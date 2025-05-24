using StockFlowService.Data;
using StockFlowService.Helpers;
using StockFlowService.Services;
using System.Security.Cryptography.Xml;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ProcedureService>();
builder.Services.AddSingleton<DapperContext>();
var app = builder.Build();
var tobeencrypted = new
{
    ProductSerialNumber = "PSN-983429-A1",
    ProductName = "Industrial Fan",
    Count = 12,
    Name = "Anirudh Pandit",
    Location = "Warehouse 7 - Sector B"
};


var x = CryptoHelper.EncryptData(tobeencrypted);

Console.WriteLine("encrypted", CryptoHelper.EncryptData(tobeencrypted));
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
