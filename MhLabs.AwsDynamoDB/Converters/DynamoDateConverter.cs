using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using System;
using System.Globalization;

namespace MhLabs.AwsDynamoDB.Converters
{
    public class DynamoDateConverter : IPropertyConverter
    {
        public object FromEntry(DynamoDBEntry entry)
        {
            var dateTime = entry?.AsString();
            if (string.IsNullOrEmpty(dateTime))
            {
                return DateTime.MinValue;
            }

            if (DateTime.TryParseExact(dateTime, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime value))
            {
                return value;
            }

            return DateTime.MinValue;
        }

        public DynamoDBEntry ToEntry(object value)
        {
            if (value == null)
            {
                return new DynamoDBNull();
            }

            if (value.GetType() != typeof(DateTime) && value.GetType() != typeof(DateTime?))
            {
                throw new ArgumentException("The given value is not a DateTime nor a Nullable<DateTime>.", nameof(value));
            }

            return ((DateTime)value).ToString("yyyy-MM-dd");
        }
    }
}
