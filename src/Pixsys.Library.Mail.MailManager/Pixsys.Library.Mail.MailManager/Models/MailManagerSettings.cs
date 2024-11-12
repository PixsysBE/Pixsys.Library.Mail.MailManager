// -----------------------------------------------------------------------
// <copyright file="MailManagerSettings.cs" company="Pixsys">
// Copyright (c) Pixsys. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Net.Mail;

namespace Pixsys.Library.Mail.MailManager.Models
{
    /// <summary>
    /// the mail manager settings model.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1206:Declaration keywords should follow order", Justification = "Reviewed.")]
    public class MailManagerSettings
    {
        /// <summary>
        /// Gets or sets the templates folder.
        /// </summary>
        /// <value>
        /// The templates folder.
        /// </value>
        public required DirectoryInfo TemplatesFolder { get; set; }

        /// <summary>
        /// Gets or sets the content folder.
        /// </summary>
        /// <value>
        /// The content folder.
        /// </value>
        public required DirectoryInfo ContentFolder { get; set; }

        /// <summary>
        /// Gets or sets the SMTP client.
        /// </summary>
        /// <value>
        /// The SMTP client.
        /// </value>
        public required SmtpClient SmtpClient { get; set; }

        /// <summary>
        /// Gets or sets the email from address.
        /// </summary>
        /// <value>
        /// The email from.
        /// </value>
        public required MailAddress EmailFrom { get; set; }

        /// <summary>
        /// Gets or sets mail address for the receiving mail server to send a DSN (delivery status notification) as soon as the person opens the email.
        /// </summary>
        public string? DispositionNotificationTo { get; set; }

        /// <summary>
        /// Gets or sets mail address for the receiving mail server to send a DSN (delivery status notification) as soon as it receives the email.
        /// </summary>
        public string? ReturnReceiptTo { get; set; }
    }
}