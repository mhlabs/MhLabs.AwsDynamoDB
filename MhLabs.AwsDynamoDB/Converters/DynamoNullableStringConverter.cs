using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;

namespace MhLabs.AwsDynamoDB.Converters
{
    public class DynamoNullableStringConverter : IPropertyConverter
    {
        public DynamoDBEntry ToEntry(object value)
        {
            DynamoDBEntry entry = new Primitive { Value = null };

            if (value != null)
                entry = new Primitive
                {
                    Value = value
                };

            return entry;
        }

        public object FromEntry(DynamoDBEntry entry)
        {
            var primitive = entry as Primitive;

            return primitive?.Value.ToString();
        }
    }
}
