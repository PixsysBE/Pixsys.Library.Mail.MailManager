// -----------------------------------------------------------------------
// <copyright file="MailAttachment.cs" company="Pixsys">
// Copyright (c) Pixsys. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Pixsys.Library.Mail.MailManager.Models
{
    /// <summary>
    /// The mail attachment model.
    /// </summary>
    public class MailAttachment
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the file path.
        /// </summary>
        /// <value>
        /// The file path.
        /// </value>
        public string? FilePath { get; set; }
    }
}