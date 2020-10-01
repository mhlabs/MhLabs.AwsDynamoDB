using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using System;
using System.Globalization;

namespace MhLabs.AwsDynamoDB.Converters
{
    /// <summary>
    /// Converts a DateTime to format "yyyy-MM-dd" when saved to DynamoDB,
    /// and returned back as a DateTime.
    /// 
    /// The date will always be treated as universal time.
    /// </summary>
    public class DynamoDateConverter : IPropertyConverter
    {
        public object FromEntry(DynamoDBEntry entry)
        {
            var dateTime = entry?.AsString();
            var returnValue = DateTime.SpecifyKind(DateTime.MinValue, DateTimeKind.Utc).Date;
            if (DateTime.TryParseExact(dateTime, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime value))
            {
                returnValue = DateTime.SpecifyKind(value.Date, DateTimeKind.Utc).Date;
            }

            return returnValue;
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

            var date = (DateTime)value;
            var universalDate = DateTime.SpecifyKind(date, DateTimeKind.Utc).Date;
            return universalDate.ToString("yyyy-MM-dd");
        }
    }
}
