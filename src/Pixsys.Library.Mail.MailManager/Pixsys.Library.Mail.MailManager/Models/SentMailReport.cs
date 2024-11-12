// -----------------------------------------------------------------------
// <copyright file="SentMailReport.cs" company="Pixsys">
// Copyright (c) Pixsys. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Net.Mail;

namespace Pixsys.Library.Mail.MailManager.Models
{
    /// <summary>
    /// The sent mail report model.
    /// </summary>
    public class SentMailReport
    {
        /// <summary>
        /// Gets or sets the mail message.
        /// </summary>
        /// <value>
        /// The mail message.
        /// </value>
        public MailMessage? MailMessage { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the sending is successful.
        /// </summary>
        /// <value>
        ///   <c>true</c> if successful; otherwise, <c>false</c>.
        /// </value>
        public bool IsSuccessful { get; set; }

        /// <summary>
        /// Gets or sets the errors encountered.
        /// </summary>
        /// <value>
        /// The errors.
        /// </value>
        public List<string>? Errors { get; set; }

        /// <summary>
        /// Gets or sets the warnings encountered.
        /// </summary>
        /// <value>
        /// The warnings.
        /// </value>
        public List<string>? Warnings { get; set; }
    }
}