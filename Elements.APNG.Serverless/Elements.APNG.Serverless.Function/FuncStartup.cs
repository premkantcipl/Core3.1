using Elements.APNG.Serverless.API.Extensions;
using Elements.APNG.Serverless.Function;
using Elements.APNG.Serverless.Services.EmailSender;
using Elements.APNG.Serverless.Services.Interfaces;
using Elements.APNG.Serverless.Services.MailTemplateServices;
using Elements.APNG.Serverless.Services.PayrollServices;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;

[assembly: FunctionsStartup(typeof(FuncStartup))]
namespace Elements.APNG.Serverless.Function
{
    public class FuncStartup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            // Example, inject a service with a one-liner.
            builder.Services.AddScoped<IInvoiceMailTemplateService, InvoiceMailTemplateService>();
            builder.Services.AddScoped<IEmailSender, SendGridEmailSender>();
            builder.Services.AddScoped<IPayrollService, PayrollServices>();
            builder.AddApiDependencies();
        }
    }
}
