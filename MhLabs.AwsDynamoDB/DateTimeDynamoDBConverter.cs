using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace MhLabs.AwsDynamoDB
{
    public class DateTimeDynamoDBConverter : IPropertyConverter
    {
        public object FromEntry(DynamoDBEntry entry)
        {
            var primitive = entry as Primitive;
            if (primitive == null)
            {
                return null;
            }

            if (primitive.Type != DynamoDBEntryType.String)
            {
                throw new InvalidCastException(string.Format("DateTime cannot be converted as its type is {0} with a value of {1}"
                    , primitive.Type, primitive.Value));
            }

            return DateTime.ParseExact(primitive.AsString(), "u", CultureInfo.InvariantCulture);
        }

        public DynamoDBEntry ToEntry(object value)
        {
            var dt = value as DateTime?;
            return !dt.HasValue ? null : new Primitive(dt.Value.ToString("u"));
        }
    }
}
