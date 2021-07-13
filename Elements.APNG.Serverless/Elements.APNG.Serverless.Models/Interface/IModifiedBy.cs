using System;

namespace Elements.APNG.Serverless.Models.Interface
{
    /// <summary>
    /// IModifiedBy interface
    /// </summary>
    public interface IModifiedBy
    {
        /// <summary>
        /// Gets or sets the modified by.
        /// </summary>
        /// <value>
        /// The modified by.
        /// </value>
        public Guid ModifiedBy { get; set; }
    }
}
