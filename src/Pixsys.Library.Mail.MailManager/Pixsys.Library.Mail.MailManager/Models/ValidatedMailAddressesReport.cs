// -----------------------------------------------------------------------
// <copyright file="ValidatedMailAddressesReport.cs" company="Pixsys">
// Copyright (c) Pixsys. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Net.Mail;

namespace Pixsys.Library.Mail.MailManager.Models
{
    /// <summary>
    /// The validated mail addresses report model.
    /// </summary>
    internal sealed class ValidatedMailAddressesReport
    {
        /// <summary>
        /// Gets or sets the validated mail addresses.
        /// </summary>
        /// <value>
        /// The addresses.
        /// </value>
        public List<MailAddress>? Addresses { get; set; }

        /// <summary>
        /// Gets or sets the non valid entries.
        /// </summary>
        /// <value>
        /// The non valid entries.
        /// </value>
        public List<string>? NonValidEntries { get; set; }
    }
}