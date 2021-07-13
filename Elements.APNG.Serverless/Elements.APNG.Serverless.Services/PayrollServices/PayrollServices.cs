// <copyright file="PayrollServices.cs" company="ElementsGS">
// Copyright (c) ElementsGS. All rights reserved.
// </copyright>

using AutoMapper;
using Elements.APNG.Serverless.Data.EF.Interfaces;
using Elements.APNG.Serverless.Services.Extensions;
using Elements.APNG.Serverless.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elements.APNG.Serverless.Services.PayrollServices
{
    /// <summary>
    /// Payroll Services
    /// </summary>
    /// <seealso cref="ServiceBase{Models.Model.Payroll, Data.EF.Entities.Payroll, Guid}" />
    /// <seealso cref="IPayrollService" />
    public class PayrollServices : IPayrollService
    {
        #region Fields


        /// <summary>
        /// Gets the logger service.
        /// </summary>
        /// <value>
        /// The logger service.
        /// </value>
        public ILogger<PayrollServices> LoggerService { get; }

        /// <summary>
        /// Gets or sets the mapper.
        /// </summary>
        /// <value>
        /// The mapper.
        /// </value>
        public IMapper Mapper { get; set; }

        private readonly IInvoiceMailTemplateService _invoiceMailTemplateService;
        private readonly ICustomerRepository _customerRepository;
        private readonly IPayrollCalendarRepository _payrollCalendarRepository;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PayrollServices"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="eFRepository">The e f repository.</param>
        /// <param name="mapper">The mapper.</param>
        public PayrollServices(IMapper mapper
            , ILogger<PayrollServices> loggerService
            , IInvoiceMailTemplateService invoiceMailTemplateService
            , ICustomerRepository customerRepository
            , IPayrollCalendarRepository payrollCalendarRepository
            )
        {
            Mapper = mapper;
            LoggerService = loggerService;
            _invoiceMailTemplateService = invoiceMailTemplateService;
            _customerRepository = customerRepository;
            _payrollCalendarRepository = payrollCalendarRepository;
        }

        #endregion

        #region IPayrollService

        /// <summary>
        /// submit payroll reminder
        /// </summary>
        /// <returns></returns>
        public async Task<bool> NotifySubmitPayrollAsync()
        {
            var startDue = DateTime.Today;
            var endDue = startDue.AddDays(5).EndOfDay();

            Dictionary<string, DateTime> output = new Dictionary<string, DateTime>();
            
            if (EnvironmentVariables.ServiceEnvironment() == "production")
            {
                var customerAll = await _customerRepository.GetCustomers();
                var payrollCalendars = await _payrollCalendarRepository.GetPayrollCalendars(startDue, endDue);

                if (payrollCalendars.Any())
                {
                    int month = payrollCalendars.First().Month;
                    int year = payrollCalendars.First().Year;

                    // take all payroll due types
                    IEnumerable<short> payrollDueTypesIds = payrollCalendars.Select(x => x.PayrollDueType).Distinct();

                    // take a list due to performances
                    var customersList = customerAll.Where(c => c.Status == 1
                                        && !string.IsNullOrEmpty(c.Email)
                                        && payrollDueTypesIds.Contains((short)c.PayrollDue))
                        .Select(i => new { i.Id, i.Email, i.PayrollDue }).ToList();

                    if (customerAll.Any())
                    {
                        foreach (var customer in customersList)
                        {
                            // find submission due date per customer
                            DateTime? submissionDueDate = payrollCalendars.FirstOrDefault(x => x.PayrollDueType == (short)customer.PayrollDue)?.DueDateOfSubmissionOfChangeForm;

                            if (submissionDueDate.HasValue && !output.ContainsKey(customer.Email))
                            {
                                output.Add(customer.Email, submissionDueDate.Value);
                            }
                        }
                    }
                }
            }
            else
            {
                output.Add("ap_test@elementsgs.com", DateTime.Now);
            }

            if (output.Any())
            {
                await _invoiceMailTemplateService.SendSubmitPayrollReminderAsync(output);
            }
            else
            {
                LoggerService.LogInformation("[NotifySubmitPayroll] - no customers found or no payroll calendar entries within next 5 days");
            }
            return true;
        }
        #endregion
    }
}
