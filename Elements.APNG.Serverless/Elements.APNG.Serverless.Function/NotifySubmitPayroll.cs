using Elements.APNG.Serverless.Services;
using Elements.APNG.Serverless.Services.Interfaces;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Elements.APNG.Serverless.Function
{
    public class NotifySubmitPayroll
    {
        readonly IPayrollService _payrollService;
        public NotifySubmitPayroll(IPayrollService payrollService)
        {
            _payrollService = payrollService;
        }

        [FunctionName("NotifySubmitPayroll")]
        public async Task Run([TimerTrigger("%CRON_EXPRESSION%", RunOnStartup = true)] TimerInfo myTimer, ILogger log)
        {
            if(EnvironmentVariables.TurnOn())
            {
                await _payrollService.NotifySubmitPayrollAsync();
                log.LogInformation($"Submitted Payroll Reminder: {DateTime.Now}");
            }
            else
            {
                log.LogInformation($"Payroll Reminder Service is turned off! ");
            }

        }
    }
}
