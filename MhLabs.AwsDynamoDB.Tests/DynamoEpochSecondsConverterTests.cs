using System;
using Amazon.DynamoDBv2.DocumentModel;
using MhLabs.AwsDynamoDB.Converters;
using Xunit;

namespace MhLabs.AwsDynamoDB.Tests
{
    public class DynamoEpochSecondsConverterTests
    {
        [Fact]
        public void Should_Create_Entry_From_DateTime()
        {
            var dt = DateTime.Today.AddDays(1);
            var converter = new DynamoEpochSecondsConverter();
            var entry = converter.ToEntry(dt);

            Assert.NotNull(entry);
            var epochSeconds = (int) ((Primitive) entry).Value;
            Assert.Equal(dt, DynamoEpochSecondsConverter.ParseEpochSeconds(epochSeconds));
        }
    }
}
