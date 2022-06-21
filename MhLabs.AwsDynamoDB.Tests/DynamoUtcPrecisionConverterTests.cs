namespace MhLabs.AwsDynamoDB.Tests
{
    using System;
    using Amazon.DynamoDBv2.DocumentModel;
    using Converters;
    using Xunit;

    public class DynamoUtcPrecisionConverterTests
    {
        [Fact]
        public void Should_Create_Null_Date_From_Nullable()
        {
            // arrange
            DateTime? date = null;
            var converter = new DynamoUtcPrecisionConverter();

            // act
            var entry = converter.ToEntry(date);

            // assert
            Assert.IsType<DynamoDBNull>(entry);
        }
        
        [Fact]
        public void Should_Treat_DateTime_As_Utc()
        {
            // arrange
            var date = new DateTime(2022, 06, 15,07,27,18,419, DateTimeKind.Utc);
            var converter = new DynamoUtcPrecisionConverter();
            var expected = "2022-06-15T07:27:18.419Z";

            // act
            var entry = converter.ToEntry(date);

            // assert
            Assert.Equal(expected, entry.AsString());
        }
        
        [Fact]
        public void Should_Create_DateTime_From_Entry()
        {
            // arrange
            DynamoDBEntry entry = new Primitive { Value = "2022-06-15T07:27:18.419Z" };
            var converter = new DynamoUtcPrecisionConverter();
            var expected = new DateTime(2022, 06, 15,07,27,18,419, DateTimeKind.Utc);

            // act
            var date = converter.FromEntry(entry);

            // assert
            Assert.Equal(expected, date);
        }
    }
}