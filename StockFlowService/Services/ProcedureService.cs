using Dapper;
using StockFlowService.Data;
using System.Data;
using System.Text.Json;

namespace StockFlowService.Services
{
    public class ProcedureService
    {
        private readonly DapperContext _context;

        public ProcedureService(DapperContext context)
        {
            _context = context;
        }

        public async Task<object> CallStoredProcedureAsync(string procedureName, Dictionary<string, object> parameters)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var dynamicParams = new DynamicParameters();

                foreach (var param in parameters)
                {
                    object value = param.Value;

                    if (value is JsonElement jsonElement)
                    {
                        value = jsonElement.ValueKind switch
                        {
                            JsonValueKind.String => jsonElement.GetString(),
                            JsonValueKind.Number => jsonElement.TryGetInt32(out int intValue) ? intValue : jsonElement.GetDecimal(),
                            JsonValueKind.True => true,
                            JsonValueKind.False => false,
                            _ => jsonElement.GetRawText()
                        };
                    }

                    dynamicParams.Add($"@{param.Key}", value);
                }

                var result = await connection.QueryAsync(procedureName, dynamicParams, commandType: CommandType.StoredProcedure);
                return  result ; 
            }
            catch (Exception ex)
            {
                
                return null;
            }
        }

    }
}
