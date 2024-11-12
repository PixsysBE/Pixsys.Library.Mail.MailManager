// -----------------------------------------------------------------------
// <copyright file="IMailManager.cs" company="Pixsys">
// Copyright (c) Pixsys. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Pixsys.Library.Mail.MailManager.Models;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace Pixsys.Library.Mail.MailManager.Interfaces
{
    /// <summary>
    /// The Mail Manager interface.
    /// </summary>
    public interface IMailManager
    {
        /// <summary>
        /// Gets the mail body.
        /// </summary>
        /// <param name="p">The mail body parameters.</param>
        /// <returns>The mail body.</returns>
        string GetBody(MailBodyParameters p);

        /// <summary>
        /// Sends the mail.
        /// </summary>
        /// <param name="p">The mail parameters.</param>
        /// <returns>The mail report.</returns>
        SentMailReport? Send(MailParameters p);

        /// <summary>
        /// The validation callback delegate.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="certificate">The certificate.</param>
        /// <param name="chain">The chain.</param>
        /// <param name="sslPolicyErrors">The SSL policy errors.</param>
        /// <returns>True or false.</returns>
        bool ServerCertificateValidationCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors);
    }
}