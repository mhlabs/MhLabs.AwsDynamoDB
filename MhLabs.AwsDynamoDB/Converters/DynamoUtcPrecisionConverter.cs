namespace MhLabs.AwsDynamoDB.Converters
{
    using System;
    using System.Globalization;
    using Amazon.DynamoDBv2.DataModel;
    using Amazon.DynamoDBv2.DocumentModel;

    public class DynamoUtcPrecisionConverter : IPropertyConverter
    {
        private const string format = "yyyy-MM-ddThh:mm:ss.fffZ";
        public DynamoDBEntry ToEntry(object value)
        {
            if (value == null)
                return new DynamoDBNull();
            return !(value is DateTime dt) ? new DynamoDBNull() :dt.ToString(format);
        }

        public object FromEntry(DynamoDBEntry entry)
        {
            if (!(entry is Primitive primitive))
            {
                return null;
            }

            if (primitive.Type != DynamoDBEntryType.String)
            {
                throw new InvalidCastException($"DateTime cannot be converted as its type is {primitive.Type} with a value of {primitive.Value}");
            }

            return DateTime.ParseExact(primitive.AsString(), format, CultureInfo.InvariantCulture).ToUniversalTime();
        }
    }
}