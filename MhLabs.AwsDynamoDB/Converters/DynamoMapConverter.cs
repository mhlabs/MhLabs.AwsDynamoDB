using System;
using System.Collections.Generic;
using System.Linq;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;

namespace MhLabs.AwsDynamoDB.Converters
{
    public class DynamoMapConverter<TEntity> : IPropertyConverter where TEntity : class
    {
        private readonly IDynamoDBContext _dbContext;

        public DynamoMapConverter(IDynamoDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public DynamoDBEntry ToEntry(object value)
        {
            throw new NotImplementedException();
        }

        public object FromEntry(DynamoDBEntry entry)
        {
            var document = entry.AsDocument();
            if (document == null) return null;

            var result = new Dictionary<string, TEntity>();

            document.Keys.ToList().ForEach(key =>
            {
                var item = document[key].AsDocument();
                var entity = _dbContext.FromDocument<TEntity>(item);

                result.Add(key, entity);
            });

            return result;
        }
    }
}