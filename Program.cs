using Amazon.SQS;
using System.Linq.Expressions;

const string accessKey = "!!!!!!!!!!!!!!";
const string secretAccessKey = "!!!!!!!!!!!!!!!!";
const string queueName = "!!!!!!!!!!!!!!!!!";

var client = new Amazon.SQS.AmazonSQSClient(
    accessKey, secretAccessKey,
    new AmazonSQSConfig
    {
        ServiceURL = "https://message-queue.api.cloud.yandex.net",
        AuthenticationRegion = "ru-central1",
        MaxErrorRetry = 2 // чтобы не ждать многократных попыток неудачного вызова
    });

// Пример успешного вызова API
var successMethodResponse = await client.GetQueueUrlAsync(queueName);
if (successMethodResponse.HttpStatusCode == System.Net.HttpStatusCode.OK)
{
    // Пример вызова с исключением "CRC value returned with response does not match the computed CRC value for the returned response body."
    try
    {
        var errorMethodResponse = await client.SetQueueAttributesAsync(
            successMethodResponse.QueueUrl,
            new Dictionary<string, string> {
                { "VisibilityTimeout", "300" }
            });
    }
    catch (Exception e)
    {
        throw e;
    }
}