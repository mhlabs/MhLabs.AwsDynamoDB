using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MhLabs.AwsDynamoDB
{
    public class DynamoDBRepository<T>
    {
        protected readonly IAmazonDynamoDB client;

        public DynamoDBRepository(IAmazonDynamoDB client = null)
        {
            this.client = client ?? new AmazonDynamoDBClient();
        }

        public async Task DeleteAsync(T obj, string overrideTableName = null)
        {
            using (var dbContext = new DynamoDBContext(client))
            {
                if (!string.IsNullOrEmpty(overrideTableName))
                    await dbContext.DeleteAsync(obj, new DynamoDBOperationConfig { OverrideTableName = overrideTableName });
                else
                    await dbContext.DeleteAsync(obj);
            }
        }

        public async Task<List<T>> FromQueryAsync(QueryOperationConfig queryOperationConfig, string overrideTableName = null)
        {
            using (var dbcontext = new DynamoDBContext(client))
            {
                if (!string.IsNullOrEmpty(overrideTableName))
                    return await dbcontext.FromQueryAsync<T>(queryOperationConfig, new DynamoDBOperationConfig { OverrideTableName = overrideTableName }).GetNextSetAsync();
                else
                    return await dbcontext.FromQueryAsync<T>(queryOperationConfig).GetNextSetAsync();
            }
        }

        public async Task<List<T>> QueryAsync(object hashKeyValue, string overrideTableName = null)
        {
            using (var dbcontext = new DynamoDBContext(client))
            {
                if (!string.IsNullOrEmpty(overrideTableName))
                    return await dbcontext.QueryAsync<T>(hashKeyValue, new DynamoDBOperationConfig { OverrideTableName = overrideTableName }).GetNextSetAsync();
                else
                    return await dbcontext.QueryAsync<T>(hashKeyValue).GetNextSetAsync();
            }
        }

        public async Task SaveAsync(T obj, string overrideTableName = null)
        {
            using (var dbContext = new DynamoDBContext(client))
            {
                if (!string.IsNullOrEmpty(overrideTableName))
                    await dbContext.SaveAsync(obj, new DynamoDBOperationConfig { OverrideTableName = overrideTableName });
                else
                    await dbContext.SaveAsync(obj);
            }
        }

        public async Task<List<T>> ScanAsync(IEnumerable<ScanCondition> conditions, string overrideTableName = null)
        {
            using (var dbcontext = new DynamoDBContext(client))
            {
                if (!string.IsNullOrEmpty(overrideTableName))
                    return await dbcontext.ScanAsync<T>(conditions, new DynamoDBOperationConfig { OverrideTableName = overrideTableName }).GetNextSetAsync();
                else
                    return await dbcontext.ScanAsync<T>(conditions).GetNextSetAsync();
            }
        }
    }
}
