using FunctionApp1.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace FunctionApp1
{
    public class CalculateDatesAndAmountsFunction
    {
        [FunctionName(nameof(CalculateDatesAndAmountsFunction))]
        public async Task Run([QueueTrigger(Literals.messagetomom, Connection = "")] MessageToMom messageToMom,
                              [Queue(Literals.outputletter)] IAsyncCollector<FormLetter> outputLetterCollector,
                              ILogger logger)
        {
            logger.LogInformation($"--------------Queue trigger function processed...--------------");
            logger.LogInformation($"{messageToMom.Greeting} {messageToMom.HowMuch} {messageToMom.HowSoon}");

            try
            {
                //Create Letter Composer to create the Form Letter based from Message To Mom Values
                //Use Loan Calculator to calculate expected date and likelihood 
                //    passing number of processing days and maximum loanable amount
                await outputLetterCollector.AddAsync(new LetterComposer(new LoanCalculator(10, 10000), messageToMom).Compose());
                logger.LogInformation("--------------Letter Queue updated.--------------");
            }
            catch (Exception e)
            {
                logger.LogError(e.ToString());
            }
        }
    }
}
