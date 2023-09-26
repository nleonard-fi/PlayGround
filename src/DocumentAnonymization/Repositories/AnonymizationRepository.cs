using Dapper;
using System.Data;
using System.Data.SqlClient;
using DocumentAnonymization.Models;
using DocumentAnonymization.Extensions;

namespace DocumentAnonymization.Repositories
{
    public class AnonymizationRepository : IAnonymizationRepository
    {
        private readonly ILogger<AnonymizationRepository> _logger;
        private readonly string _dbConnString;
        private readonly int _maxNumberOfRetries;

        public AnonymizationRepository(IConfiguration configuration, ILogger<AnonymizationRepository> logger)
        {
            _logger = logger;
            _dbConnString = configuration.GetConnectionString("EcmAppsDbConnection");
            _maxNumberOfRetries = configuration.GetValue<int>("MaxNumberOfRetriesOnDbTransientErrors");
        }

        public async Task<Guid> InsertAnonymizationRequest(AnonymizationRequestDto anonymizationRequest)
        {
            _logger.LogInformation("Attempting to add anonymization request to the repository. CorrelationId: {CorrelationId}", anonymizationRequest.CorrelationId);

            const string sprocName = "sfdgcspInsertAnonymizationRequest";
            
            var sqlParams = new DynamicParameters();
            //Add input parameters.
            sqlParams.Add("@errorMsg", value: null, DbType.AnsiString, ParameterDirection.Output, size: 500);
            sqlParams.Add("@returnVal", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
            //throw new NotImplementedException();
            using IDbConnection dbConn = new SqlConnection(_dbConnString);
            
            dbConn.Open();
            await dbConn.ExecuteAsyncWithRetry(sprocName, sqlParams, commandType: CommandType.StoredProcedure, maxRetries: _maxNumberOfRetries);

            int storedProcReturnVal = sqlParams.Get<int>("@returnVal");

            if(storedProcReturnVal < 0)
            {
                var errorMsg = sqlParams.Get<string>("@errorMsg");
                if(!string.IsNullOrEmpty(errorMsg))
                {
                    _logger.LogError("Error occurred in the stored procedure {StoredProcedure}. CorrelationId: {CorrelationId}. Error Message: {Error}",
                        sprocName, anonymizationRequest.CorrelationId, errorMsg);
                    throw new ApplicationException($"Error in the stored procedure {sprocName}. Error Message: {errorMsg}");
                }
            }

            return anonymizationRequest.CorrelationId;
        }
    }
}
