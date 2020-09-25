using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using System;

namespace MhLabs.AwsDynamoDB.Converters
{
    public class DynamoEnumConverter<TEnum> : IPropertyConverter
    {
        public object FromEntry(DynamoDBEntry entry)
        {
            string valueAsString = entry.AsString();
            TEnum valueAsEnum = (TEnum)Enum.Parse(typeof(TEnum), valueAsString);
            return valueAsEnum;
        }

        public DynamoDBEntry ToEntry(object value)
        {
            string valueAsString = value.ToString();
            DynamoDBEntry entry = new Primitive(valueAsString);
            return entry;
        }
    }
}
