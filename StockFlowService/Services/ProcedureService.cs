using Dapper;
using StockFlowService.Data;
using System.Data;

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
            using var connection = _context.CreateConnection();
            var dynamicParams = new DynamicParameters();

            foreach (var param in parameters)
                dynamicParams.Add($"@{param.Key}", param.Value);

            var result = await connection.QueryAsync(procedureName, dynamicParams, commandType: CommandType.StoredProcedure);
            return result.Any() ? result.First() : new { message = "No data found" };
        }
    }
}
