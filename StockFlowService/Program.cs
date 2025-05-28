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
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();
var tobeencrypted = new
{Username = "Anirudh" , Password = "Vishalgad5@" };


var x = CryptoHelper.EncryptData(tobeencrypted);
Console.WriteLine(x);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapControllers();
app.UseHttpsRedirection();
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
