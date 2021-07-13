// <copyright file="EGSInvoiceAppApConfigurationExtension.cs" company="ElementsGS">
// Copyright (c) ElementsGS. All rights reserved.
// </copyright>

using Elements.APNG.Serverless.Data.EF.Interfaces;
using Elements.APNG.Serverless.Data.EF.Repository;
using Elements.APNG.Serverless.Models.ConfigurationModel;
using Elements.APNG.Serverless.Services;
using Elements.APNG.Serverless.Services.Exceptions;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace Elements.APNG.Serverless.API.Extensions
{
    /// <summary>
    /// EGS Invoice App Configuration Extension
    /// </summary>
    public static class PayrollNotifyConfigurationExtension
    {
        #region Public Methods

        /// <summary>
        /// Registers the invoice application dependencies.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns>Returns IServiceCollection</returns>
        public static IFunctionsHostBuilder AddApiDependencies(this IFunctionsHostBuilder services)
        {         
            services.AddRepositories();
            services.LoadConfigurations();
            return services;
        }


        /// <summary>
        /// Load configurations inside of the services collection
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IFunctionsHostBuilder LoadConfigurations(this IFunctionsHostBuilder services)
        {
            // Bind database-related bindings
            List<ContainerInfo> containers = EnvironmentVariables.GetCosmosContainers();

            // register CosmosDB client and data repositories
            var cosmosConfig = EnvironmentVariables.GetCosmosConnection();
            services.AddCosmosDb(cosmosConfig.EndpointUrl,
                                 cosmosConfig.PrimaryKey,
                                 cosmosConfig.DatabaseName,
                                 containers);

            return services;
        }

        #endregion

        #region Private Methods

        private static IFunctionsHostBuilder AddRepositories(this IFunctionsHostBuilder services)
        {
            services.Services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.Services.AddScoped<IPayrollCalendarRepository, PayrollCalendarRepository>();
           
            return services;
        }
        private static void ThrowEnvironmentVariableMissingException(string variableName)
        {
            throw new EnvironmentVariableMissingException($"Environment variable \"{variableName}\" is missing");
        }

        #endregion
    }
}
