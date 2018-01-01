using System;
using System.Linq;
using System.Collections.Generic;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Amazon.Runtime;

namespace HowToDynamoDb
{
    class DynamoDBUtility
    {
        AmazonDynamoDBClient aws;
        public DynamoDBUtility()
        {
            var myCredentials = new BasicAWSCredentials("sizin access key id", "sizin secret access key değeri");
            aws = new AmazonDynamoDBClient(myCredentials, RegionEndpoint.USEast2);
        }
        public List<string> GetTables()
        {
            var response = aws.ListTablesAsync();
            return response.Result.TableNames;
        }

        public void CreateTable(string tableName, string partionKeyName, ScalarAttributeType partitionKeyType)
        {
            var tableResponse = GetTables();
            if (!tableResponse.Contains(tableName))
            {
                var response = aws.CreateTableAsync(new CreateTableRequest
                {
                    TableName = tableName,
                    KeySchema = new List<KeySchemaElement>
                    {
                        new KeySchemaElement
                        {
                            AttributeName = partionKeyName,
                            KeyType = KeyType.HASH
                        }
                    },
                    AttributeDefinitions = new List<AttributeDefinition>
                    {
                        new AttributeDefinition {
                            AttributeName = partionKeyName,
                            AttributeType=partitionKeyType
                        }
                    },
                    ProvisionedThroughput = new ProvisionedThroughput
                    {
                        ReadCapacityUnits = 3,
                        WriteCapacityUnits = 3
                    },
                });
                Console.WriteLine($"HTTP Response : {response.Result.HttpStatusCode}");
            }
            else
            {
                Console.WriteLine($"{tableName} isimli tablo zaten var");
            }
        }

        public void InsertQuote(Quote quote)
        {
            var context = new DynamoDBContext(aws);           
            context.SaveAsync<Quote>(quote).Wait();            
        }

        public Quote FindQuoteByID(int quoteID)
        {
            var context = new DynamoDBContext(aws);  
            List<ScanCondition> queryConditions = new List<ScanCondition>();
            queryConditions.Add(new ScanCondition("QuoteID", ScanOperator.Equal, quoteID));
            var queryResult = context.ScanAsync<Quote>(queryConditions).GetRemainingAsync();
            return queryResult.Result.FirstOrDefault();
        }
    }
}