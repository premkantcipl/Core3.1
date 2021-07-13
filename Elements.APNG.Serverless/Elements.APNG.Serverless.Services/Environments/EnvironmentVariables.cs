using Elements.APNG.Serverless.Models.ConfigurationModel;
using Elements.APNG.Serverless.Models.Model;
using Elements.APNG.Serverless.Services.Exceptions;
using System;
using System.Collections.Generic;

namespace Elements.APNG.Serverless.Services
{
    public static class EnvironmentVariables
    {
        public static MicroServiceConfig GetPayrollBaseUrl()
        {
            string BaseUsrl = Environment.GetEnvironmentVariable("PAYROLL_BASE_URL");
            if (string.IsNullOrWhiteSpace(BaseUsrl)) ThrowEnvironmentVariableMissingException("PAYROLL_BASE_URL");
            return new MicroServiceConfig
            {
                BaseURL = BaseUsrl
            };
        }

        public static EmailConfiguration GetEmailConfigration()
        {
            string fromEmail = Environment.GetEnvironmentVariable("EMAIL_FROM");
            string sendGridKey = Environment.GetEnvironmentVariable("EMAIL_SENDGRID_API_KEY");
            string emailTemplateVersion = Environment.GetEnvironmentVariable("EMAIL_TEMPLATE_VERSION");

            if (string.IsNullOrWhiteSpace(fromEmail)) ThrowEnvironmentVariableMissingException("EMAIL_FROM");
            if (string.IsNullOrWhiteSpace(sendGridKey)) ThrowEnvironmentVariableMissingException("EMAIL_SENDGRID_API_KEY");
            if (string.IsNullOrWhiteSpace(emailTemplateVersion)) ThrowEnvironmentVariableMissingException("EMAIL_TEMPLATE_VERSION");
            return new EmailConfiguration
            {
                From = fromEmail,
                MailTemplateVersion = emailTemplateVersion,
                SendGridApiKey = sendGridKey,
            };
        }

        public static List<ContainerInfo> GetCosmosContainers()
        {

            string Container_Customer_Name = Environment.GetEnvironmentVariable("Container_Customer_Name");
            string Container_Customer_PartitionKey = Environment.GetEnvironmentVariable("Container_Customer_PartitionKey");

            string Container_PayrollCalendar_Name = Environment.GetEnvironmentVariable("Container_PayrollCalendar_Name");
            string Container_PayrollCalendar_PartitionKey = Environment.GetEnvironmentVariable("Container_PayrollCalendar_PartitionKey");

            var containers = new List<ContainerInfo>();
            containers.Add(new ContainerInfo { Name = Container_PayrollCalendar_Name, PartitionKey = Container_PayrollCalendar_PartitionKey });
            containers.Add(new ContainerInfo { Name = Container_Customer_Name, PartitionKey = Container_Customer_PartitionKey });

            foreach (var cont in containers)
            {
                if (string.IsNullOrWhiteSpace(cont.Name)) ThrowEnvironmentVariableMissingException(cont.Name);
                if (string.IsNullOrWhiteSpace(cont.PartitionKey)) ThrowEnvironmentVariableMissingException(cont.PartitionKey);
            }

            return containers;
        }

        public static CosmosConnection GetCosmosConnection()
        {
            string endpointUrl = Environment.GetEnvironmentVariable("DB_ENDPOINT_URL");
            string primaryKey = Environment.GetEnvironmentVariable("DB_PRIMARY_KEY");
            string databaseName = Environment.GetEnvironmentVariable("DB_NAME");

            if (string.IsNullOrWhiteSpace(endpointUrl)) ThrowEnvironmentVariableMissingException("DB_ENDPOINT_URL");
            if (string.IsNullOrWhiteSpace(primaryKey)) ThrowEnvironmentVariableMissingException("DB_PRIMARY_KEY");
            if (string.IsNullOrWhiteSpace(databaseName)) ThrowEnvironmentVariableMissingException("DB_NAME");

            return new CosmosConnection
            {
                DatabaseName = databaseName,
                EndpointUrl = endpointUrl,
                PrimaryKey = primaryKey
            };
        }

        public static bool TurnOn()
        {
            string turnOn = Environment.GetEnvironmentVariable("TurnOn");

            if (string.IsNullOrWhiteSpace(turnOn)) ThrowEnvironmentVariableMissingException("TurnOn");

            return Convert.ToBoolean(turnOn);
        }

        public static string ServiceEnvironment()
        {
            string serviceEnv = Environment.GetEnvironmentVariable("Service_Environment");

            if (string.IsNullOrWhiteSpace(serviceEnv)) ThrowEnvironmentVariableMissingException("Service_Environment");

            return serviceEnv.ToLower();
        }

        private static void ThrowEnvironmentVariableMissingException(string variableName)
        {
            throw new EnvironmentVariableMissingException($"Environment variable \"{variableName}\" is missing");
        }
    }
}
