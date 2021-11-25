using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;

namespace AzureDemo.Host.Models
{
    public class IndexViewModel
    {
        public IndexViewModel()
        {
            /*
            var accessKey = "AKIAZPQ5FUBMI5H2KANC";// Get access key from a secure store
            var secretKey = "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";// Get secret key from a secure store
            var s3Client = AWSClientFactory.CreateAmazonS3Client(accessKey, secretKey, RegionEndpoint.USWest2);
            */
            
        }

        public string RandomGuy
        {
            get
            {
                var accessKey = Environment.GetEnvironmentVariable("AWS_ACCESSKEY");
                var secretKey = Environment.GetEnvironmentVariable("AWS_SECRETKEY");
                //var region = Environment.GetEnvironmentVariable("AWS_REGION");
                //var accessKey = "AKIAZPQ5FUBMI5H2KANC";// Get access key from a secure store
                //var secretKey = "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";// Get secret key from a secure store

                AmazonDynamoDBClient client = new AmazonDynamoDBClient(accessKey,secretKey, RegionEndpoint.EUCentral1);
                string tableName = "AwsDemo";

                var request = new GetItemRequest
                {
                    TableName = tableName,
                    Key = new Dictionary<string, AttributeValue>() { 
                        { "PartitionKey", new AttributeValue { S = "key1" } },
                        { "SortKey", new AttributeValue { S = "sort1" } }
                    }
                };

                var response = client.GetItemAsync(request);

                // Check the response.
                var result = response.Result;
                var attributeMap = result.Item; // Attribute list in the response.

                attributeMap.TryGetValue("Name", out var name);
                attributeMap.TryGetValue("Surname", out var surname);

                return string.Concat(name.S, " ", surname.S);
            }
        }
    }
}
