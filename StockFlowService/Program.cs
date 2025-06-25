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
{
    TransferChildNumber = "TRN-CH-20250623-001",
    User = "warehouse-user-12",
    ProductSerialNumber = "['SN-TRF-3001', 'SN-TRF-3002', 'SN-TRF-3003']",
    Remark= "Reallocation for zone B optimization",
    UserId =101
};


var x = CryptoHelper.EncryptData(tobeencrypted);

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
