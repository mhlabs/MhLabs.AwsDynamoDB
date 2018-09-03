using System;
using System.Globalization;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;

namespace MhLabs.AwsDynamoDB.Converters
{
    public class DynamoEpochSecondsConverter : IPropertyConverter
    {
        public static int CreateEpochSeconds(DateTime dateTime)
        {
            var result = Amazon.Util.AWSSDKUtils.ConvertToUnixEpochSeconds(dateTime);
            return result;
        }

        public static DateTime ParseEpochSeconds(int epoch)
        {
            var result = Amazon.Util.AWSSDKUtils.ConvertFromUnixEpochSeconds(epoch);
            return result;
        }

        public DynamoDBEntry ToEntry(object value)
        {
            DynamoDBEntry entry = new Primitive { Value = null };

            if (value != null)
            {
                if (DateTime.TryParse(value.ToString(), out DateTime dt))
                {
                    entry = new Primitive
                    {
                        Value = CreateEpochSeconds(dt),
                        Type = DynamoDBEntryType.Numeric
                    };
                }
            }
            return entry;
        }

        public object FromEntry(DynamoDBEntry entry)
        {
            var primitive = entry as Primitive;

            if (primitive == null) return null;

            if (int.TryParse(primitive.Value.ToString(), out int epoch))
            {
                var value = ParseEpochSeconds(epoch).ToUniversalTime();
                return value;
            }

            return null;
        }
    }
}