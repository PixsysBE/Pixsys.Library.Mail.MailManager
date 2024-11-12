// -----------------------------------------------------------------------
// <copyright file="MailManagerAppSettings.cs" company="Pixsys">
// Copyright (c) Pixsys. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Pixsys.Library.Mail.MailManager.Models
{
    /// <summary>
    /// Mail Manager app settings.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1206:Declaration keywords should follow order", Justification = "Reviewed.")]
    internal sealed class MailManagerAppSettings
    {
        /// <summary>
        /// Gets or sets the templates folder.
        /// </summary>
        /// <value>
        /// The templates folder.
        /// </value>
        public required string TemplatesFolder { get; set; }

        /// <summary>
        /// Gets or sets the content folder.
        /// </summary>
        /// <value>
        /// The content folder.
        /// </value>
        public required string ContentFolder { get; set; }

        /// <summary>
        /// Gets or sets the host.
        /// </summary>
        /// <value>
        /// The host.
        /// </value>
        public required string Host { get; set; }

        /// <summary>
        /// Gets or sets the port.
        /// </summary>
        /// <value>
        /// The port.
        /// </value>
        public int Port { get; set; }

        /// <summary>
        /// Gets or sets the timeout.
        /// </summary>
        /// <value>
        /// The timeout.
        /// </value>
        public int Timeout { get; set; } = 10000;

        /// <summary>
        /// Gets or sets the email from address.
        /// </summary>
        /// <value>
        /// The email from address.
        /// </value>
        public required string EmailFromAddress { get; set; }

        /// <summary>
        /// Gets or sets the display name of the email from.
        /// </summary>
        /// <value>
        /// The display name of the email from.
        /// </value>
        public string? EmailFromDisplayName { get; set; }

        /// <summary>
        /// Gets or sets the name of the network credential user.
        /// </summary>
        /// <value>
        /// The name of the network credential user.
        /// </value>
        public string? NetworkCredentialUserName { get; set; }

        /// <summary>
        /// Gets or sets the network credential password.
        /// </summary>
        /// <value>
        /// The network credential password.
        /// </value>
        public string? NetworkCredentialPassword { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [enable SSL].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [enable SSL]; otherwise, <c>false</c>.
        /// </value>
        public bool EnableSsl { get; set; }

        /// <summary>
        /// Gets or sets the disposition notification to.
        /// </summary>
        /// <value>
        /// The disposition notification to.
        /// </value>
        public string? DispositionNotificationTo { get; set; }

        /// <summary>
        /// Gets or sets the return receipt to.
        /// </summary>
        /// <value>
        /// The return receipt to.
        /// </value>
        public string? ReturnReceiptTo { get; set; }
    }
}