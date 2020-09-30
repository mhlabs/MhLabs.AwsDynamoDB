using Amazon.DynamoDBv2.DocumentModel;
using MhLabs.AwsDynamoDB.Converters;
using System;
using Xunit;

namespace MhLabs.AwsDynamoDB.Tests
{
    public class DynamoDateConverterTests
    {
        [Fact]
        public void Should_Create_Null_Date_From_Nullable()
        {
            // arrange
            DateTime? date = null;
            var converter = new DynamoDateConverter();

            // act
            var entry = converter.ToEntry(date);

            // assert
            Assert.IsType<DynamoDBNull>(entry);
        }

        [Fact]
        public void Should_Throw_If_Not_DateTime()
        {
            // arrange
            var value = 1;
            var converter = new DynamoDateConverter();
            var expected = @"The given value is not a DateTime nor a Nullable<DateTime>.
Parameter name: value";

            // act
            var exception = Assert.Throws<ArgumentException>(() => converter.ToEntry(value));

            // assert
            Assert.Equal(expected, exception.Message);
        }

        [Fact]
        public void Should_Create_Entry_From_DateTime()
        {
            // arrange
            var date = new DateTime(2020, 1, 1, 12, 1, 1);
            var converter = new DynamoDateConverter();
            var expected = "2020-01-01";

            // act
            var entry = converter.ToEntry(date);
            
            // assert
            Assert.Equal(expected, entry.AsString());
        }

        [Fact]
        public void Should_Create_DateTime_From_Entry()
        {
            // arrange
            DynamoDBEntry entry = new Primitive { Value = "2020-01-01" };
            var converter = new DynamoDateConverter();
            var expected = new DateTime(2020, 1, 1);

            // act
            var date = converter.FromEntry(entry);

            // assert
            Assert.Equal(expected, date);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("20-1-1")]
        [InlineData("20-01-01")]
        [InlineData("01/12/20")]
        public void Should_Return_DateTime_Minvalue_From_Null(string input)
        {
            // arrange
            DynamoDBEntry entry = new Primitive { Value = input };
            var converter = new DynamoDateConverter();
            var expected = DateTime.MinValue;

            // act
            var date = converter.FromEntry(entry);

            // assert
            Assert.Equal(expected, date);
        }
    }
}
