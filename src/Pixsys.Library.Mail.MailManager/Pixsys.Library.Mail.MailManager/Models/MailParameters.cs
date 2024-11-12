// -----------------------------------------------------------------------
// <copyright file="MailParameters.cs" company="Pixsys">
// Copyright (c) Pixsys. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Pixsys.Library.Mail.MailManager.Models
{
    /// <summary>
    /// Mail parameters.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1206:Declaration keywords should follow order", Justification = "Reviewed.")]
    public class MailParameters
    {
        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        /// <value>
        /// The subject.
        /// </value>
        public required string Subject { get; set; }

        /// <summary>
        /// Gets or sets the body.
        /// </summary>
        /// <value>
        /// The body.
        /// </value>
        public required string Body { get; set; }

        /// <summary>
        /// Gets or sets all recipients separated by a comma or a semicolon.
        /// </summary>
        public required string To { get; set; }

        /// <summary>
        /// Gets or sets all CC recipients separated by a comma or a semicolon.
        /// </summary>
        public string? Cc { get; set; }

        /// <summary>
        /// Gets or sets all BCC recipients separated by a comma or a semicolon.
        /// </summary>
        public string? Bcc { get; set; }

        /// <summary>
        /// Gets or sets the attachments.
        /// </summary>
        /// <value>
        /// The attachments.
        /// </value>
        public List<MailAttachment>? Attachments { get; set; }
    }
}
