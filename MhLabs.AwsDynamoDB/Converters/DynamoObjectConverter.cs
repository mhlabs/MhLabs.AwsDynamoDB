using System.Linq;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;

namespace MhLabs.AwsDynamoDB.Converters
{
    public class DynamoObjectConverter<TEntity>: IPropertyConverter where TEntity: class
    {
        private readonly IDynamoDBContext _dbContext;

        public DynamoObjectConverter(IDynamoDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public DynamoDBEntry ToEntry(object value)
        {
            var entity = value as TEntity;
            return entity == null ? null : _dbContext.ToDocument(entity);
        }

        public object FromEntry(DynamoDBEntry entry)
        {
            var document = entry.AsDocument();

           return _dbContext.FromDocument<TEntity>(document);
        }
    }
}
