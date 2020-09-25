using Amazon.DynamoDBv2.DocumentModel;
using MhLabs.AwsDynamoDB.Converters;
using System;
using Xunit;

namespace MhLabs.AwsDynamoDB.Tests
{
    public class DynamoEnumConverterTests
    {
        [Fact]
        public void Should_Create_Entry_From_Enum()
        {
            var dayOfWeek = DayOfWeek.Friday;
            var converter = new DynamoEnumConverter<DayOfWeek>();
            var entry = converter.ToEntry(dayOfWeek);
            Assert.Equal("Friday", (string)((Primitive)entry).Value);
        }

        [Fact]
        public void Should_Create_Enum_From_Entry()
        {
            DynamoDBEntry entry = new Primitive { Value = "Friday" };
            var converter = new DynamoEnumConverter<DayOfWeek>();
            var dayOfWeek = converter.FromEntry(entry);

            Assert.IsType<DayOfWeek>(dayOfWeek);
            Assert.Equal(DayOfWeek.Friday, dayOfWeek);
        }
    }
}
