using System;

namespace Elements.APNG.Serverless.Models.Interface
{
    /// <summary>
    /// ICreatedByModifiedBy interface
    /// </summary>
    public interface ICreatedBy
    {
        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        /// <value>
        /// The created by.
        /// </value>
        public Guid CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the modified by.
        /// </summary>
        /// <value>
        /// The modified by.
        /// </value>
        public Guid ModifiedBy { get; set; }
    }
}
