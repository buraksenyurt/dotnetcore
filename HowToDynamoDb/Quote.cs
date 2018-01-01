using Amazon.DynamoDBv2.DataModel;

namespace HowToDynamoDb
{
    [DynamoDBTable("GameQuotes")]
    public class Quote
    {
        [DynamoDBHashKey]
        public int QuoteID { get; set; }
        public QuoteInfo QuoteInfo { get; set; }
    }
    public class QuoteInfo
    {
        public string Character { get; set; }
        public int Like { get; set; }
        public string Game { get; set; }
        public string Text { get; set; }
    }
}