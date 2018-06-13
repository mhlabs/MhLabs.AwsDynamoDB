using System;
using System.Globalization;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;

namespace MhLabs.AwsDynamoDB.Converters
{
    public class DynamoNullableRoundtripDateTimeConverter : IPropertyConverter
    {
        public DynamoDBEntry ToEntry(object value)
        {
            DynamoDBEntry entry = new Primitive {Value = null};

            if (value != null)
                entry = new Primitive
                {
                    Value = ((DateTime) value).ToUniversalTime().ToString("o", CultureInfo.InvariantCulture)
                };

            return entry;
        }

        public object FromEntry(DynamoDBEntry entry)
        {
            var primitive = entry as Primitive;

            if (primitive == null) return null;

            var dtString = primitive.Value.ToString();
            var value = DateTime.ParseExact(dtString, "o", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);
            return value;
        }
    }
}