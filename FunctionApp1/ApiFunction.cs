using FunctionApp1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace FunctionApp1
{
    public class ApiFunction
    {
        [FunctionName("ApiFunction")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest request,
                                             [Queue(Literals.messagetomom)] IAsyncCollector<MessageToMom> messageToMomCollector,
                                             ILogger logger)
        {

            logger.LogInformation("--------------New message to mom received...--------------");

            try
            {
                logger.LogInformation("Parsing message...");

                //Read Request Body
                string requestBody = await new StreamReader(request.Body).ReadToEndAsync();

                if (string.IsNullOrWhiteSpace(requestBody))
                    throw new InvalidDataException("Message body required.");

                //Map HttpRequest from fields of MessageToMom
                MessageToMom message = JsonConvert.DeserializeObject<MessageToMom>(requestBody);

                //Add Message to Queue
                logger.LogInformation("Updating queue...");
                await messageToMomCollector.AddAsync(message);

                logger.LogInformation("--------------Queue updated.--------------");
                return new OkObjectResult(message.Greeting);
            }
            catch (Exception e)
            {
                logger.LogError(e.ToString());
                return new BadRequestObjectResult(e);
            }
        }
    }
}
