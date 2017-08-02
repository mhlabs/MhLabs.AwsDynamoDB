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

        public async Task DeleteAsync(T obj)
        {
            using (var dbContext = new DynamoDBContext(client))
            {
                await dbContext.DeleteAsync(obj);
            }
        }

        public async Task<List<T>> FromQueryAsync(QueryOperationConfig queryOperationConfig, DynamoDBOperationConfig operationConfig = null)
        {
            using (var dbcontext = new DynamoDBContext(client))
            {
                return await dbcontext.FromQueryAsync<T>(queryOperationConfig, operationConfig: operationConfig).GetNextSetAsync();
            }
        }

        public async Task<List<T>> QueryAsync(object hashKeyValue)
        {
            using (var dbcontext = new DynamoDBContext(client))
            {
                return await dbcontext.QueryAsync<T>(hashKeyValue).GetNextSetAsync();
            }
        }

        public async Task SaveAsync(T obj)
        {
            using (var dbContext = new DynamoDBContext(client))
            {
                await dbContext.SaveAsync(obj);
            }
        }

        public async Task<List<T>> ScanAsync(IEnumerable<ScanCondition> conditions)
        {
            using (var dbcontext = new DynamoDBContext(client))
            {
                return await dbcontext.ScanAsync<T>(conditions).GetNextSetAsync();
            }
        }
    }
}
