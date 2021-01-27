using FunctionApp1.DTO;
using FunctionApp1.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace FunctionApp1
{
    public class LogFormLetterToStorage
    {
        [FunctionName(nameof(LogFormLetterToStorage))]
        public async Task Run([QueueTrigger(Literals.outputletter, Connection = "")] FormLetter formLetter,
                              [Table(Literals.letters)] IAsyncCollector<LetterEntity> letterTableCollector,
                              ILogger logger)
        {
            try
            {
                logger.LogInformation($"--------------Queue trigger function processed: {formLetter.Heading}...--------------");

                //Map FormLetter message to LetterEntity type and save to table storage
                await letterTableCollector.AddAsync(new LetterEntity(formLetter));

                logger.LogInformation("--------------Letter Queue updated.--------------");
            }
            catch (Exception e)
            {
                logger.LogError(e.ToString());
            }
        }
    }

}
