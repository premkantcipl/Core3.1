// <copyright file="IPayrollService.cs" company="ElementsGS">
// Copyright (c) ElementsGS. All rights reserved.
// </copyright>

using System.Threading.Tasks;

namespace Elements.APNG.Serverless.Services.Interfaces
{
    /// <summary>
    /// IPayroll Service 
    /// </summary>
    public interface IPayrollService
    {
        /// <summary>
        /// To notify customer for payroll submitting
        /// </summary>
        /// <returns></returns>
        Task<bool> NotifySubmitPayrollAsync();

    }
}
