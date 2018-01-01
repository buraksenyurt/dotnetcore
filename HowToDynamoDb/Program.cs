using System;
using Amazon.DynamoDBv2;

namespace HowToDynamoDb
{
    class Program
    {
        static void Main(string[] args)
        {
            var utl = new DynamoDBUtility();
            var findingQuote=utl.FindQuoteByID(1001);
            Console.WriteLine($"{findingQuote.QuoteInfo.Character}\n{findingQuote.QuoteInfo.Text}");
            /*            
            Quote quote=new Quote{
                QuoteID=1001,
                QuoteInfo=new QuoteInfo{
                    Character="Cortana",
                    Game="Halo 2",
                    Like=192834,
                    Text="Child of my enemy, why have you come? I offer no forgiveness, a father's sins, passed to his son."
                }
            };
            utl.InsertQuote(quote);
            var findingQuote=utl.FindQuoteByID(1001);
            Console.WriteLine($"{findingQuote.QuoteInfo.Character}\n{findingQuote.QuoteInfo.Text}");
            */
            
            /*
            utl.CreateTable("GameQuotes","QuoteID", ScalarAttributeType.N);
            utl.CreateTable("Players","Nickname", ScalarAttributeType.S);

            var tableNames = utl.GetTables();
            foreach (var tableName in tableNames)
            {
                Console.WriteLine($"{tableName}");
            }
            */
        }
    }    
}